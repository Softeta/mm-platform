using Jobs.Application.IntegrationEventHandlers.Subscribers.CandidateJobs.Payload.Models;

namespace Jobs.Application.IntegrationEventHandlers.Subscribers.CandidateJobs.Payload
{
    public class CandidateJobsPayload
    {
        public Guid CandidateId { get; set; }
        public IEnumerable<CandidateSelectedInJob> SelectedInJobs { get; set; } = new List<CandidateSelectedInJob>();
    }
}
