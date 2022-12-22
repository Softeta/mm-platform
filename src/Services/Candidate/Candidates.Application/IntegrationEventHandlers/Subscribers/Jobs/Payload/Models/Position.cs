namespace Candidates.Application.IntegrationEventHandlers.Subscribers.Jobs.Payload.Models
{
    public class Position
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = null!;
        public Tag? AliasTo { get; set; }
    }
}