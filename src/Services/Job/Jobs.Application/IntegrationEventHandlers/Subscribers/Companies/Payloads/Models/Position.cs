namespace Jobs.Application.IntegrationEventHandlers.Subscribers.Companies.Payloads.Models
{
    public class Position
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = null!;
        public Tag? AliasTo { get; set; }
    }
}
