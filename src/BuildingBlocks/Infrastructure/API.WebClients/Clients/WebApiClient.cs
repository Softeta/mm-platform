using Domain.Seedwork.Exceptions;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace API.WebClients.Clients
{
    public class WebApiClient : 
        IJobServiceWebApiClient, 
        ICompanyServiceWebApiClient,
        ICandidateWebApiClient,
        IElasticSearchWebApiClient,
        IAdministrationSettingsClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiClientName;

        public WebApiClient(IHttpClientFactory httpClientFactory, string apiClientName)
        {
            _httpClientFactory = httpClientFactory;
            _apiClientName = apiClientName;
        }

        public async Task<TOut?> GetAsync<TOut>(string endpoint)
        {
            var httpClient = _httpClientFactory.CreateClient(_apiClientName);

            var response = await httpClient.GetAsync(endpoint);

            return await GetResponse<TOut>(response);
        }

        public async Task<TOut?> PutAsync<TIn, TOut>(TIn payload, string endpoint) where TIn : class
        {
            var httpClient = _httpClientFactory.CreateClient(_apiClientName);

            var response = await httpClient.PutAsJsonAsync(endpoint, payload);

            return await GetResponse<TOut>(response);
        }

        public async Task<TOut?> PutAsync<TOut>(string endpoint)
        {
            var httpClient = _httpClientFactory.CreateClient(_apiClientName);

            var response = await httpClient.PutAsJsonAsync(endpoint, new {});

            return await GetResponse<TOut>(response);
        }

        public async Task<TOut?> PostAsync<TIn, TOut>(TIn payload, string endpoint) where TIn : class
        {
            var httpClient = _httpClientFactory.CreateClient(_apiClientName);

            var response = await httpClient.PostAsJsonAsync(endpoint, payload);

            return await GetResponse<TOut>(response);
        }

        public async Task<TOut?> DeleteAsync<TOut>(string endpoint)
        {
            var httpClient = _httpClientFactory.CreateClient(_apiClientName);

            var response = await httpClient.DeleteAsync(endpoint);

            return await GetResponse<TOut>(response);
        }

        private static async Task<TOut?> GetResponse<TOut>(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TOut>(jsonResponse);
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            throw new HttpException(responseContent, response.StatusCode, ErrorCodes.HttpRequest.LocalApiException);
        }
    }
}
