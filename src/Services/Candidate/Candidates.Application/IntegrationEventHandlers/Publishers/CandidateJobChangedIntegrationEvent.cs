using Candidates.Application.IntegrationEventHandlers.Publishers.Payloads;
using EventBus.EventHandlers;

namespace Candidates.Application.IntegrationEventHandlers.Publishers
{
    internal class CandidateJobChangedIntegrationEvent : IntegrationEvent
    {
        public CandidateJobChangedIntegrationEvent(CandidateJobChangedPayload payload, DateTimeOffset emittedAt) : base(emittedAt)
        {
            Payload = payload;
        }

        public CandidateJobChangedPayload Payload { get; }
    }
}
