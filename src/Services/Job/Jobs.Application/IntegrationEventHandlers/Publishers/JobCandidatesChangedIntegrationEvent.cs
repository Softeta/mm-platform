using EventBus.EventHandlers;
using Jobs.Application.IntegrationEventHandlers.Publishers.Payloads;

namespace Jobs.Application.IntegrationEventHandlers.Publishers
{
    public class JobCandidatesChangedIntegrationEvent : IntegrationEvent
    {
        public JobCandidatesChangedIntegrationEvent(JobCandidatesPayload payload, DateTimeOffset emittedAt) : base(emittedAt)
        {
            Payload = payload;
        }

        public JobCandidatesPayload Payload { get; }
    }
}
