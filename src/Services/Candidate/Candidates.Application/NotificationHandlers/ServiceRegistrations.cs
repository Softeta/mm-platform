using Microsoft.Extensions.DependencyInjection;

namespace Candidates.Application.NotificationHandlers
{
    public static class ServiceRegistrations
    {
        public static IServiceCollection AddNotificationHandlers(this IServiceCollection services)
        {
            services.AddTransient<INotificationHandler, AssessmentStartedHandler>();
            services.AddTransient<INotificationHandler, AssessmentCompletedHandler>();
            services.AddTransient<INotificationHandler, AssessmentScoredHandler>();
            services.AddTransient<INotificationHandler, PackageInstanceScoredHandler>();
            services.AddTransient<INotificationHandlersManager, NotificationHandlersManager>();

            return services;
        }
    }
}
