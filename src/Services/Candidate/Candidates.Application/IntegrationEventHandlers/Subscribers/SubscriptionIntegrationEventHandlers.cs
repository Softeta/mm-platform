using Candidates.Application.IntegrationEventHandlers.Subscribers.Jobs;
using Candidates.Application.IntegrationEventHandlers.Subscribers.Schedulers;
using Candidates.Application.IntegrationEventHandlers.Subscribers.Tags.Positions;
using Candidates.Application.IntegrationEventHandlers.Subscribers.Tags.Skills;
using EventBus.Constants;
using EventBus.EventHandlers;

namespace Candidates.Application.IntegrationEventHandlers.Subscribers
{
    public class SubscriptionIntegrationEventHandlers : SubscribersEventHandlersManager
    {
        public SubscriptionIntegrationEventHandlers(
           IIntegrationEventHandler<JobSelectedCandidatesAddedIntegrationEvent> jobSelectedCandidatesAddedEventHandler,
           IIntegrationEventHandler<JobSelectedCandidatesUpdatedIntegrationEvent> jobSelectedCandidatesUpdatedEventHandler,
           IIntegrationEventHandler<JobArchivedCandidatesUpdatedIntegrationEvent> jobArchivedCandidatesUpdatedEventHandler,
           IIntegrationEventHandler<JobCandidatesInformationUpdatedIntegrationEvent> jobCandidatesInformationUpdatedEventHandler,
           IIntegrationEventHandler<JobCandidatesHiredIntegrationEvent> jobCandidatesHiredEventHandler,
           IIntegrationEventHandler<DeleteCandidatesSheduledIntegrationEvent> deleteCandidatesSheduledEventHandler,
           IIntegrationEventHandler<RemoveExpiredCandidateFileCacheSheduledIntegrationEvent> removeExpiredCandidateFileCacheSheduledEventHandler,
           IIntegrationEventHandler<JobCandidatesJobStageUpdatedIntegrationEvent> jobCandidatesJobStageUpdatedEventHandler,
           IIntegrationEventHandler<SkillUpdatedIntegrationEvent> skillUpdatedEventHandler,
           IIntegrationEventHandler<JobPositionUpdatedIntegrationEvent> jobPositionUpdatedEventHandler
           )
        {
            AddSubscriberHandler(Topics.JobCandidatesChanged.Filters.JobSelectedCandidatesAdded,
                jobSelectedCandidatesAddedEventHandler
                as IntegrationEventHandler);

            AddSubscriberHandler(Topics.JobCandidatesChanged.Filters.JobSelectedCandidatesUpdated,
                jobSelectedCandidatesUpdatedEventHandler
                as IntegrationEventHandler);

            AddSubscriberHandler(Topics.JobCandidatesChanged.Filters.JobArchivedCandidatesChanged, 
                jobArchivedCandidatesUpdatedEventHandler 
                as IntegrationEventHandler);

            AddSubscriberHandler(Topics.JobCandidatesChanged.Filters.JobCandidatesInformationUpdated,
                jobCandidatesInformationUpdatedEventHandler
                as IntegrationEventHandler);

            AddSubscriberHandler(Topics.JobCandidatesChanged.Filters.JobCandidatesHired,
                jobCandidatesHiredEventHandler
                as IntegrationEventHandler);

            AddSubscriberHandler(Topics.SchedulerJobScheduled.Filters.DeleteCandidatesScheduled,
                deleteCandidatesSheduledEventHandler
                as IntegrationEventHandler);

            AddSubscriberHandler(Topics.SchedulerJobScheduled.Filters.RemoveExpiredFileCacheScheduled,
                removeExpiredCandidateFileCacheSheduledEventHandler
                as IntegrationEventHandler);

            AddSubscriberHandler(Topics.JobCandidatesChanged.Filters.JobCandidatesJobStageUpdated,
                jobCandidatesJobStageUpdatedEventHandler
                as IntegrationEventHandler);

            AddSubscriberHandler(Topics.SkillChanged.Filters.SkillMerged,
                skillUpdatedEventHandler
                as IntegrationEventHandler);

            AddSubscriberHandler(Topics.SkillChanged.Filters.SkillUnmerged,
                skillUpdatedEventHandler
                as IntegrationEventHandler);

            AddSubscriberHandler(
                Topics.JobPositionChanged.Filters.JobPositionMerged,
                jobPositionUpdatedEventHandler as IntegrationEventHandler
           );
            AddSubscriberHandler(
                Topics.JobPositionChanged.Filters.JobPositionUnmerged,
                jobPositionUpdatedEventHandler as IntegrationEventHandler
           );
        }
    }
}
