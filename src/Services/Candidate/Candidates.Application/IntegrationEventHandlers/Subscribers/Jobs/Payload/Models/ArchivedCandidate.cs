using Domain.Seedwork.Enums;

namespace Candidates.Application.IntegrationEventHandlers.Subscribers.Jobs.Payload.Models
{
    public class ArchivedCandidate : Candidate
    {
        public ArchivedCandidateStage Stage { get; set; }
    }
}
