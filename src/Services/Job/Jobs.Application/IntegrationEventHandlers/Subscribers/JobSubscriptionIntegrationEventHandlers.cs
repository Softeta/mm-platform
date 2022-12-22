using EventBus.Constants;
using EventBus.EventHandlers;
using Jobs.Application.IntegrationEventHandlers.Subscribers.BackOfficeUsers;
using Jobs.Application.IntegrationEventHandlers.Subscribers.CandidateJobs;
using Jobs.Application.IntegrationEventHandlers.Subscribers.Candidates;
using Jobs.Application.IntegrationEventHandlers.Subscribers.Companies;
using Jobs.Application.IntegrationEventHandlers.Subscribers.Tags.JobPositions;
using Jobs.Application.IntegrationEventHandlers.Subscribers.Tags.Skills;

namespace Jobs.Application.IntegrationEventHandlers.Subscribers
{
    public class JobSubscriptionIntegrationEventHandlers : SubscribersEventHandlersManager
    {
        public JobSubscriptionIntegrationEventHandlers(
            IIntegrationEventHandler<CandidateUpdatedIntegrationEvent> candidateUpdatedEventHandler,
            IIntegrationEventHandler<CompanyUpdatedIntegrationEvent> companyUpdatedEventHandler,
            IIntegrationEventHandler<BackOfficeUserUpdatedIntegrationEvent> backOfficeUserUpdatedEventHandler,
            IIntegrationEventHandler<CandidateJobsShortlistedIntegrationEvent> candidateJobsShortlistedIntegrationEventHandler,
            IIntegrationEventHandler<CandidateJobsUnshortlistedIntegrationEvent> candidateJobsUnshortlistedIntegrationEventHandler,
            IIntegrationEventHandler<CandidateJobRejectedIntegrationEvent> candidateJobRejectedEventHandler,
            IIntegrationEventHandler<CandidateAppliedToJobIntegrationEvent> candidateAppliedToJobEventHandler,
            IIntegrationEventHandler<ContactPersonUpdatedIntegrationEvent> contactPersonUpdatedIntegrationEventHandler,
            IIntegrationEventHandler<CandidateJobsHiredIntegrationEvent> candidateJobsHiredIntegrationEventHandler,
            IIntegrationEventHandler<CompanyRejectedIntegrationEvent> companyRejectedIntegrationEventHandler,
            IIntegrationEventHandler<SkillUpdatedIntegrationEvent> skillUpdatedIntegrationEventHandler,
            IIntegrationEventHandler<JobPositionUpdatedIntegrationEvent> jobPositionUpdatedIntegrationEventHandler,
            IIntegrationEventHandler<ContactPersonLinkedIntegrationEvent> contactPersonLinkedEventHandler
            )
        {
            AddSubscriberHandler(
                Topics.CandidateChanged.Filters.CandidateUpdated,
                candidateUpdatedEventHandler as IntegrationEventHandler
            );
            AddSubscriberHandler(
                Topics.CompanyChanged.Filters.CompanyUpdated,
                companyUpdatedEventHandler as IntegrationEventHandler
            );
            AddSubscriberHandler(
                Topics.BackOfficeUserChanged.Filters.BackOfficeUserUpdated,
                backOfficeUserUpdatedEventHandler as IntegrationEventHandler
            );
            AddSubscriberHandler(
                Topics.CandidateJobsChanged.Filters.CandidateJobsShortlisted,
                candidateJobsShortlistedIntegrationEventHandler as IntegrationEventHandler
            );
            AddSubscriberHandler(
                Topics.CandidateJobsChanged.Filters.CandidateJobsUnshortlisted, 
                candidateJobsUnshortlistedIntegrationEventHandler as IntegrationEventHandler
            );
            AddSubscriberHandler(
                Topics.CandidateJobsChanged.Filters.CandidateJobRejected,
                candidateJobRejectedEventHandler as IntegrationEventHandler
            );
            AddSubscriberHandler(
                Topics.CandidateJobsChanged.Filters.CandidateAppliedToJob,
                candidateAppliedToJobEventHandler as IntegrationEventHandler
            );
            AddSubscriberHandler(
                Topics.ContactPersonChanged.Filters.ContactPersonUpdated,
                contactPersonUpdatedIntegrationEventHandler as IntegrationEventHandler
            );
            AddSubscriberHandler(
                Topics.ContactPersonChanged.Filters.ContactPersonLinked,
                contactPersonLinkedEventHandler as IntegrationEventHandler
            );
            AddSubscriberHandler(
                Topics.CandidateJobsChanged.Filters.CandidateJobsHired,
                candidateJobsHiredIntegrationEventHandler as IntegrationEventHandler
            );
            AddSubscriberHandler(
                Topics.CompanyChanged.Filters.CompanyRejected,
                companyRejectedIntegrationEventHandler as IntegrationEventHandler
            );
            AddSubscriberHandler(
                Topics.CompanyChanged.Filters.CompanyApproved,
                companyUpdatedEventHandler as IntegrationEventHandler
            );
            AddSubscriberHandler(
                Topics.SkillChanged.Filters.SkillMerged,
                skillUpdatedIntegrationEventHandler as IntegrationEventHandler
           );
            AddSubscriberHandler(
                Topics.SkillChanged.Filters.SkillUnmerged,
                skillUpdatedIntegrationEventHandler as IntegrationEventHandler
           );
            AddSubscriberHandler(
                Topics.JobPositionChanged.Filters.JobPositionMerged,
                jobPositionUpdatedIntegrationEventHandler as IntegrationEventHandler
           );
            AddSubscriberHandler(
                Topics.JobPositionChanged.Filters.JobPositionUnmerged,
                jobPositionUpdatedIntegrationEventHandler as IntegrationEventHandler
           );
        }
    }
}
