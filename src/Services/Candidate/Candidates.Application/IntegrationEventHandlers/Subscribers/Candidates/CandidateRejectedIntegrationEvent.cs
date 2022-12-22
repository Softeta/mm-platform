using Candidates.Application.IntegrationEventHandlers.Subscribers.Candidates.Payload;
using EventBus.EventHandlers;

namespace Candidates.Application.IntegrationEventHandlers.Subscribers.Candidates
{
    public class CandidateRejectedIntegrationEvent : IntegrationEvent
    {
        public Candidate? Payload { get; set; }
    }
}
