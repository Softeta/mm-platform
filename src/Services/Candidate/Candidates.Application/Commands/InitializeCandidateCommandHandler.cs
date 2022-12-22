using API.WebClients.Clients.HereSearch;
using API.WebClients.Clients.HereSearch.Models;
using Candidates.Application.Validations;
using Candidates.Domain.Aggregates.CandidateAggregate;
using Candidates.Domain.Aggregates.CandidateAggregate.Entities;
using Candidates.Infrastructure.Persistence.Repositories;
using Contracts.Candidate.Courses.Requests;
using Contracts.Candidate.Educations.Requests;
using Contracts.Shared;
using Domain.Seedwork.Enums;
using MediatR;
using Persistence.Customization.Queries;
using Persistence.Customization.TableStorage;

namespace Candidates.Application.Commands
{
    public class InitializeCandidateCommandHandler : IRequestHandler<InitializeCandidateCommand, Candidate>
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly ILocationProvider _locationProvider;
        private readonly IMediator _mediator;

        public InitializeCandidateCommandHandler(
            ICandidateRepository candidateRepository,
            ILocationProvider locationProvider,
            IMediator mediator)

        {
            _candidateRepository = candidateRepository;
            _locationProvider = locationProvider;
            _mediator = mediator;
        }

        public async Task<Candidate> Handle(InitializeCandidateCommand request, CancellationToken cancellationToken)
        {
            await ValidateAsync(request);

            var candidate = new Candidate();
            var fileCacheIds = new List<Guid>();

            var addressDetails = await GetAddressDetailsAsync(request.Address);

            var industries = request.Industries.Select(s =>
                new CandidateIndustry(s.Id, candidate.Id, s.Code));

            var skills = request.Skills.Select(s =>
                new CandidateSkill(s.Id, candidate.Id, s.Code, s.AliasTo?.Id, s.AliasTo?.Code));

            var desiredSkills = request.DesiredSkills.Select(s =>
                new CandidateDesiredSkill(s.Id, candidate.Id, s.Code, s.AliasTo?.Id, s.AliasTo?.Code));

            var languages = request.Languages.Select(s =>
                new CandidateLanguage(candidate.Id, s.Id, s.Code, s.Name));

            var hobbies = request.Hobbies.Select(s =>
                new CandidateHobby(s.Id, candidate.Id, s.Code));

            var workExperiences = request.WorkExperiences.Select(w =>
            {
                var candidate = new CandidateWorkExperience();
                candidate.Create(
                    candidate.Id,
                    w.Type,
                    w.CompanyName,
                    w.Position.Id,
                    w.Position.Code,
                    w.Position.AliasTo?.Id,
                    w.Position.AliasTo?.Code,
                    w.From,
                    w.To,
                    w.JobDescription,
                    w.IsCurrentJob,
                    w.Skills.Select(s => new CandidateWorkExperienceSkill(s.Id, candidate.Id, s.Code, s.AliasTo?.Id, s.AliasTo?.Code)));
                return candidate;
            });

            var courses = new List<CandidateCourse>();
            var coursesTask = Parallel.ForEachAsync(request.Courses, cancellationToken, async (course, _) =>
            {
                var candidateCourse = await GetCandidateCourseAsync(course, candidate.Id, fileCacheIds);
                courses.Add(candidateCourse);
            });
            var educations = new List<CandidateEducation>();
            var educationsTask = Parallel.ForEachAsync(request.Educations, cancellationToken, async (education, _) =>
            {
                var candidateEducation = await GetCandidateEducationAsync(education, candidate.Id, fileCacheIds);
                educations.Add(candidateEducation);
            });
            var picturePathsTask = GetImageAsync(request.Picture?.CacheId, fileCacheIds);
            var curriculumVitaeTask = GetFileAsync(request.CurriculumVitae?.CacheId, fileCacheIds);
            var videoTask = GetFileAsync(request.Video?.CacheId, fileCacheIds);

            await Task.WhenAll(
                coursesTask,
                educationsTask,
                picturePathsTask,
                curriculumVitaeTask,
                videoTask);
            var picturePaths = picturePathsTask.Result;
            var curriculumVitae = curriculumVitaeTask.Result;
            var video = videoTask.Result;

            candidate.Initialize(
                request.Email,
                request.FirstName,
                request.LastName,
                picturePaths,
                request.CurrentPosition?.Id,
                request.CurrentPosition?.Code,
                request.CurrentPosition?.AliasTo?.Id,
                request.CurrentPosition?.AliasTo?.Code,
                request.BirthDate,
                request.OpenForOpportunities,
                request.LinkedInUrl,
                request.PersonalWebsiteUrl,
                request.Address?.AddressLine,
                addressDetails?.City,
                addressDetails?.Country,
                addressDetails?.PostalCode,
                addressDetails?.Longitude,
                addressDetails?.Latitude,
                request.StartDate,
                request.EndDate,
                request.WeeklyWorkHours,
                request.Currency,
                request.Freelance?.HourlySalary,
                request.Freelance?.MonthlySalary,
                request.Permanent?.MonthlySalary,
                request.WorkingHourTypes,
                request.Phone?.CountryCode,
                request.Phone?.Number,
                request.YearsOfExperience,
                request.Bio,
                curriculumVitae.FileUrl,
                curriculumVitae.FileName,
                video.FileUrl,
                video.FileName,
                industries,
                skills,
                desiredSkills,
                languages,
                courses,
                educations,
                workExperiences,
                hobbies,
                request.Formats,
                request.WorkTypes,
                fileCacheIds);

            var candidateEntity = _candidateRepository.Add(candidate);
            await _candidateRepository.UnitOfWork.SaveEntitiesAsync<Candidate>(cancellationToken);

            return candidateEntity;
        }

