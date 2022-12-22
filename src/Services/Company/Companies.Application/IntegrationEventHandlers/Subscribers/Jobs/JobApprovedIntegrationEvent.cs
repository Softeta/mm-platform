using Companies.Application.IntegrationEventHandlers.Subscribers.Jobs.Payloads;
using EventBus.EventHandlers;

namespace Companies.Application.IntegrationEventHandlers.Subscribers.Jobs
{
    public class JobApprovedIntegrationEvent : IntegrationEvent
    {
        public JobPayload? Payload { get; set; }
    }
}
