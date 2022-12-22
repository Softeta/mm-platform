using API.Customization.Authentication;
using API.Customization.KeyVault;
using BackOffice.Shared.Constants;
using BackOffice.Shared.Queries;
using BackOffice.Users.CacheRefresher.BackOfficeUsersServices;
using BackOffice.Users.CacheRefresher.Commands;
using BackOffice.Users.CacheRefresher.Constants;
using BackOffice.Users.CacheRefresher.EventBus;
using EventBus;
using MediatR;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Customization.FileStorage;
using Persistence.Customization.Storages;

[assembly: FunctionsStartup(typeof(BackOffice.Users.CacheRefresher.Startup))]
namespace BackOffice.Users.CacheRefresher
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

            var publicStorageAccountConfigurations = GetPublicStorageAccountConfigurations(configurations);
            var storageAccountConfigurations = GetStorageAccountConfigurations(configurations);

            builder.Services.Configure<AppRegistrationSettings>(authConfigurations =>
            {
                authConfigurations ??= new AppRegistrationSettings();

                authConfigurations.TenantId = configurations[AppSettingNames.AuthorizationTenantId];
                authConfigurations.ClientId = configurations[AppSettingNames.AuthorizationClientId];
                authConfigurations.ClientSecret = configurations[GlobalKeyVaultSecretNames.BackOfficeAppRegistrationSecret]; 
            });

            builder.Services
                .Configure<PublicStorageAccountConfigurations>(s =>
                {
                    s.AccountName = publicStorageAccountConfigurations.AccountName;
                    s.AccountKey = publicStorageAccountConfigurations.AccountKey;
                })
                .AddMediatR(typeof(GetCachedBackOfficeUsersQuery).Assembly)
                .AddMediatR(typeof(BaseCommandHandler).Assembly)
                .AddWebTableServiceClient(storageAccountConfigurations.ConnectionString)
                .AddSingleton<IMsGraphServiceClient, MsGraphServiceClient>()
                .AddEventBus(configurations[KeyVaultSecretNames.ServiceBusConnectionString])
                .AddSingleton<IBackOfficeUserEventBusPublisher, BackOfficeUserEventBusPublisher>()
                .AddPublicFileClients();
        }

        private static PublicStorageAccountConfigurations GetPublicStorageAccountConfigurations(IConfiguration configurations)
        {
            return new PublicStorageAccountConfigurations
            {
                AccountKey = configurations[GlobalKeyVaultSecretNames.PublicStorageAccountKey],
                AccountName = configurations[AppSettingNames.PublicStorageAccountName]
            };
        }

        private static StorageAccountConfigurations GetStorageAccountConfigurations(IConfiguration configurations)
        {
            return new StorageAccountConfigurations
            {
                AccountKey = configurations[KeyVaultSecretNames.MmWebStorageAccountKey],
                AccountName = configurations[AppSettingNames.StorageAccountName]
            };
        }
    }
}
