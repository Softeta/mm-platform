using Domain.Seedwork.Enums;

namespace Companies.Application.IntegrationEventHandlers.Subscribers.Jobs.Payloads.Models
{
    public class Company
    {
        public Guid Id { get; set; }
        public CompanyStatus Status { get; set; }
    }
}
