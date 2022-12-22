using EventBus.EventHandlers;
using Jobs.Application.IntegrationEventHandlers.Subscribers.CandidateJobs.Payload;

namespace Jobs.Application.IntegrationEventHandlers.Subscribers.CandidateJobs
{
    public class CandidateJobRejectedIntegrationEvent : IntegrationEvent
    {
        public CandidateJobPayload? Payload { get; set; }
    }
}
