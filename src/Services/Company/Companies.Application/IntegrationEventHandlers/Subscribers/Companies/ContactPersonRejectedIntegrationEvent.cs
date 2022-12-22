using Companies.Application.IntegrationEventHandlers.Subscribers.Companies.Payload;
using EventBus.EventHandlers;

namespace Companies.Application.IntegrationEventHandlers.Subscribers.Companies
{
    public class ContactPersonRejectedIntegrationEvent : IntegrationEvent
    {
        public ContactPersonPayload? Payload { get; set; }
    }
}
