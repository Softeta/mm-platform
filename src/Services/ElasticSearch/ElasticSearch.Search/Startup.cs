using API.Customization.KeyVault;
using ElasticSearch.Search;
using ElasticSearch.Search.Constants;
using ElasticSearch.Search.Models.Settings;
using ElasticSearch.Shared;
using ElasticSearch.Shared.Configurations;
using ElasticSearch.Shared.Constants;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]
namespace ElasticSearch.Search
{
    public class Startup : FunctionsStartup
    {
        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            builder.AddGlobalAzureKeyVault();
            base.ConfigureAppConfiguration(builder);
        }

        public override void Configure(IFunctionsHostBuilder builder)
        {
            var configurations = builder.GetContext().Configuration;
            var cognitiveSearchConfigurations = new CognitiveSearchConfigurations
            {
                Url = configurations[AppSettings.CognitiveSearchUrl],
                AdminKey = configurations[KeyVaultSecretNames.ElasticSearchAdminKey]
            };

            var candidatesIndex = configurations[AppSettings.CognitiveSearchCandidatesIndex];
            var jobsIndex = configurations[AppSettings.CognitiveSearchJobsIndex];

            builder.Services
                .Configure<FilterSettings>(configurations)
                .AddCandidatesSearchClient(cognitiveSearchConfigurations, candidatesIndex)
                .AddJobsSearchClient(cognitiveSearchConfigurations, jobsIndex);
        }
    }
}
