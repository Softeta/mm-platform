using API.Customization.KeyVault;
using EmailService.WebHook.Constants;
using EmailService.WebHook.EventBus;
using EmailService.WebHook.IntegrationEventHandlers.Publishers;
using EventBus;
using MediatR;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Persistence.Customization.Storages;
using Persistence.Customization.TableStorage;
using Persistence.Customization.TableStorage.Helpers;

[assembly: FunctionsStartup(typeof(EmailService.WebHook.Startup))]
namespace EmailService.WebHook
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

            var storageAccountConfigurations = new StorageAccountConfigurations
            {
                AccountName = configurations[AppSettings.StorageAccountName],
                AccountKey = configurations[GlobalKeyVaultSecretNames.PrivateStorageAccountKey]
            };

            builder.Services
                .AddPrivateTableServiceClient(storageAccountConfigurations.ConnectionString)
                .AddEventBusPublishers()
                .AddEventBus(configurations[KeyVaultSecretNames.ServiceBusConnectionString])
                .AddMediatR(typeof(EmailServiceWebHookedIntegrationEventHandler));

            StorageTableHelper.CreateIfNotExistAsync(
                storageAccountConfigurations.ConnectionString, 
                EmailMessageTableStorageConstants.TableName)
                .Wait();
        }
    }
}
