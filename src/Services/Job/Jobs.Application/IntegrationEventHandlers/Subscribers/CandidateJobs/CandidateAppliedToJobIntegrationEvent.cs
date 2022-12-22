using EventBus.EventHandlers;
using Jobs.Application.IntegrationEventHandlers.Subscribers.CandidateJobs.Payload;

namespace Jobs.Application.IntegrationEventHandlers.Subscribers.CandidateJobs
{
    public class CandidateAppliedToJobIntegrationEvent : IntegrationEvent
    {
        public CandidateAppliedToJobPayload? Payload { get; set; }
    }
}
