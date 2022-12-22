using API.WebClients.Clients.DanishCompaniesService;
using Contracts.Company.Responses;
using Contracts.Shared;
using MediatR;
using System.Text;
using System.Text.RegularExpressions;

namespace Companies.Application.Queries
{
    public class GetCompaniesFromDanishRcQueryHandler
        : IRequestHandler<GetCompaniesFromDanishRcQuery, GetCompaniesSearchResponse>
    {
        private readonly IDasnishCvrWebApiClient _dasnishCvrWebApiClient;
        private readonly Regex _multiEmptySpacesRegex;
        private const string Denmark = "Denmark";

        public GetCompaniesFromDanishRcQueryHandler(IDasnishCvrWebApiClient dasnishCvrWebApiClient)
        {
            _dasnishCvrWebApiClient = dasnishCvrWebApiClient;
            _multiEmptySpacesRegex = new Regex("[ ]{2,}", RegexOptions.None);
        }

        public async Task<GetCompaniesSearchResponse> Handle(GetCompaniesFromDanishRcQuery request, CancellationToken cancellationToken)
        {
            var companies = new List<GetCompanySearchResponse>();

            if (string.IsNullOrWhiteSpace(request.Search))
            {
                return new GetCompaniesSearchResponse(0, companies, false);
            }

            var response = await _dasnishCvrWebApiClient.GetAsync(request.PageSize, request.Search!, null);

            if (response is null)
            {
                return new GetCompaniesSearchResponse(0, companies, false);
            }

            foreach (var hit in response.Hits.Hits)
            {
                var rcCompany = hit.Source.Company;

                if (string.IsNullOrWhiteSpace(rcCompany.CompanyMetadata?.CompanyName?.Name)) continue;
                if (string.IsNullOrWhiteSpace(rcCompany.CompanyMetadata.Address?.CountryCode)) continue;

                var company = new GetCompanySearchResponse
                {
                    RegistrationNumber = rcCompany.RegistrationNumber.ToString(),
                    Name = CleanEmptySpaces(rcCompany.CompanyMetadata.CompanyName.Name)!,
                    Address = new AddressWithLocation
                    {
                        AddressLine = CleanEmptySpaces(BuildAddressLine(rcCompany.CompanyMetadata.Address))!,
                        City = CleanEmptySpaces(rcCompany.CompanyMetadata.Address?.City),
                        Country = Denmark,
                        PostalCode = rcCompany.CompanyMetadata.Address?.ZipCode
                    }
                };

                companies.Add(company);
            }

            return new GetCompaniesSearchResponse(response.Hits.Total, companies, false);
        }

        private string? CleanEmptySpaces(string? input) =>
            !string.IsNullOrWhiteSpace(input)
                ? _multiEmptySpacesRegex.Replace(input, " ")
                : input;

        private static string BuildAddressLine(API.WebClients.Clients.DanishCompaniesService.Models.Address? address)
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
