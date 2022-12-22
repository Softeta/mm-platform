using Candidates.Application.IntegrationEventHandlers.Subscribers.Jobs.Payload;
using EventBus.EventHandlers;

namespace Candidates.Application.IntegrationEventHandlers.Subscribers.Jobs
{
    public class JobSelectedCandidatesUpdatedIntegrationEvent : IntegrationEvent
    {
        public JobSelectedCandidates? Payload { get; set; }
    }
}
