using EventBus.EventHandlers;
using Jobs.Application.IntegrationEventHandlers.Publishers.Payloads;

namespace Jobs.Application.IntegrationEventHandlers.Publishers
{
    public class JobCandidateChangedIntegrationEvent : IntegrationEvent
    {
        public JobCandidateChangedIntegrationEvent(JobCandidatePayload payload, DateTimeOffset emittedAt) : base(emittedAt)
        {
            Payload = payload;
        }

        public JobCandidatePayload Payload { get; }
    }
}
