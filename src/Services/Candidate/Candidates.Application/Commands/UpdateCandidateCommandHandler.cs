using API.WebClients.Clients.HereSearch;
using API.WebClients.Clients.HereSearch.Models;
using Candidates.Application.Validations;
using Candidates.Domain.Aggregates.CandidateAggregate;
using Candidates.Domain.Aggregates.CandidateAggregate.Entities;
using Candidates.Infrastructure.Persistence.Repositories;
using Contracts.Shared;
using Contracts.Shared.Requests;
using Domain.Seedwork.Enums;
using MediatR;
using Persistence.Customization.FileStorage.Clients.Private;
using Persistence.Customization.FileStorage.Clients.Public;
using Persistence.Customization.Queries;
using Persistence.Customization.TableStorage;
using ValueObjects = Domain.Seedwork.Shared.ValueObjects;

namespace Candidates.Application.Commands
{
    public class UpdateCandidateCommandHandler : ModifyCandidateBaseCommandHandler<UpdateCandidateCommand, Candidate>
    {
        private readonly ILocationProvider _locationProvider;
        private readonly IMediator _mediator;
        private readonly IPrivateFileDeleteClient _privateFileDeleteClient;
        private readonly IPublicFileDeleteClient _publicFileDeleteClient;

        public UpdateCandidateCommandHandler(
            ICandidateRepository candidateRepository,
            ILocationProvider locationProvider,
            IMediator mediator,
            IPrivateFileDeleteClient privateFileDeleteClient,
            IPublicFileDeleteClient publicFileDeleteClient) : base(candidateRepository)
        {
            _locationProvider = locationProvider;
            _mediator = mediator;
            _privateFileDeleteClient = privateFileDeleteClient;
            _publicFileDeleteClient = publicFileDeleteClient;
        }

        protected override async Task<Candidate> Handle(UpdateCandidateCommand request, Candidate candidate, CancellationToken cancellationToken)
        {
            await ValidateAsync(request, candidate);

            var addressDetails = await GetAddressDetailsAsync(request.Address);

            var fileCacheIds = new List<Guid>();

            var deletePictureTask  =  DeleteOldImageAsync(request.Picture.HasChanged, candidate.Picture, cancellationToken);
            var deleteCurriculumVitaeTask = DeleteOldFileAsync(request.CurriculumVitae.HasChanged, candidate.CurriculumVitae?.Uri, cancellationToken);
            var deleteVideoTask = DeleteOldFileAsync(request.Video.HasChanged, candidate.Video?.Uri, cancellationToken);
            var picturePathsTask = GetImagesAsync(request.Picture, fileCacheIds);
            var curriculumVitaeTask = GetFileAsync(request.CurriculumVitae, fileCacheIds);
            var videoTask = GetFileAsync(request.Video, fileCacheIds);

            await Task.WhenAll(
                deletePictureTask,
                deleteCurriculumVitaeTask,
                deleteVideoTask,
                picturePathsTask,
                curriculumVitaeTask,
                videoTask);

            var curriculumVitae = curriculumVitaeTask.Result;
            var video = videoTask.Result;
            var picturePaths = picturePathsTask.Result;

            candidate.Update(
                request.Email,
                request.FirstName,
                request.LastName,
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
                request.CurriculumVitae.HasChanged,
                video.FileUrl,
                video.FileName,
                request.Video.HasChanged,
                picturePaths,
                request.Picture.HasChanged,
                request.ActivityStatuses,
                request.Industries.Select(x => new CandidateIndustry(x.Id, candidate.Id, x.Code)),
                request.Skills.Select(x => new CandidateSkill(x.Id, candidate.Id, x.Code, x.AliasTo?.Id, x.AliasTo?.Code)),
                request.DesiredSkills.Select(x => new CandidateDesiredSkill(x.Id, candidate.Id, x.Code, x.AliasTo?.Id, x.AliasTo?.Code)),
                request.Languages.Select(x => new CandidateLanguage(candidate.Id, x.Id, x.Code, x.Name)),
                request.Hobbies.Select(x => new CandidateHobby(x.Id, candidate.Id, x.Code)),
                request.Formats,
                request.WorkTypes,
                fileCacheIds);

            CandidateRepository.Update(candidate);
            await CandidateRepository.UnitOfWork.SaveEntitiesAsync<Candidate>(cancellationToken);

            return candidate;
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

        private async Task DeleteOldFileAsync(bool hasChanged, string? currentFieUrl, CancellationToken token)
        {
            if (hasChanged && !string.IsNullOrWhiteSpace(currentFieUrl))
            {
                await _privateFileDeleteClient.DeleteAsync(currentFieUrl, token);
            }
        }

        private async Task DeleteOldImageAsync(bool hasChanged, ValueObjects.Image? oldImage, CancellationToken token)
        {
            if (oldImage is not null && hasChanged)
            {
                var files = new[]
                {
                    new Uri(oldImage.OriginalUri),
                    new Uri(oldImage.ThumbnailUri)
                };

                await _publicFileDeleteClient.BatchDeleteAsync(files, token);
            }
        }

        private async Task<(string? FileUrl, string? FileName)> GetFileAsync(UpdateFileCacheRequest file, List<Guid> fileCacheIds)
        {
            if (file.HasChanged)
            {
                var getFileCommand = new GetFileQuery(file.CacheId,
                    FileCacheTableStorage.Candidate.FilePartitionKey);
                var result =  await _mediator.Send(getFileCommand);

                if (file.CacheId.HasValue)
                {
                    fileCacheIds.Add(file.CacheId.Value);
                }
                return result;
            }
            return (null, null);
        }

        private async Task<Dictionary<ImageType, string?>?> GetImagesAsync(UpdateFileCacheRequest image, List<Guid> fileCacheIds)
        {
            if (image.HasChanged)
            {
                var command = new GetImageQuery(image.CacheId,
                    FileCacheTableStorage.Candidate.FilePartitionKey);
                var result = await _mediator.Send(command);

                if (image.CacheId.HasValue)
                {
                    fileCacheIds.Add(image.CacheId.Value);
                }
                return result;
            }
            return null;
        }

        private async Task ValidateAsync(UpdateCandidateCommand request, Candidate candidate)
        {
            if (candidate.Email?.Address != request.Email)
            {
                await _mediator.Publish(new ValidateCandidateDuplicationByEmailValidation(request.Email));
            }
            if (candidate.LinkedInUrl != request.LinkedInUrl)
            {
                await _mediator.Publish(new ValidateCandidateDuplicationByLinkedInValidation(request.LinkedInUrl));
            }
        }
    }
}