        private async Task<CandidateCourse> GetCandidateCourseAsync(
            AddCandidateCourseRequest course,
            Guid candidateId,
            List<Guid> fileCacheIds)
        {
            var getCertificateCommand = new GetFileQuery(course.Certificate?.CacheId,
                FileCacheTableStorage.Candidate.FilePartitionKey);
            var certificate = await _mediator.Send(getCertificateCommand);

            if (course.Certificate != null && course.Certificate.CacheId.HasValue)
            {
                fileCacheIds.Add(course.Certificate.CacheId.Value);
            }
            return new CandidateCourse(
                    candidateId,
                    course.Name,
                    course.IssuingOrganization,
                    course.Description,
                    certificate.FileUrl,
                    certificate.FileName);
        }

        private async Task<CandidateEducation> GetCandidateEducationAsync(
            AddCandidateEducationRequest education,
            Guid candidateId,
            List<Guid> fileCacheIds)
        {
            var getCertificateCommand = new GetFileQuery(education.Certificate?.CacheId,
                FileCacheTableStorage.Candidate.FilePartitionKey);
            var certificate = await _mediator.Send(getCertificateCommand);


            if (education.Certificate != null && education.Certificate.CacheId.HasValue)
            {
                fileCacheIds.Add(education.Certificate.CacheId.Value);
            }
            return new CandidateEducation(
                candidateId,
                education.SchoolName,
                education.Degree,
                education.FieldOfStudy,
                education.From,
                education.To,
                education.StudiesDescription,
                education.IsStillStudying,
                certificate.FileUrl,
                certificate.FileName);
        }

        private async Task<(string? FileUrl, string? FileName)> GetFileAsync(Guid? cacheId, List<Guid> fileCacheIds)
        {
            var getFileCommand = new GetFileQuery(cacheId,
                FileCacheTableStorage.Candidate.FilePartitionKey);
            var file = await _mediator.Send(getFileCommand);
            if (cacheId.HasValue)
            {
                fileCacheIds.Add(cacheId.Value);
            }
            return file;
        }

        private async Task<Dictionary<ImageType, string?>?> GetImageAsync(Guid? cacheId, List<Guid> fileCacheIds)
        {
            var getImageCommand = new GetImageQuery(cacheId,
                FileCacheTableStorage.Candidate.FilePartitionKey);
            var imagePaths = await _mediator.Send(getImageCommand);

            if (cacheId.HasValue)
            {
                fileCacheIds.Add(cacheId.Value);
            }
            return imagePaths;
        }

        private async Task<AddressDetails?> GetAddressDetailsAsync(Address? address)
        {
            if (string.IsNullOrWhiteSpace(address?.AddressLine))
            {
                return null;
            }

            var areAllFieldsFilledIn =
                address.Latitude.HasValue &&
                address.Longitude.HasValue &&
                !string.IsNullOrWhiteSpace(address.City) &&
                !string.IsNullOrWhiteSpace(address.Country);

            if (areAllFieldsFilledIn)
            {
                return new AddressDetails
                {
                    AddressLine = address.AddressLine,
                    City = address.City!,
                    Country = address.Country!,
                    PostalCode = address.PostalCode,
                    Longitude = address.Longitude!.Value,
                    Latitude = address.Latitude!.Value
                };
            }

            return await _locationProvider.GetAddressDetailsAsync(address.AddressLine);
        }

        private async Task ValidateAsync(InitializeCandidateCommand request)
        {
            await _mediator.Publish(new ValidateCandidateDuplicationByEmailValidation(request.Email));
            await _mediator.Publish(new ValidateCandidateDuplicationByLinkedInValidation(request.LinkedInUrl));
        }
    }
}
