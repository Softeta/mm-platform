using Jobs.Application.IntegrationEventHandlers.Subscribers.CandidateJobs.Payload.Models.Candidates;

namespace Jobs.Application.IntegrationEventHandlers.Subscribers.CandidateJobs.Payload
{
    public class CandidateAppliedToJobPayload
    {
        public Guid CandidateId { get; set; }
        public Guid JobId { get; set; }
        public Candidate? Candidate { get; set; }
    }
}
