using Domain.Seedwork.Enums;

namespace Candidates.Application.IntegrationEventHandlers.Subscribers.Jobs.Payload.Models
{
    public class SelectedCandidate : Candidate
    {
        public SelectedCandidateStage Stage { get; set; }
    }
}
