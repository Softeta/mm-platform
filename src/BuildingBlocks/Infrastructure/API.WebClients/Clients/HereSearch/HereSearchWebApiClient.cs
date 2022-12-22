using API.WebClients.Clients.HereSearch.Configurations;
using API.WebClients.Clients.HereSearch.Models;
using API.WebClients.Constants;
using Domain.Seedwork.Exceptions;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace API.WebClients.Clients.HereSearch
{
    public class HereSearchWebApiClient : ILocationProvider
    {
        private readonly string _apiKey;
        private readonly IHttpClientFactory _httpClientFactory;

        public HereSearchWebApiClient(IHttpClientFactory httpClientFactory, IConfiguration configurations)
        {
            _httpClientFactory = httpClientFactory;
            _apiKey = configurations[HereSearchKeyVaultSecretNames.HereSearchApiKey];
        }

        public async Task<AddressDetails> GetAddressDetailsAsync(string address)
        {
            var client = _httpClientFactory.CreateClient(ApiClients.HereSearchClient);

            var uri = BuildUrlWithParams(address, client.BaseAddress!);

            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var hereGeoCodeItems = JsonConvert.DeserializeObject<HereSearchGeoCodeItemsResponse>(
                    await response.Content.ReadAsStringAsync()
                );

                return SelectAddressDetails(hereGeoCodeItems, address);
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            throw new HttpException(
                $"Here search API failed to response status code: {response.StatusCode}. Response content: {responseContent}",
                response.StatusCode,
                ErrorCodes.HttpRequest.HereApiException);
        }

        private Uri BuildUrlWithParams(string address, Uri baseUrl)
        {
            var queryParams = new Dictionary<string, string>() {
                { "apiKey", _apiKey },
                { "q", address },
                { "lang", "en" } 
            };

            return new Uri(QueryHelpers.AddQueryString(baseUrl.ToString(), queryParams!));
        }

        private static AddressDetails SelectAddressDetails(HereSearchGeoCodeItemsResponse? hereGeoCodeItems, string address)
        {
            if (hereGeoCodeItems is null || 
                hereGeoCodeItems.Items is null || 
                hereGeoCodeItems.Items.Count == 0)
            {
                throw new BadRequestException($"Here API cannot find suitable address details for address: {address}",
                    ErrorCodes.BadRequest.SuitableAddressDetailsNotFound);
            }

            var hereSearchGeoCode = hereGeoCodeItems.Items
                .OrderByDescending(x => x.Scoring.QueryScore)
                .First();

            if (hereSearchGeoCode.Address.City is null)
            {
                throw new BadRequestException($"Here API cannot find suitable city for address: {address}",
                    ErrorCodes.BadRequest.SuitableCityNotFound);
            }

            return new AddressDetails
            {
                AddressLine = hereSearchGeoCode.Address.Label,
                City = hereSearchGeoCode.Address.City,
                Country = hereSearchGeoCode.Address.CountryName,
                PostalCode = hereSearchGeoCode.Address.PostalCode,
                Longitude = hereSearchGeoCode.Position.Longitude,
                Latitude = hereSearchGeoCode.Position.Latitude
            };
        }
    }
}
