using API.WebClients.Clients.HereSearch;
using API.WebClients.Clients.HereSearch.Models;
using Contracts.Shared;
using Jobs.Domain.Aggregates.JobAggregate;
using Jobs.Infrastructure.Persistence.Repositories;
using MediatR;
using CompanyJobDomain = Jobs.Domain.Aggregates.JobAggregate.ValueObjects.Company;
using JobContactPersonDomain = Jobs.Domain.Aggregates.JobAggregate.Entities.JobContactPerson;

namespace Jobs.Application.Commands
{
    public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, CompanyJobDomain>
    {
        private readonly IJobRepository _jobRepository;
        private readonly ILocationProvider _locationProvider;

        public UpdateCompanyCommandHandler(IJobRepository jobRepository, ILocationProvider locationProvider)
        {
            _jobRepository = jobRepository;
            _locationProvider = locationProvider;
        }

        public async Task<CompanyJobDomain> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            var job = await _jobRepository.GetAsync(request.JobId);

            var addressDetails = await GetAddresDetailsAsync(request.Address);

            var contactPersons = BuildJobContactPersons(job, request);

            job.UpdateCompany(
                new CompanyJobDomain(
                job.Company.Id,
                job.Company.Status,
                job.Company.Name,
                request.Address?.AddressLine,
                addressDetails?.City,
                addressDetails?.Country,
                addressDetails?.PostalCode,
                addressDetails?.Longitude,
                addressDetails?.Latitude,
                request.Description,
                job.Company.LogoUri,
                contactPersons));

            _jobRepository.Update(job);
            await _jobRepository.UnitOfWork.SaveEntitiesAsync<Job>(cancellationToken);

            return job.Company;
        }

        private List<JobContactPersonDomain> BuildJobContactPersons(Job job, UpdateCompanyCommand request)
        {
            var currentContactPersons = job.Company.ContactPersons.ToList();
            currentContactPersons.RemoveAll(x => request.ContactPersonsToRemove.Contains(x.PersonId));

            var contactPersons = new List<JobContactPersonDomain>();

            contactPersons.AddRange(currentContactPersons.Select(p => new JobContactPersonDomain(
                 p.Id,
                 p.PersonId,
                 p.PersonId == request.MainContactId,
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
                 p.ExternalId)));

            contactPersons.AddRange(request.ContactPersonsToAdd.Select(p => new JobContactPersonDomain(
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
                    p.ExternalId)));

            return contactPersons;
        }

        private async Task<AddressDetails?> GetAddresDetailsAsync(Address? requestAddress)
        {
            if (requestAddress is null) return null;

            var areAllFieldsFilledIn = 
                requestAddress.Latitude.HasValue &&
                requestAddress.Longitude.HasValue &&
                !string.IsNullOrWhiteSpace(requestAddress.City) &&
                !string.IsNullOrWhiteSpace(requestAddress.Country);

            if (areAllFieldsFilledIn)
            {
                return new AddressDetails
                {
                    AddressLine = requestAddress.AddressLine,
                    City = requestAddress.City!,
                    Country = requestAddress.Country!,
                    PostalCode = requestAddress.PostalCode,
                    Longitude = requestAddress.Longitude!.Value,
                    Latitude = requestAddress.Latitude!.Value
                };
            }

            return await _locationProvider.GetAddressDetailsAsync(requestAddress.AddressLine);
        }
    }
}
