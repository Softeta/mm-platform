using EventBus.EventHandlers;
using Jobs.Application.IntegrationEventHandlers.Subscribers.Companies.Payloads;

namespace Jobs.Application.IntegrationEventHandlers.Subscribers.Companies
{
    public class ContactPersonUpdatedIntegrationEvent : IntegrationEvent
    {
        public ContactPersonPayload Payload { get; set; } = null!;
    }
}
