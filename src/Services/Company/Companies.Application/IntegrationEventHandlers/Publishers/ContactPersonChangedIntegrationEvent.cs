using Companies.Application.IntegrationEventHandlers.Publishers.Payloads;
using EventBus.EventHandlers;

namespace Companies.Application.IntegrationEventHandlers.Publishers
{
    internal class ContactPersonChangedIntegrationEvent : IntegrationEvent
    {
        public ContactPersonChangedIntegrationEvent(ContactPersonPayload payload, DateTimeOffset emittedAt) : base(emittedAt)
        {
            Payload = payload;
        }

        public ContactPersonPayload Payload { get; }
    }
}
