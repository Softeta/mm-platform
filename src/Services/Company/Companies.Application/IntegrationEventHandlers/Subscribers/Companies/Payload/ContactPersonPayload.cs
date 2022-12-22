using Companies.Application.IntegrationEventHandlers.Subscribers.Companies.Payload.Models;

namespace Companies.Application.IntegrationEventHandlers.Subscribers.Companies.Payload
{
    public class ContactPersonPayload
    {
        public Guid Id { get; set; }
        public Guid? ExternalId { get; set; }
        public Guid CompanyId { get; set; } 
        public Image? Picture { get; set; }
    }
}
