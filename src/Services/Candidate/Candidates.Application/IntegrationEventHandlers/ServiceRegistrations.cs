using Candidates.Application.IntegrationEventHandlers.Subscribers;
using Candidates.Application.IntegrationEventHandlers.Subscribers.Jobs;
using Candidates.Application.IntegrationEventHandlers.Subscribers.Schedulers;
using Candidates.Application.IntegrationEventHandlers.Subscribers.Tags.Positions;
using Candidates.Application.IntegrationEventHandlers.Subscribers.Tags.Skills;
using EventBus.EventHandlers;
using Microsoft.Extensions.DependencyInjection;

namespace Candidates.Application.IntegrationEventHandlers
{
    public static class ServiceRegistrations
    {
        public static IServiceCollection AddIntegrationEventHandlers(this IServiceCollection services)
        {
            services.AddSingleton<IIntegrationEventHandler<JobSelectedCandidatesAddedIntegrationEvent>, JobSelectedCandidatesAddedIntegrationEventHandler>();
            services.AddSingleton<IIntegrationEventHandler<JobSelectedCandidatesUpdatedIntegrationEvent>, JobSelectedCandidatesUpdatedIntegrationEventHandler>();
            services.AddSingleton<IIntegrationEventHandler<JobArchivedCandidatesUpdatedIntegrationEvent>, JobArchivedCandidatesUpdatedIntegrationEventHandler>();
            services.AddSingleton<IIntegrationEventHandler<JobCandidatesInformationUpdatedIntegrationEvent>, JobCandidatesInformationUpdatedIntegrationEventHandler>();
            services.AddSingleton<IIntegrationEventHandler<JobCandidatesHiredIntegrationEvent>, JobCandidatesHiredIntegrationEventHandler>();
            services.AddSingleton<IIntegrationEventHandler<DeleteCandidatesSheduledIntegrationEvent>, DeleteCandidatesSheduledIntegrationEventHandler>();
            services.AddSingleton<IIntegrationEventHandler<RemoveExpiredCandidateFileCacheSheduledIntegrationEvent>, RemoveExpiredCandidateFileCacheSheduledIntegrationEventHandler>();
            services.AddSingleton<IIntegrationEventHandler<JobCandidatesJobStageUpdatedIntegrationEvent>, JobCandidatesJobStageUpdatedIntegrationEventHandler>();
            services.AddSingleton<IIntegrationEventHandler<SkillUpdatedIntegrationEvent>, SkillUpdatedIntegrationEventHandler>();
            services.AddSingleton<IIntegrationEventHandler<JobPositionUpdatedIntegrationEvent>, JobPositionUpdatedIntegrationEventHandler>();
            services.AddSingleton<ISubscribersEventHandlersManager, SubscriptionIntegrationEventHandlers>();

            return services;
        }
    }
}
