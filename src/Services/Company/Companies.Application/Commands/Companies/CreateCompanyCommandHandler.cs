using API.WebClients.Clients.HereSearch;
using API.WebClients.Clients.HereSearch.Models;
using Companies.Application.Commands.Validations;
using Companies.Domain.Aggregates.CompanyAggregate;
using Companies.Domain.Aggregates.CompanyAggregate.Entities;
using Companies.Infrastructure.Persistence.Repositories;
using Contracts.Shared;
using Domain.Seedwork.Enums;
using Domain.Seedwork.Exceptions;
using MediatR;
using Persistence.Customization.Queries;
using Persistence.Customization.TableStorage;

namespace Companies.Application.Commands.Companies
{
    public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, Company>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly ILocationProvider _locationProvider;
        private readonly IMediator _mediator;

        public CreateCompanyCommandHandler(
            ICompanyRepository companyRepository,
            ILocationProvider locationProvider,
            IMediator mediator)
        {
            _companyRepository = companyRepository;
            _locationProvider = locationProvider;
            _mediator = mediator;          
        }

        public async Task<Company> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            await _mediator.Publish(new ValidateContactPersonDuplicationByEmailValidation(request.PersonEmail));

            var company = new Company();

            var addressDetailsTask = GetAddressDetailsAsync(request.Address);

            var fileCacheIds = new List<Guid>();
            var personPictureTask = GetImageAsync(request.PersonPicture?.CacheId, fileCacheIds);
            var logoTask = GetImageAsync(request.Logo?.CacheId, fileCacheIds);

            await Task.WhenAll(addressDetailsTask, personPictureTask, logoTask);
            var addressDetails = addressDetailsTask.Result;

            company.Create(
                request.Name,
                request.RegistrationNumber,
                logoTask.Result,
                request.WebsiteUrl,
                request.LinkedInUrl,
                request.GlassdoorUrl,
                addressDetails.AddressLine,
                addressDetails.City,
                addressDetails.Country,
                addressDetails.PostalCode,
                addressDetails.Longitude,
                addressDetails.Latitude,
                request.PersonEmail,
                request.PersonFirstName,
                request.PersonLastName,
                request.PersonPhoneCountryCode,
                request.PersonPhoneNumber,
                request.PersonPosition?.Id,
                request.PersonPosition?.Code,
                request.PersonPosition?.AliasTo?.Id,
                request.PersonPosition?.AliasTo?.Code,
                personPictureTask.Result,
                request.PersonPicture?.CacheId,
                request.Industries.Select(s => new CompanyIndustry(s.Id, company.Id, s.Code)),
                fileCacheIds,
                request.CreatedById,
                request.CreatedByScope);

            _companyRepository.Add(company);
            await _companyRepository.UnitOfWork.SaveEntitiesAsync<Company>(cancellationToken);

            return company;
        }

        private async Task<AddressDetails> GetAddressDetailsAsync(AddressWithLocation address)
        {
            if (string.IsNullOrWhiteSpace(address.AddressLine))
            {
                throw new BadRequestException("AddressLine is required",
                    ErrorCodes.BadRequest.AddressLineRequired);
            }
            if (string.IsNullOrWhiteSpace(address.PostalCode))
            {
                throw new BadRequestException("PostalCode is required",
                    ErrorCodes.BadRequest.PostalCodeRequired);
            }

            var addressWithPostalCode = $"{address.AddressLine}, {address.PostalCode}";
            var addressDetails = await _locationProvider.GetAddressDetailsAsync(addressWithPostalCode);

            if (addressDetails.PostalCode != address.PostalCode)
            {
                throw new BadRequestException($"Invalid postalCode. Postal code by address ({address.AddressLine}): {addressDetails.PostalCode} Requested postal code: {address.PostalCode}",
                    ErrorCodes.BadRequest.InvalidPostalCode);
            }

            return addressDetails;
        }

        private async Task<Dictionary<ImageType, string?>?> GetImageAsync(Guid? cacheId, List<Guid> fileCacheIds)
        {
            var getImageCommand = new GetImageQuery(cacheId,
                FileCacheTableStorage.Company.FilePartitionKey);
            var imagePaths = await _mediator.Send(getImageCommand);

            if (cacheId.HasValue)
            {
                fileCacheIds.Add(cacheId.Value);
            }
            return imagePaths;
        }
    }
}
