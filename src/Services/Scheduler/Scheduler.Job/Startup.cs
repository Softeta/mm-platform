using API.Customization.KeyVault;
using EventBus;
using MediatR;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Scheduler.Job.Constants;
using Scheduler.Job.EventBus;
using Scheduler.Job.IntegrationEventHandlers;

[assembly: FunctionsStartup(typeof(Scheduler.Job.Startup))]
namespace Scheduler.Job
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

            builder.Services
                .AddEventBusPublishers()
                .AddEventBus(configurations[KeyVaultSecretNames.ServiceBusConnectionString])
                .AddMediatR(typeof(ScheduledIntegrationEventHandler));
        }
    }
}
