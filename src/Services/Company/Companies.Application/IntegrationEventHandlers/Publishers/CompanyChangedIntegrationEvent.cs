using Companies.Application.IntegrationEventHandlers.Publishers.Payloads;
using EventBus.EventHandlers;

namespace Companies.Application.IntegrationEventHandlers.Publishers
{
    internal class CompanyChangedIntegrationEvent : IntegrationEvent
    {
        public CompanyChangedIntegrationEvent(CompanyPayload payload, DateTimeOffset emittedAt) : base(emittedAt)
        {
            Payload = payload;
        }

        public CompanyPayload Payload { get; }
    }
}
