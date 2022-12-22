using Companies.Application.IntegrationEventHandlers.Subscribers.Jobs.Payloads.Models;

namespace Companies.Application.IntegrationEventHandlers.Subscribers.Jobs.Payloads
{
    public class JobPayload
    {
        public Company Company { get; set; } = null!;
    }
}
