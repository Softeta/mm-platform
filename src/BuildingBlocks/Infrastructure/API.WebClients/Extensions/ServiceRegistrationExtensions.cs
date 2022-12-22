using API.WebClients.Clients;
using API.WebClients.Clients.DanishCompaniesService;
using API.WebClients.Clients.DanishCompaniesService.Configurations;
using API.WebClients.Clients.HereSearch;
using API.WebClients.Constants;
using API.WebClients.HttpMessageHandlers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using System.Text;
using API.WebClients.Clients.FormRecognizer;
using API.WebClients.Clients.TagSystem;
using Azure;

namespace API.WebClients.Extensions
{
    public static class ServiceRegistrationExtensions
    {
        public static void AddJobServiceWebApiClient(this IServiceCollection services, string url)
        {
            services.AddHttpClient(ApiClients.JobServiceClient, url);
            services.AddSingleton<IJobServiceWebApiClient, WebApiClient>(sp =>
                new WebApiClient(
                    sp.GetRequiredService<IHttpClientFactory>(),
                    ApiClients.JobServiceClient));
        }

        public static void AddCompanyServiceWebApiClient(this IServiceCollection services, string url)
        {
            services.AddHttpClient(ApiClients.CompanyServiceClient, url);
            services.AddSingleton<ICompanyServiceWebApiClient, WebApiClient>(sp =>
                new WebApiClient(
                    sp.GetRequiredService<IHttpClientFactory>(),
                    ApiClients.CompanyServiceClient));
        }

        public static void AddCandidateServiceWebApiClient(this IServiceCollection services, string url)
        {
            services.AddHttpClient(ApiClients.CandidateServiceClient, url);
            services.AddSingleton<ICandidateWebApiClient, WebApiClient>(sp =>
                new WebApiClient(
                    sp.GetRequiredService<IHttpClientFactory>(),
                    ApiClients.CandidateServiceClient));
        }

        public static void AddElasticSearchWebApiClient(this IServiceCollection services, string url)
        {
            services.AddHttpClient(ApiClients.ElasticSearchClient, url);
            services.AddSingleton<IElasticSearchWebApiClient, WebApiClient>(sp =>
                new WebApiClient(
                    sp.GetRequiredService<IHttpClientFactory>(),
                    ApiClients.ElasticSearchClient));
        }

        public static IServiceCollection AddHereSearchWebApiClient(this IServiceCollection services, string url)
        {
            services.AddHttpClient(ApiClients.HereSearchClient, client =>
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });

            services.AddSingleton<ILocationProvider, HereSearchWebApiClient>();

            return services;
        }

        public static IServiceCollection AddDanishRegistryCenterWebApiClient(
            this IServiceCollection services,
            ConfigurationManager configuration)
        {
            var options = configuration.GetDanishCrvApiOptions();
            var password = configuration[DanishCrvKeyVaultSecretNames.DanishCrvApiUserPassword];

            var base64Token = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"{options.UserId}:{password}"));
            services.AddHttpClient(ApiClients.DanishRegistryCenterClient, client =>
            {
                client.BaseAddress = new Uri(options.Uri);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });

            services.AddSingleton<IDasnishCvrWebApiClient, DasnishCvrWebApiClient>();

            return services;
        }

        public static IServiceCollection AddFormRecognizer(this IServiceCollection services)
        {
            services.AddSingleton<IFormRecognizerApiClient, FormRecognizerApiClient>();

            return services;
        }

        public static void AddAdministrationSettingsServiceWebApiClient(this IServiceCollection services, string url)
        {
            services.AddHttpClient(ApiClients.AdministrationSettingsServiceClient, url);
            services.AddSingleton<IAdministrationSettingsClient, WebApiClient>(sp =>
                new WebApiClient(
                    sp.GetRequiredService<IHttpClientFactory>(),
                    ApiClients.AdministrationSettingsServiceClient));
        }

        public static IServiceCollection AddTagSystemWebApiClient(this IServiceCollection services, string url)
        {
            services.AddHttpClient(ApiClients.TagSystemClient, client =>
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });

            services.AddSingleton<ITagSystemApiClient, TagSystemApiClient>();

            return services;
        }

        private static void AddHttpClient(this IServiceCollection services, string serviceClient, string url)
        {
            services.AddHttpClient(serviceClient, client =>
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            })
                .AddHttpMessageHandler<SetBearerTokenHandler>();
        }
    }
}
