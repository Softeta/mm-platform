using API.WebClients.Clients.HereSearch;
using API.WebClients.Clients.HereSearch.Models;
using Jobs.Domain.Aggregates.JobAggregate;
using Jobs.Domain.Aggregates.JobAggregate.Entities;
using Jobs.Domain.Aggregates.JobAggregate.ValueObjects;
using Jobs.Infrastructure.Persistence.Repositories;
using MediatR;

namespace Jobs.Application.Commands
{
    public class InitializePendingJobCommandHandler : IRequestHandler<InitializePendingJobCommand, Job>
    {
        private readonly IJobRepository _jobRepository;
        private readonly ILocationProvider _locationProvider;

        public InitializePendingJobCommandHandler(IJobRepository jobRepository, ILocationProvider locationProvider)
        {
            _jobRepository = jobRepository;
            _locationProvider = locationProvider;
        }

        public async Task<Job> Handle(InitializePendingJobCommand request, CancellationToken cancellationToken)
        {
            var job = new Job();

            var addressDetails = await GetAddressDetailsAsync(request.Company.Address?.AddressLine);

            job.Initialize(
                new Company(
                    request.Company.Id,
                    request.Company.Status,
                    request.Company.Name,
                    request.Company.Address?.AddressLine,
                    addressDetails?.City,
                    addressDetails?.Country,
                    addressDetails?.PostalCode,
                    addressDetails?.Longitude,
                    addressDetails?.Latitude,
                    request.Company.Description,
                    request.Company.LogoUri,
                    request.Company.ContactPersons.Select(p => new JobContactPerson(
                        job.Id,
                        p.Id,
                        p.IsMainContact,
                        p.FirstName,
                        p.LastName,
                        p.PhoneNumber,
                        p.Email,
                        p.Position?.Id,
                        p.Position?.Code,
                        p.Position?.AliasTo?.Id,
                        p.Position?.AliasTo?.Code,
                        p.PictureUri,
                        p.SystemLanguage,
                        p.ExternalId))),
                request.Position.Id,
                request.Position.Code,
                request.Position.AliasTo?.Id,
                request.Position.AliasTo?.Code,
                request.StartDate,
                request.EndDate,
                request.WorkTypes,
                request.IsUrgent);

            var jobEntity = _jobRepository.Add(job);
            await _jobRepository.UnitOfWork.SaveEntitiesAsync<Job>(cancellationToken);

            return jobEntity;
        }

        private async Task<AddressDetails?> GetAddressDetailsAsync(string? addressLine)
        {
            if (string.IsNullOrWhiteSpace(addressLine))
            {
                return null;
            }

            return await _locationProvider.GetAddressDetailsAsync(addressLine);
        }
    }
}
