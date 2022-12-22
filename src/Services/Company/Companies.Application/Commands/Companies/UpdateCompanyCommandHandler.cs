using API.WebClients.Clients.HereSearch;
using API.WebClients.Clients.HereSearch.Models;
using Companies.Domain.Aggregates.CompanyAggregate;
using Companies.Domain.Aggregates.CompanyAggregate.Entities;
using Companies.Infrastructure.Persistence.Repositories;
using Contracts.Shared;
using Contracts.Shared.Requests;
using Domain.Seedwork.Enums;
using Domain.Seedwork.Exceptions;
using Domain.Seedwork.Shared.ValueObjects;
using MediatR;
using Persistence.Customization.FileStorage.Clients.Public;
using Persistence.Customization.Queries;
using Persistence.Customization.TableStorage;

namespace Companies.Application.Commands.Companies
{
    public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, Company>
    {
        private readonly IPublicFileDeleteClient _publicFileDeleteClient;
        private readonly ICompanyRepository _companyRepository;
        private readonly ILocationProvider _locationProvider;   
        private readonly IMediator _mediator;

        public UpdateCompanyCommandHandler(
            IPublicFileDeleteClient publicFileDeleteClient,
            ICompanyRepository companyRepository,
            ILocationProvider locationProvider,
            IMediator mediator)
        {
            _publicFileDeleteClient = publicFileDeleteClient;
            _companyRepository = companyRepository;
            _locationProvider = locationProvider;
            _mediator = mediator;
        }

        public async Task<Company> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetAsync(request.CompanyId);

            var addressDetailsTask = GetAddressDetailsAsync(request.Address);
            var deleteLogoTask = DeleteOldImageAsync(request.Logo.HasChanged, company.Logo, cancellationToken);
            var logoTask = GetImagesAsync(request.Logo);

            await Task.WhenAll(addressDetailsTask, deleteLogoTask, logoTask);

            var address = addressDetailsTask.Result;
            var logos = logoTask.Result;

            company.Update(
                request.WebsiteUrl,
                request.LinkedInUrl,
                request.GlassdoorUrl,
                address.AddressLine,
                address.City,
                address.Country,
                address.PostalCode,
                address.Longitude,
                address.Latitude,
                request.Industries.Select(s => new CompanyIndustry(s.Id, request.CompanyId, s.Code)),
                logos,
                request.Logo.HasChanged,
                request.Logo.CacheId);

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

            var addressWithPostalCode = $"{address.AddressLine}, {address.PostalCode}";
            var addressDetails = await _locationProvider.GetAddressDetailsAsync(addressWithPostalCode);

            if (addressDetails.PostalCode != address.PostalCode)
            {
                throw new BadRequestException($"Invalid postalCode. Postal code by address ({address.AddressLine}): {addressDetails.PostalCode} Requested postal code: {address.PostalCode}",
                    ErrorCodes.BadRequest.InvalidPostalCode);
            }

            return addressDetails;
        }

        private async Task DeleteOldImageAsync(bool hasChanged, Image? oldImage, CancellationToken token)
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

        private async Task<Dictionary<ImageType, string?>?> GetImagesAsync(UpdateFileCacheRequest image)
        {
            if (image.HasChanged)
            {
                var command = new GetImageQuery(image.CacheId,
                    FileCacheTableStorage.Company.FilePartitionKey);
                return await _mediator.Send(command);
            }
            return null;
        }
    }
}