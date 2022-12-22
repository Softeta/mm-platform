using Candidates.Application.IntegrationEventHandlers.Subscribers.Jobs.Payload.Models;
using Domain.Seedwork.Enums;

namespace Candidates.Application.IntegrationEventHandlers.Subscribers.Jobs.Payload
{
    public class JobCandidateJobStage
    {
        public Guid JobId { get; set; }
        public JobStage Stage { get; set; }

        public IEnumerable<ArchivedCandidate> ArchivedCandidates { get; set; } = new List<ArchivedCandidate>();
        public IEnumerable<SelectedCandidate> SelectedCandidates { get; set; } = new List<SelectedCandidate>();
    }
}
