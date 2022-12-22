using Candidates.Application.IntegrationEventHandlers.Subscribers.Jobs.Payload;
using EventBus.EventHandlers;

namespace Candidates.Application.IntegrationEventHandlers.Subscribers.Jobs
{
    public class JobCandidatesHiredIntegrationEvent : IntegrationEvent
    {
        public JobCandidatesInformation? Payload { get; set; }
    }
}
