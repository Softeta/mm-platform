using Microsoft.Extensions.DependencyInjection;
using Scheduler.Job.EventBus.Publishers;

namespace Scheduler.Job.EventBus
{
    public static class ServiceRegistrations
    {
        public static IServiceCollection AddEventBusPublishers(this IServiceCollection services)
        {
            services.AddSingleton<ISchedulerJobEventBusPublisher, SchedulerJobEventBusPublisher>();

            return services;
        }
    }
}
