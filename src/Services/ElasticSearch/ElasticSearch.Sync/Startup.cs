using API.Customization.KeyVault;
using Azure.Messaging.ServiceBus.Administration;
using ElasticSearch.Shared;
using ElasticSearch.Shared.Configurations;
using ElasticSearch.Shared.Constants;
using ElasticSearch.Sync;
using ElasticSearch.Sync.Commands;
using ElasticSearch.Sync.Constants;
using EventBus.Constants;
using EventBus.Filters;
using MediatR;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using System.Threading.Tasks;

[assembly: FunctionsStartup(typeof(Startup))]
namespace ElasticSearch.Sync
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
                .AddMediatR(typeof(SyncCandidateDocumentCommand))
                .AddCandidatesSearchClient(cognitiveSearchConfigurations, candidatesIndex)
                .AddJobsSearchClient(cognitiveSearchConfigurations, jobsIndex);

            CreateFilterRulesAsync(configurations[KeyVaultSecretNames.ServiceBusConnectionString]).Wait();
        }

        private static async Task CreateFilterRulesAsync(string serviceBusConnectionString)
        {
            var adminClient = new ServiceBusAdministrationClient(serviceBusConnectionString);
            var serviceBusFilterManager = new ServiceBusFiltersManager(adminClient);

            await Task.WhenAll(
                serviceBusFilterManager.CreateFiltersAsync(
                    Topics.CandidateChanged.Name,
                    Topics.CandidateChanged.Subscribers.ElasticSearchFilters.subscriptionName,
                    Topics.CandidateChanged.Subscribers.ElasticSearchFilters.filterNames),
                serviceBusFilterManager.CreateFiltersAsync(
                    Topics.CandidateJobsChanged.Name,
                    Topics.CandidateJobsChanged.Subscribers.ElasticSearchFilters.subscriptionName,
                    Topics.CandidateJobsChanged.Subscribers.ElasticSearchFilters.filterNames),
                serviceBusFilterManager.CreateFiltersAsync(
                    Topics.JobChanged.Name,
                    Topics.JobChanged.Subscribers.ElasticSearchFilters.subscriptionName,
                    Topics.JobChanged.Subscribers.ElasticSearchFilters.filterNames),
                serviceBusFilterManager.CreateFiltersAsync(
                    Topics.JobCandidatesChanged.Name,
                    Topics.JobCandidatesChanged.Subscribers.ElasticSearchFilters.subscriptionName,
                    Topics.JobCandidatesChanged.Subscribers.ElasticSearchFilters.filterNames)
            );
        }
    }
}
