using Candidates.Infrastructure.Clients.Talogy.Authorization;
using Candidates.Infrastructure.Clients.Talogy.Constants;
using Domain.Seedwork.Exceptions;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Candidates.Infrastructure.Clients.Talogy
{
    internal class TalogyApiClient : ITalogyApiClient
    {
        private readonly IAuthorizationManager _authorizationManager;
        private readonly IHttpClientFactory _httpClientFactory;

        public TalogyApiClient(
            IHttpClientFactory httpClientFactory,
            IAuthorizationManager authorizationManager)
        {
            _httpClientFactory = httpClientFactory;
            _authorizationManager = authorizationManager;
        }

        public async Task<TOut?> GetAsync<TOut>(string endpoint)
        {
            var httpClient = await CreateHttpClientAsync();
            var response = await httpClient.GetAsync(endpoint);

            return await GetResponse<TOut>(response);
        }

        public async Task<Stream?> GetFileAsync(string endpoint, string accept)
        {
            var httpClient = await CreateHttpClientAsync();

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{httpClient.BaseAddress}/{endpoint}"),
            };

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(accept));

            var response = await httpClient.SendAsync(request);

            return await GetFileResponseAsync(response);
        }

        public async Task<TOut?> PostAsync<TIn, TOut>(TIn payload, string endpoint) where TIn : class
        {
            var httpClient = await CreateHttpClientAsync();

            var response = await httpClient.PostAsJsonAsync(endpoint, payload);

            return await GetResponse<TOut>(response);
        }

        public async Task DeleteAsync(string endpoint)
        {
            var httpClient = await CreateHttpClientAsync();
            var response = await httpClient.DeleteAsync(endpoint);

            await GetResponse<bool>(response);
        }

        private static async Task<TOut?> GetResponse<TOut>(HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return default;
            }

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TOut>(jsonResponse);
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            throw new HttpException(responseContent, response.StatusCode, ErrorCodes.HttpRequest.TalogyApiException);
        }

        private static async Task<Stream?> GetFileResponseAsync(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStreamAsync();
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            throw new HttpException(responseContent, response.StatusCode, ErrorCodes.HttpRequest.TalogyApiException);
        }

        private async Task<HttpClient> CreateHttpClientAsync()
        {
            var accessToken = await _authorizationManager.GetAccessTokenAsync();

            var client = _httpClientFactory.CreateClient(ApiClients.TalogApiClientName);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            return client;
        }
    }
}
