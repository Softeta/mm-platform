using EmailService.Send.Constants;
using EmailService.Send.SendInBlue;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmailService.Send
{
    public static class ServiceRegistrations
    {
        public static IServiceCollection AddSendInBlueClient(this IServiceCollection services)
        {
            services.AddSingleton<ISmtpProvider, SendInBlueClient>(sp =>
            {
                var config = sp.GetRequiredService<IConfiguration>();
                var apiKey = config[KeyVaultSecretNames.SendInBlueApiKey];
                return new SendInBlueClient(apiKey);
            });
            return services;
        }
    }
}
