using Candidates.Application.IntegrationEventHandlers.Subscribers.Jobs.Payload;
using EventBus.EventHandlers;

namespace Candidates.Application.IntegrationEventHandlers.Subscribers.Jobs
{
    public class JobCandidatesJobStageUpdatedIntegrationEvent : IntegrationEvent
    {
        public JobCandidateJobStage? Payload { get; set; }
    }
}
