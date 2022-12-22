using API.WebClients.Extensions;
using API.WebClients.HttpMessageHandlers;

namespace FrontOffice.Bff.API.Infrastructure.Extensions
{
    internal static class ServiceRegistrationExtensions
    {
        public static void AddServiceClients(this IServiceCollection services, Services serviceSettings)
        {
            services.AddJobServiceWebApiClient(serviceSettings.JobsApiUrl);
            services.AddCompanyServiceWebApiClient(serviceSettings.CompaniesApiUrl);
            services.AddCandidateServiceWebApiClient(serviceSettings.CandidatesApiUrl);
            services.AddElasticSearchWebApiClient(serviceSettings.ElasticSearchApiUrl);
            services.AddHttpContextAccessor();
            services.AddTransient<SetBearerTokenHandler>();
        }
    }
}
