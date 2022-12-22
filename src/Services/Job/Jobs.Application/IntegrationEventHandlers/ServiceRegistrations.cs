using EventBus.EventHandlers;
using Jobs.Application.IntegrationEventHandlers.Subscribers;
using Jobs.Application.IntegrationEventHandlers.Subscribers.BackOfficeUsers;
using Jobs.Application.IntegrationEventHandlers.Subscribers.CandidateJobs;
using Jobs.Application.IntegrationEventHandlers.Subscribers.Candidates;
using Jobs.Application.IntegrationEventHandlers.Subscribers.Companies;
using Jobs.Application.IntegrationEventHandlers.Subscribers.Tags.JobPositions;
using Jobs.Application.IntegrationEventHandlers.Subscribers.Tags.Skills;
using Microsoft.Extensions.DependencyInjection;

namespace Jobs.Application.IntegrationEventHandlers
{
    public static class ServiceRegistrations
    {
        public static IServiceCollection AddIntegrationEventHandlers(this IServiceCollection services)
        {
            services.AddSingleton<IIntegrationEventHandler<CandidateUpdatedIntegrationEvent>, CandidateUpdatedIntegrationEventHandler>();
            services.AddSingleton<IIntegrationEventHandler<CompanyUpdatedIntegrationEvent>, CompanyUpdatedIntegrationEventHandler>();
            services.AddSingleton<IIntegrationEventHandler<BackOfficeUserUpdatedIntegrationEvent>, BackOfficeUserUpdatedIntegrationEventHandler>();
            services.AddSingleton<IIntegrationEventHandler<CandidateJobsShortlistedIntegrationEvent>, CandidateJobsShortlistedIntegrationEventHandler>();
            services.AddSingleton<IIntegrationEventHandler<CandidateJobsUnshortlistedIntegrationEvent>, CandidateJobsUnshortlistedIntegrationEventHandler>();
            services.AddSingleton<IIntegrationEventHandler<CandidateJobRejectedIntegrationEvent>, CandidateJobRejectedIntegrationEventHandler>();
            services.AddSingleton<IIntegrationEventHandler<CandidateAppliedToJobIntegrationEvent>, CandidateAppliedToJobIntegrationEventHandler>();
            services.AddSingleton<IIntegrationEventHandler<ContactPersonUpdatedIntegrationEvent>, ContactPersonUpdatedIntegrationEventHandler>();
            services.AddSingleton<IIntegrationEventHandler<CandidateJobsHiredIntegrationEvent>, CandidateJobsHiredIntegrationEventHandler>();
            services.AddSingleton<IIntegrationEventHandler<CompanyRejectedIntegrationEvent>, CompanyRejectedIntegrationEventHandler>();
            services.AddSingleton<IIntegrationEventHandler<SkillUpdatedIntegrationEvent>, SkillUpdatedIntegrationEventHandler>();
            services.AddSingleton<IIntegrationEventHandler<JobPositionUpdatedIntegrationEvent>, JobPositionUpdatedIntegrationEventHandler>();
            services.AddSingleton<IIntegrationEventHandler<ContactPersonLinkedIntegrationEvent>, ContactPersonLinkedIntegrationEventHandler>();
            services.AddSingleton<ISubscribersEventHandlersManager, JobSubscriptionIntegrationEventHandlers>();

            return services;
        }
    }
}
