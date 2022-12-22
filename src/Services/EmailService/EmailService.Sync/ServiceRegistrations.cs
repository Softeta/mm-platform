using EmailService.Sync.Constants;
using EmailService.Sync.SendInBlue;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmailService.Sync
{
    public static class ServiceRegistrations
    {
        public static IServiceCollection AddSendInBlueClient(this IServiceCollection services)
        {
            services.AddSingleton<ICandidateContactsClient, SendInBlueClient>(sp =>
            {
                var config = sp.GetRequiredService<IConfiguration>();
                var apiKey = config[KeyVaultSecretNames.SendInBlueApiKey];
                return new SendInBlueClient(apiKey);
            });
            services.AddSingleton<IContactPersonContactsClient, SendInBlueClient>(sp =>
            {
                var config = sp.GetRequiredService<IConfiguration>();
                var apiKey = config[KeyVaultSecretNames.SendInBlueApiKey];
                return new SendInBlueClient(apiKey);
            });
            return services;
        }
    }
}
