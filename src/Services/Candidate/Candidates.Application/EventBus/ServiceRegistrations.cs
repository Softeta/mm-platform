using Candidates.Application.EventBus.Publishers;
using Microsoft.Extensions.DependencyInjection;

namespace Candidates.Application.EventBus
{
    public static class ServiceRegistrations
    {
        public static IServiceCollection AddEventBusPublishers(this IServiceCollection services)
        {
            services
                .AddSingleton<ICandidateEventBusPublisher, CandidateEventBusPublisher>()
                .AddSingleton<ICandidateJobsEventBusPublisher, CandidateJobsEventBusPublisher>();

            return services;
        }
    }
}
