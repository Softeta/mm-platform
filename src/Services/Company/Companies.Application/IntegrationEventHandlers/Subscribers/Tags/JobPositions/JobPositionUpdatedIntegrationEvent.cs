using Companies.Application.IntegrationEventHandlers.Subscribers.Tags.JobPositions.Payloads;
using EventBus.EventHandlers;

namespace Companies.Application.IntegrationEventHandlers.Subscribers.Tags.JobPositions
{
    public class JobPositionUpdatedIntegrationEvent : IntegrationEvent
    {
        public JobPositionPayload? Payload { get; set; }
    }
}
