using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Customization.TableStorage.Clients;

namespace Persistence.Customization.Storages
{
    public static class ServiceRegistrationExtensions
    {
        public const string PublicStorageAccountKey = "public-storage-account-key";
        public const string PrivateStorageAccountKey = "private-storage-account-key";

        public static IServiceCollection AddPrivateTableServiceClient(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton<IPrivateTableServiceClient>(new TableServiceClientResolver(connectionString));
            return services;
        }

        public static IServiceCollection AddWebTableServiceClient(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton<IWebTableServiceClient>(new TableServiceClientResolver(connectionString));
            return services;
        }

        public static IServiceCollection BindPublicStorageAccount(
            this IServiceCollection services, 
            ConfigurationManager configurations,
            string sectionName)
        {
            return services.BindStorageAccount<PublicStorageAccountConfigurations>(configurations, sectionName, PublicStorageAccountKey);
        }

        public static IServiceCollection BindPrivateStorageAccount(
            this IServiceCollection services,
            ConfigurationManager configurations,
            string sectionName)
        {
            return services.BindStorageAccount<PrivateStorageAccountConfigurations>(configurations, sectionName, PrivateStorageAccountKey);
        }

        private static IServiceCollection BindStorageAccount<T>(
            this IServiceCollection services,
            ConfigurationManager configurations,
            string sectionName,
            string keyVaultSecretName) where T : StorageAccountConfigurations
        {
            services.AddOptions<T>().Configure(s =>
            {
                s.AccountKey = configurations[keyVaultSecretName];
            }).BindConfiguration(sectionName);

            return services;
        }

        public static T GetStorageAccountConfigurations<T>(
            this IConfiguration configurations,
            string sectionName,
            string keyVaultSecretName) where T : StorageAccountConfigurations
        {
            var configs = configurations.GetSection(sectionName).Get<T>();
            configs.AccountKey = configurations[keyVaultSecretName];

            return configs;
        }
    }
}
