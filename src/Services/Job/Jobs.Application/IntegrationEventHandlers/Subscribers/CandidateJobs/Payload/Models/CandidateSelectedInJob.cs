using Domain.Seedwork.Enums;

namespace Jobs.Application.IntegrationEventHandlers.Subscribers.CandidateJobs.Payload.Models
{
    public class CandidateSelectedInJob
    {
        public Guid JobId { get; set; }
        public SelectedCandidateStage Stage { get; set; }
    }
}
