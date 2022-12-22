using Candidates.Application.IntegrationEventHandlers.Subscribers.Jobs.Payload;
using EventBus.EventHandlers;

namespace Candidates.Application.IntegrationEventHandlers.Subscribers.Jobs
{
    public class JobCandidatesInformationUpdatedIntegrationEvent : IntegrationEvent
    {
        public JobCandidatesInformation? Payload { get; set; }
    }
}
