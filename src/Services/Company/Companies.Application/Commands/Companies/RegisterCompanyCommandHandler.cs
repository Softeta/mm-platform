using API.WebClients.Clients.HereSearch;
using API.WebClients.Clients.HereSearch.Models;
using Companies.Application.Queries;
using Companies.Domain.Aggregates.CompanyAggregate;
using Companies.Domain.Aggregates.CompanyAggregate.Entities;
using Companies.Infrastructure.Persistence.Repositories;
using Contracts.Shared;
using Domain.Seedwork.Exceptions;
using MediatR;

namespace Companies.Application.Commands.Companies
{
    public class RegisterCompanyCommandHandler : IRequestHandler<RegisterCompanyCommand, Company>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly ILocationProvider _locationProvider;
        private readonly IMediator _mediator;

        public RegisterCompanyCommandHandler(
            ICompanyRepository companyRepository,
            ILocationProvider locationProvider,
            IMediator mediator)
        {
            _companyRepository = companyRepository;
            _locationProvider = locationProvider;
            _mediator = mediator;          
        }

        public async Task<Company> Handle(RegisterCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = await _mediator.Send(
                new GetCompanyByContactPersonEmailQuery(request.PersonEmail),
                cancellationToken);

            if (company is null)
            {
                throw new BadRequestException($"Company not found by contact person email address {request.PersonEmail}",
                    ErrorCodes.Company.CompanyNotFoundByEmail, new[] { request.PersonEmail });
            }

            var addressDetails = await GetAddressDetailsAsync(request.Address);

            company.Register(
                request.Name,
                request.RegistrationNumber,
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
                request.Industries.Select(s => new CompanyIndustry(s.Id, company.Id, s.Code)));

            _companyRepository.Update(company);
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
    }
}
