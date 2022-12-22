using EventBus.EventHandlers;
using Jobs.Application.IntegrationEventHandlers.Subscribers.Tags.JobPositions.Payloads;

namespace Jobs.Application.IntegrationEventHandlers.Subscribers.Tags.JobPositions
{
    public class JobPositionUpdatedIntegrationEvent : IntegrationEvent
    {
        public JobPositionPayload? Payload { get; set; }
    }
}
