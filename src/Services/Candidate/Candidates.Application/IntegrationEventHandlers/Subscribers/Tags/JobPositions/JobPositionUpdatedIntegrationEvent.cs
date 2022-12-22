using Candidates.Application.IntegrationEventHandlers.Subscribers.Tags.JobPositions.Payloads;
using EventBus.EventHandlers;

namespace Candidates.Application.IntegrationEventHandlers.Subscribers.Tags.Positions
{
    public class JobPositionUpdatedIntegrationEvent : IntegrationEvent
    {
        public JobPositionPayload? Payload { get; set; }
    }
}
