using Domain.Seedwork.Enums;

namespace Jobs.Application.IntegrationEventHandlers.Subscribers.Companies.Payloads
{
    public class CompanyPayload
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public CompanyStatus Status { get; set; }
        public string? LogoUri { get; set; }
    }
}
