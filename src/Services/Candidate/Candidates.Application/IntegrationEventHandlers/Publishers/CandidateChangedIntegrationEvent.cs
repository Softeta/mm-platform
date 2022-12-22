using Candidates.Application.IntegrationEventHandlers.Publishers.Payloads;
using EventBus.EventHandlers;

namespace Candidates.Application.IntegrationEventHandlers.Publishers
{
    internal class CandidateChangedIntegrationEvent : IntegrationEvent
    {
        public CandidateChangedIntegrationEvent(CandidatePayload payload, DateTimeOffset emittedAt) : base(emittedAt)
        {
            Payload = payload;
        }

        public CandidatePayload Payload { get; }
    }
}
