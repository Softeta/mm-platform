using Companies.Application.IntegrationEventHandlers.Subscribers.Companies.Payload.Models;

namespace Companies.Application.IntegrationEventHandlers.Subscribers.Companies.Payload
{
    public class CompanyPayload
    {
        public Guid Id { get; set; }
        public Image? Logo { get; set; }
        public IEnumerable<ContactPersonPayload> ContactPersons { get; set; } = new List<ContactPersonPayload>();
    }
}
