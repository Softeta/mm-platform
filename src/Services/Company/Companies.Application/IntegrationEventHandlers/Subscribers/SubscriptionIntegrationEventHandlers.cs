using Companies.Application.IntegrationEventHandlers.Subscribers.Companies;
using Companies.Application.IntegrationEventHandlers.Subscribers.Jobs;
using Companies.Application.IntegrationEventHandlers.Subscribers.Schedulers;
using Companies.Application.IntegrationEventHandlers.Subscribers.Tags.JobPositions;
using EventBus.Constants;
using EventBus.EventHandlers;

namespace Companies.Application.IntegrationEventHandlers.Subscribers
{
    public class SubscriptionIntegrationEventHandlers : SubscribersEventHandlersManager
    {
        public SubscriptionIntegrationEventHandlers(
            IIntegrationEventHandler<SyncRegistryCenterCompaniesSheduledIntegrationEvent> syncRegistryCenterCompanieScheduledEventHandler,
            IIntegrationEventHandler<RemoveExpiredCompanyFileCacheSheduledIntegrationEvent> removeExpiredCompanyFileCacheSheduledIntegrationEventHandler,
            IIntegrationEventHandler<ContactPersonRejectedIntegrationEvent> contactPersonRejectedIntegrationEventHandler,
            IIntegrationEventHandler<CompanyRejectedIntegrationEvent> companyRejectedIntegrationEventHandler,
            IIntegrationEventHandler<JobApprovedIntegrationEvent> jobApprovedIntegrationEventHandler,
            IIntegrationEventHandler<JobPositionUpdatedIntegrationEvent> jobPositionUpdatedIntegrationEvent
           )
        {
            AddSubscriberHandler(Topics.SchedulerJobScheduled.Filters.SyncRegistryCenterCompaniesScheduled,
                syncRegistryCenterCompanieScheduledEventHandler as IntegrationEventHandler);

            AddSubscriberHandler(Topics.SchedulerJobScheduled.Filters.RemoveExpiredFileCacheScheduled,
                removeExpiredCompanyFileCacheSheduledIntegrationEventHandler
                as IntegrationEventHandler);

            AddSubscriberHandler(Topics.ContactPersonChanged.Filters.ContactPersonRejected,
                contactPersonRejectedIntegrationEventHandler
                as IntegrationEventHandler);

            AddSubscriberHandler(Topics.CompanyChanged.Filters.CompanyRejected,
                companyRejectedIntegrationEventHandler
                as IntegrationEventHandler);

            AddSubscriberHandler(Topics.JobChanged.Filters.JobApproved,
                jobApprovedIntegrationEventHandler
                as IntegrationEventHandler);

            AddSubscriberHandler(Topics.JobPositionChanged.Filters.JobPositionMerged,
                jobPositionUpdatedIntegrationEvent
                as IntegrationEventHandler);

            AddSubscriberHandler(Topics.JobPositionChanged.Filters.JobPositionUnmerged,
                jobPositionUpdatedIntegrationEvent
                as IntegrationEventHandler);
        }
    }
}
