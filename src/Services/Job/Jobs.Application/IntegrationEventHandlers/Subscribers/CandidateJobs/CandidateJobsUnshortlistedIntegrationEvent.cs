using EventBus.EventHandlers;
using Jobs.Application.IntegrationEventHandlers.Subscribers.CandidateJobs.Payload;

namespace Jobs.Application.IntegrationEventHandlers.Subscribers.CandidateJobs
{
    public class CandidateJobsUnshortlistedIntegrationEvent : IntegrationEvent
    {
        public CandidateJobsPayload? Payload { get; set; }
    }
}
