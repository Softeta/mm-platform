using API.Customization.Pagination;
using API.WebClients.Clients;
using API.WebClients.Clients.FormRecognizer;
using API.WebClients.Clients.FormRecognizer.Models;
using API.WebClients.Clients.HereSearch;
using API.WebClients.Clients.TagSystem;
using API.WebClients.Constants;
using BackOffice.Application.Contracts.Responses;
using BackOffice.Application.Contracts.Responses.Cv;
using Contracts.AdministrationSettings;
using Contracts.Shared;
using Domain.Seedwork.Exceptions;
using MediatR;
using Persistence.Customization.FileStorage.Clients.Private;
using Persistence.Customization.Queries;
using Persistence.Customization.TableStorage;
using Address = Domain.Seedwork.Shared.ValueObjects.Address;

namespace BackOffice.Application.Queries;

public class GetCandidateFromCvQueryHandler : IRequestHandler<GetCandidateFromCvQuery, CvCandidateResponse?>
{
    private readonly IReadOnlyCollection<string> _supportedFileExtensions = new[] { ".pdf" };

    private readonly IPrivateFileDownloadClient _fileDownloadClient;
    private readonly IFormRecognizerApiClient _formRecognizerApiClient;
    private readonly ILocationProvider _locationProvider;
    private readonly IAdministrationSettingsClient _administrationSettingsClient;
    private readonly IMediator _mediator;
    private readonly ITagSystemApiClient _tagSystemApiClient;

    public GetCandidateFromCvQueryHandler(
        IPrivateFileDownloadClient fileDownloadClient,
        IFormRecognizerApiClient formRecognizerApiClient,
        ILocationProvider locationProvider,
        IMediator mediator,
        IAdministrationSettingsClient administrationSettingsClient,
        ITagSystemApiClient tagSystemApiClient)
    {
        _fileDownloadClient = fileDownloadClient;
        _formRecognizerApiClient = formRecognizerApiClient;
        _locationProvider = locationProvider;
        _mediator = mediator;
        _administrationSettingsClient = administrationSettingsClient;
        _tagSystemApiClient = tagSystemApiClient;
    }

    public async Task<CvCandidateResponse?> Handle(GetCandidateFromCvQuery request, CancellationToken cancellationToken)
    {
        var fileUrl = await GetFileUrlAsync(request.FileUri, request.FileCacheId, cancellationToken);
        var file = await _fileDownloadClient.DownloadAsync(fileUrl, cancellationToken);
        var analyzeResult = await _formRecognizerApiClient.AnalyzeCvAsync(file, request.Source, cancellationToken);

        if (analyzeResult == null)
        {
            return null;
        }

        var languagesTask = GetLanguagesAsync(analyzeResult.Languages);
        var addressTask = GetAddressAsync(analyzeResult.Location);
        var experiencesTask = BuildWorkExperiencesAsync(analyzeResult.Experiences);
        var educationsTask = Task.FromResult(BuildEducations(analyzeResult.Educations));
        var coursesTask = Task.FromResult(BuildCourses(analyzeResult.Courses));
        var skillsTask = GetSkillsAsync(analyzeResult.Skills);

        await Task.WhenAll(languagesTask, addressTask, skillsTask, experiencesTask, educationsTask, coursesTask);

        return new CvCandidateResponse
        {
            FirstName = analyzeResult.FirstName,
            LastName = analyzeResult.LastName,
            PhoneNumber = analyzeResult.PhoneNumber,
            Email = analyzeResult.Email,
            LinkedInUrl = analyzeResult.LinkedInUrl,
            Languages = languagesTask.Result,
            Address = addressTask.Result,
            CandidateWorkExperiences = experiencesTask.Result,
            CandidateEducations = educationsTask.Result,
            CurrentPosition = experiencesTask.Result.FirstOrDefault(x => x.IsCurrentJob)?.Position,
            CandidateCourses = coursesTask.Result,
            Skills = skillsTask.Result
        };
    }

    private async Task<IEnumerable<Skill>> GetSkillsAsync(IEnumerable<string?> skills)
    {
        var result = new List<Skill>();
        await Parallel.ForEachAsync(skills, async (skill, _) =>
        {
            var foundSkill = await GetSkillAsync(skill);
            if (foundSkill != null)
            {
                result.Add(foundSkill);
            }
        });
        return result;
    }

    private static IEnumerable<CandidateCvCourseResponse> BuildCourses(IEnumerable<string?> courses)
    {
        return courses.Where(x => x != null).Select(x => new CandidateCvCourseResponse
        {
            Name = x,
        });
    }

