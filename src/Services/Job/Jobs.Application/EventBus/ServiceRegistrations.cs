using Jobs.Application.EventBus.Publishers;
using Microsoft.Extensions.DependencyInjection;

namespace Jobs.Application.EventBus
{
    public static class ServiceRegistrations
    {
        public static IServiceCollection AddEventBusPublishers(this IServiceCollection services)
        {
            services.AddSingleton<IJobEventBusPublisher, JobEventBusPublisher>();
            services.AddSingleton<IJobCandidatesEventBusPublisher, JobCandidatesEventBusPublisher>();

            return services;
        }
    }
}
