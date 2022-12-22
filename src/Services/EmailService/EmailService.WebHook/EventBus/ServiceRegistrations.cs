using EmailService.WebHook.EventBus.Publishers;
using Microsoft.Extensions.DependencyInjection;

namespace EmailService.WebHook.EventBus
{
    public static class ServiceRegistrations
    {
        public static IServiceCollection AddEventBusPublishers(this IServiceCollection services)
        {
            services.AddSingleton<IEmailServiceWebHookEventBusPublisher, EmailServiceWebHookEventBusPublisher>();

            return services;
        }
    }
}