    private async Task<string> GetFileUrlAsync(string? fileUrl, Guid? fileCacheId, CancellationToken cancellationToken)
    {
        if (fileCacheId.HasValue)
        {
            var fileQuery = new GetFileQuery(fileCacheId, FileCacheTableStorage.Candidate.FilePartitionKey);
            var fileData = await _mediator.Send(fileQuery, cancellationToken);
            fileUrl = fileData.FileUrl;
        }

        if (fileUrl == null)
        {
            throw new NotFoundException("File not found");
        }

        if (fileUrl.Contains('?'))
        {
            var index = fileUrl.IndexOf('?', StringComparison.Ordinal);
            fileUrl = fileUrl[..index];
        }

        var fileExtension = new FileInfo(fileUrl).Extension;
        if (!_supportedFileExtensions.Contains(fileExtension))
        {
            throw new NotSupportedException("File extension is not supported");
        }

        return fileUrl;
    }

    private async Task<IEnumerable<Language>> GetLanguagesAsync(string?[]? foundLanguages)
    {
        var languages = new List<Language>();
        if (foundLanguages == null)
        {
            return languages;
        }

        await Parallel.ForEachAsync(foundLanguages.Where(l => l != null), async (s, token) =>
        {
            var endpoint = string.Format(Endpoints.AdministrationSettingsService.Languages, 1, s);
            var systemLanguage = await _administrationSettingsClient.GetAsync<PagedResponse<LanguageResponse>>(endpoint);
            if (systemLanguage?.Data != null && systemLanguage.Data.Any())
            {
                var languageData = systemLanguage.Data.First();
                languages.Add(new Language
                {
                    Code = languageData.Code,
                    Id = languageData.Id,
                    Name = languageData.Name
                });
            }
        });

        return languages;
    }

    private async Task<AddressWithLocation?> GetAddressAsync(string? location)
    {
        if (location is null)
        {
            return null;
        }

        try
        {
            var addressDetails = await _locationProvider.GetAddressDetailsAsync(location);
            return AddressWithLocation.FromDomain(new Address(
                addressDetails.AddressLine,
                addressDetails.City,
                addressDetails.Country,
                addressDetails.PostalCode,
                addressDetails.Longitude,
                addressDetails.Latitude));
        }
        catch
        {
            return null;
        }
    }

    private async Task<IEnumerable<CandidateCvWorkExperienceResponse>> BuildWorkExperiencesAsync(IEnumerable<Experience> experiences)
    {
        var responses = new List<CandidateCvWorkExperienceResponse>();

        await Parallel.ForEachAsync(experiences, async (experience, token) =>
        {
            var position = await GetPositionAsync(experience.Position);

            var response = new CandidateCvWorkExperienceResponse
            {
                CompanyName = experience.Company ?? string.Empty,
                From = experience.DateFrom ?? DateTimeOffset.MinValue,
                To = experience.DateTo,
                IsCurrentJob = experience.IsCurrentlyWorking,
                JobDescription = experience.Description,
                Position = position
            };

            responses.Add(response);
        });

        return responses;
    }

    private async Task<Position?> GetPositionAsync(string? positionTitle)
    {
        if (positionTitle == null)
        {
            return null;
        }

        try
        {
            var positionsResponse = await _tagSystemApiClient.GetSimilarPositions(positionTitle, 1);
            var position = positionsResponse.Data.FirstOrDefault();
            return position != null ? Position.From(position.Id, position.Code, null, null) : null;
        }
        catch
        {
            return null;
        }
    }

    private async Task<Skill?> GetSkillAsync(string? skillTitle)
    {
        if (skillTitle == null)
        {
            return null;
        }

        try
        {
            var skillsResponse = await _tagSystemApiClient.GetSimilarSkills(skillTitle, 1);
            var skill = skillsResponse.Data.FirstOrDefault();
            return skill != null ? Skill.From(skill.Id, skill.Code) : null;
        }
        catch
        {
            return null;
        }
    }

    private static IEnumerable<CandidateCvEducationResponse> BuildEducations(IEnumerable<Education> educations)
    {
        return educations.Select(x => new CandidateCvEducationResponse
        {
            Degree = x.Degree ?? string.Empty,
            FieldOfStudy = x.FieldOfStudy ?? string.Empty,
            From = x.DateFrom ?? DateTimeOffset.MinValue,
            SchoolName = x.School ?? string.Empty,
            To = x.DateTo
        });
    }
}