using Companies.Application.IntegrationEventHandlers.Subscribers.Companies.Payload;
using EventBus.EventHandlers;

namespace Companies.Application.IntegrationEventHandlers.Subscribers.Companies
{
    public class CompanyRejectedIntegrationEvent : IntegrationEvent
    {
        public CompanyPayload? Payload { get; set; }
    }
}
