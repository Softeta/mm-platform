namespace Jobs.Application.IntegrationEventHandlers.Subscribers.Companies.Payloads.Models
{
    public class Tag
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = null!;
    }
}
