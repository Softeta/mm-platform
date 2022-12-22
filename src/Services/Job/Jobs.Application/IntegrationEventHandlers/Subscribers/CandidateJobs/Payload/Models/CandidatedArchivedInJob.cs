using Domain.Seedwork.Enums;

namespace Jobs.Application.IntegrationEventHandlers.Subscribers.CandidateJobs.Payload.Models
{
    public class CandidatedArchivedInJob
    {
        public Guid JobId { get; set; }
        public ArchivedCandidateStage Stage { get; set; }
    }
}
