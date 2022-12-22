using ElasticSearch.Shared.Clients;
using ElasticSearch.Shared.Configurations;
using Microsoft.Extensions.DependencyInjection;

namespace ElasticSearch.Shared
{
    public static class ServiceRegistrations
    {
        public static IServiceCollection AddCandidatesSearchClient(
            this IServiceCollection services, 
            CognitiveSearchConfigurations configurations,
            string index)
        {
            services.AddSingleton<ICandidatesSearchClient>(
                new DocumentsServiceClient(
                    configurations.Url,
                    configurations.AdminKey,
                    index));

            return services;
        }

        public static IServiceCollection AddJobsSearchClient(
            this IServiceCollection services, 
            CognitiveSearchConfigurations configurations,
            string index)
        {
            services.AddSingleton<IJobsSearchClient>(
                new DocumentsServiceClient(
                    configurations.Url,
                    configurations.AdminKey,
                    index));

            return services;
        }
    }
}
