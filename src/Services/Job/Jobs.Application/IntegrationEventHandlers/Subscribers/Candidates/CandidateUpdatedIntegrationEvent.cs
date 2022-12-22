using EventBus.EventHandlers;
using Jobs.Application.IntegrationEventHandlers.Subscribers.Candidates.Payload;

namespace Jobs.Application.IntegrationEventHandlers.Subscribers.Candidates
{
    public class CandidateUpdatedIntegrationEvent : IntegrationEvent
    {
        public CandidatePayload Payload { get; set; } = null!;
    }
}
