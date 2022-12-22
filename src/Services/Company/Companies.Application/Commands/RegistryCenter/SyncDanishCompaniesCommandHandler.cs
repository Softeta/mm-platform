using API.WebClients.Clients.DanishCompaniesService;
using API.WebClients.Clients.DanishCompaniesService.Models;
using Companies.Domain.ReadModels;
using Companies.Infrastructure.Persistence.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.RegularExpressions;

namespace Companies.Application.Commands.RegistryCenter
{
    public class SyncDanishCompaniesCommandHandler : INotificationHandler<SyncDanishCompaniesCommand>
    {
        private readonly IRcCompanyRepository _rcCompanyRepository;
        private readonly IDasnishCvrWebApiClient _dasnishCvrWebApiClient;
        private readonly ILogger<SyncDanishCompaniesCommandHandler> _logger;
        private readonly Regex _regex;
        private const string Denmark = "Denmark";

        public SyncDanishCompaniesCommandHandler(
            IRcCompanyRepository rcCompanyRepository,
            IDasnishCvrWebApiClient dasnishCvrWebApiClient,
            ILogger<SyncDanishCompaniesCommandHandler> logger)
        {
            _rcCompanyRepository = rcCompanyRepository;
            _dasnishCvrWebApiClient = dasnishCvrWebApiClient;
            _logger = logger;
            _regex = new Regex("[ ]{2,}", RegexOptions.None);
        }

        public async Task Handle(SyncDanishCompaniesCommand request, CancellationToken cancellationToken)
        {
            string[]? searchAfter = null;
            var pageSize = 1000;

            while (true)
            {
                searchAfter = await SyncOnePageAsync(pageSize, searchAfter, cancellationToken);

                if (searchAfter is null) return;
            }
        }

        private async Task<string[]?> SyncOnePageAsync(int pageSize, string[]? searchAfter, CancellationToken cancellationToken)
        {
            var companies = new List<RegistryCenterCompany>();

            var response = await _dasnishCvrWebApiClient.GetAsync(pageSize, string.Empty, searchAfter);

            if (response is null) return null;

            try
            {
                foreach (var hit in response.Hits.Hits)
                {
                    var rcCompany = hit.Source.Company;

                    if (string.IsNullOrWhiteSpace(rcCompany.CompanyMetadata?.CompanyName?.Name)) continue;
                    if (string.IsNullOrWhiteSpace(rcCompany.CompanyMetadata.Address?.CountryCode)) continue;

                    var company = new RegistryCenterCompany
                    {
                        RegistrationNumber = rcCompany.RegistrationNumber.ToString(),
                        Name = CleanEmptySpaces(rcCompany.CompanyMetadata.CompanyName.Name)!,
                        Country = Denmark,
                        CountryCode = rcCompany.CompanyMetadata.Address?.CountryCode!,
                        Region = CleanEmptySpaces(rcCompany.CompanyMetadata.Address?.Region?.Name),
                        City = CleanEmptySpaces(rcCompany.CompanyMetadata.Address?.City),
                        AddressLine = CleanEmptySpaces(BuildAddressLine(rcCompany.CompanyMetadata.Address))!,
                        ZipCode = rcCompany.CompanyMetadata.Address?.ZipCode
                    };

                    company.BuildIndexes();

                    companies.Add(company);
                }

                await _rcCompanyRepository.AddOrUpdateRange(companies);
                await _rcCompanyRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to store companies from registry center");
            }

            return response.Hits.Hits.LastOrDefault()?.Sort;
        }

        private string? CleanEmptySpaces(string? input) => 
            !string.IsNullOrWhiteSpace(input) 
                ? _regex.Replace(input, " ") 
                : input;

        private static string BuildAddressLine(Address? address)
        {
            var addressBuilder = new StringBuilder();

            if (string.IsNullOrEmpty(address?.StreetName))
            {
                return Denmark;
            }

            addressBuilder.Append(address!.StreetName);

            if (string.IsNullOrEmpty(address!.HouseNumber))
            {
                return addressBuilder.ToString();
            }

            addressBuilder.Append($" {address!.HouseNumber}");

            if (string.IsNullOrEmpty(address!.DoorNumber))
            {
                return addressBuilder.ToString();
            }

            addressBuilder.Append($"-{address!.DoorNumber}");

            return addressBuilder.ToString();
        }
    }
}
