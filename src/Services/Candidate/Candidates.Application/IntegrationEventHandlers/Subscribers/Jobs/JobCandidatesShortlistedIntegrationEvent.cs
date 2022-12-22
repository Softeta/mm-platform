using Candidates.Application.IntegrationEventHandlers.Subscribers.Jobs.Payload;
using EventBus.EventHandlers;

namespace Candidates.Application.IntegrationEventHandlers.Subscribers.Jobs
{
    public class JobCandidatesShortlistedIntegrationEvent : IntegrationEvent
    {
        public JobCandidateJobStage? Payload { get; set; }
    }
}
