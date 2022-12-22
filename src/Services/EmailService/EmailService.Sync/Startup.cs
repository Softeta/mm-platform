using API.Customization.KeyVault;
using Azure.Messaging.ServiceBus.Administration;
using EmailService.Sync.Constants;
using EventBus.Constants;
using EventBus.Filters;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using System.Threading.Tasks;

[assembly: FunctionsStartup(typeof(EmailService.Sync.Startup))]
namespace EmailService.Sync
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
            var createFilterRulesTask = CreateFilterRulesAsync(configurations[KeyVaultSecretNames.ServiceBusConnectionString]);

            builder.Services
                .AddSendInBlueClient();

            Task.WaitAll(
                createFilterRulesTask
            );
        }

        private static async Task CreateFilterRulesAsync(string serviceBusConnectionString)
        {
            var adminClient = new ServiceBusAdministrationClient(serviceBusConnectionString);
            var serviceBusFilterManager = new ServiceBusFiltersManager(adminClient);

            await Task.WhenAll(
                serviceBusFilterManager.CreateFiltersAsync(
                        Topics.CandidateChanged.Name,
                        Topics.CandidateChanged.Subscribers.EmailServiceSyncFilters.subscriptionName,
                        Topics.CandidateChanged.Subscribers.EmailServiceSyncFilters.filterNames),
                serviceBusFilterManager.CreateFiltersAsync(
                        Topics.ContactPersonChanged.Name,
                        Topics.ContactPersonChanged.Subscribers.EmailServiceSyncFilters.subscriptionName,
                        Topics.ContactPersonChanged.Subscribers.EmailServiceSyncFilters.filterNames)
            );
        }
    }
}
