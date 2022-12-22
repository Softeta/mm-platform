using Candidates.Application.IntegrationEventHandlers.Publishers.Payloads;
using EventBus.EventHandlers;

namespace Candidates.Application.IntegrationEventHandlers.Publishers
{
    internal class CandidateAppliedToJobIntegrationEvent : IntegrationEvent
    {
        public CandidateAppliedToJobIntegrationEvent(CandidateAppliedToJobPayload payload, DateTimeOffset emittedAt) : base(emittedAt)
        {
            Payload = payload;
        }

        public CandidateAppliedToJobPayload Payload { get; }
    }
}
