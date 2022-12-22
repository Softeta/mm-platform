using Companies.Application.IntegrationEventHandlers.Subscribers.Tags.JobPositions.Models;

namespace Companies.Application.IntegrationEventHandlers.Subscribers.Tags.JobPositions.Payloads
{
    public class JobPositionPayload
    {
        public Guid? Id { get; set; }
        public string? Code { get; set; }
        public Alias? AliasTo { get; set; }
    }
}
