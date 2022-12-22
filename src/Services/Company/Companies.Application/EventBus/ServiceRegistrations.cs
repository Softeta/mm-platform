using Companies.Application.EventBus.Publishers;
using Microsoft.Extensions.DependencyInjection;

namespace Companies.Application.EventBus
{
    public static class ServiceRegistrations
    {
        public static IServiceCollection AddEventBusPublishers(this IServiceCollection services)
        {
            services.AddSingleton<ICompanyEventBusPublisher, CompanyEventBusPublisher>();
            services.AddSingleton<IContactPersonEventBusPublisher, ContactPersonEventBusPublisher>();

            return services;
        }
    }
}
