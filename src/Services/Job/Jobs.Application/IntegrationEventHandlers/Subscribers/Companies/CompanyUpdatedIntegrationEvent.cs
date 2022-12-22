using EventBus.EventHandlers;
using Jobs.Application.IntegrationEventHandlers.Subscribers.Companies.Payloads;

namespace Jobs.Application.IntegrationEventHandlers.Subscribers.Companies
{
    public class CompanyUpdatedIntegrationEvent : IntegrationEvent
    {
        public CompanyPayload Payload { get; set; } = null!;
    }
}
