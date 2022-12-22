namespace Candidates.Application.IntegrationEventHandlers.Subscribers.Jobs.Payload.Models
{
    public class Company
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? LogoUri { get; set; }
    }
}
