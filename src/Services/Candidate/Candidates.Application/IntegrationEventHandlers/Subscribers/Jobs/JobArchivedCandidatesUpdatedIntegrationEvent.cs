using Candidates.Application.IntegrationEventHandlers.Subscribers.Jobs.Payload;
using EventBus.EventHandlers;

namespace Candidates.Application.IntegrationEventHandlers.Subscribers.Jobs
{
    public class JobArchivedCandidatesUpdatedIntegrationEvent : IntegrationEvent
    {
        public JobArchivedCandidates? Payload { get; set; }
    }
}
