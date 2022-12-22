using Candidates.Infrastructure.Clients.Talogy.Configurations;
using Candidates.Infrastructure.Clients.Talogy.Constants;
using Candidates.Infrastructure.Clients.Talogy.Models.Token;
using Domain.Seedwork.Exceptions;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Candidates.Infrastructure.Clients.Talogy.Authorization
{
    internal class TalogyAuthApiClient : ITalogyAuthApiClient
    {
        private readonly TalogyConfigurations _configurations;
        private readonly IHttpClientFactory _httpClientFactory;

        public TalogyAuthApiClient(
            IHttpClientFactory httpClientFactory,
            IOptions<TalogyConfigurations> configurations)
        {
            _httpClientFactory = httpClientFactory;
            _configurations = configurations.Value;
        }

        public async Task<AuthenticationResult?> AuthorizeAsync()
        {
            var httpClient = _httpClientFactory.CreateClient(ApiClients.TalogyAuthApiClientName);

            var message = new HttpRequestMessage(HttpMethod.Post, string.Empty)
            {
                Content = new FormUrlEncodedContent(
                    new Dictionary<string, string> {
                                { "client_id", _configurations.ClientId },
                                { "client_secret", _configurations.ClientSecret },
                                { "grant_type", _configurations.GrantType }
                    })
            };

            var response = await httpClient.SendAsync(message);
            return await GetResponseAsync(response);
        }

        private static async Task<AuthenticationResult?> GetResponseAsync(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<AuthenticationResult>(jsonResponse);
                result!.CalculateExpirationDate();

                return result;
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            throw new HttpException(responseContent, response.StatusCode, ErrorCodes.HttpRequest.TalogyAuthApiException);
        }
    }
}
