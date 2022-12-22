using Jobs.Application.IntegrationEventHandlers.Subscribers.CandidateJobs.Payload.Models;

namespace Jobs.Application.IntegrationEventHandlers.Subscribers.CandidateJobs.Payload
{
    public class CandidateJobPayload
    {
        public Guid CandidateId { get; set; }
        public Guid JobId { get; set; }
        public IEnumerable<CandidatedArchivedInJob> ArchivedInJobs { get; set; } = new List<CandidatedArchivedInJob>();
    }
}
