using Companies.Application.IntegrationEventHandlers.Subscribers;
using Companies.Application.IntegrationEventHandlers.Subscribers.Companies;
using Companies.Application.IntegrationEventHandlers.Subscribers.Jobs;
using Companies.Application.IntegrationEventHandlers.Subscribers.Schedulers;
using Companies.Application.IntegrationEventHandlers.Subscribers.Tags.JobPositions;
using EventBus.EventHandlers;
using Microsoft.Extensions.DependencyInjection;

namespace Companies.Application.IntegrationEventHandlers
{
    public static class ServiceRegistrations
    {
        public static IServiceCollection AddIntegrationEventHandlers(this IServiceCollection services)
        {
            services.AddSingleton<IIntegrationEventHandler<SyncRegistryCenterCompaniesSheduledIntegrationEvent>, SyncRegistryCenterCompaniesSheduledIntegrationEventHandler>();
            services.AddSingleton<IIntegrationEventHandler<RemoveExpiredCompanyFileCacheSheduledIntegrationEvent>, RemoveExpiredCompanyFileCacheSheduledIntegrationEventHandler>();
            services.AddSingleton<IIntegrationEventHandler<ContactPersonRejectedIntegrationEvent>, ContactPersonRejectedIntegrationEventHandler>();
            services.AddSingleton<IIntegrationEventHandler<CompanyRejectedIntegrationEvent>, CompanyRejectedIntegrationEventHandler>();
            services.AddSingleton<IIntegrationEventHandler<JobApprovedIntegrationEvent>, JobApprovedIntegrationEventHandler>();
            services.AddSingleton<IIntegrationEventHandler<JobPositionUpdatedIntegrationEvent>, JobPositionUpdatedIntegrationEventHandler>();
            services.AddSingleton<ISubscribersEventHandlersManager, SubscriptionIntegrationEventHandlers>();

            return services;
        }
    }
}
