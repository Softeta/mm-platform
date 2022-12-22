using Candidates.Application.IntegrationEventHandlers.Publishers.Payloads;
using EventBus.EventHandlers;

namespace Candidates.Application.IntegrationEventHandlers.Publishers
{
    internal class CandidateJobsChangedIntegrationEvent : IntegrationEvent
    {
        public CandidateJobsChangedIntegrationEvent(CandidateJobsPayload payload, DateTimeOffset emittedAt) : base(emittedAt)
        {
            Payload = payload;
        }

        public CandidateJobsPayload Payload { get; }
    }
}
