using EventBus.EventHandlers;
using Jobs.Application.IntegrationEventHandlers.Publishers.Payloads;

namespace Jobs.Application.IntegrationEventHandlers.Publishers
{
    public class JobChangedIntegrationEvent : IntegrationEvent
    {
        public JobChangedIntegrationEvent(JobPayload payload, DateTimeOffset emittedAt) : base(emittedAt)
        {
            Payload = payload;
        }

        public JobPayload Payload { get; }
    }
}
