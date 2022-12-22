using Candidates.Application.IntegrationEventHandlers.Subscribers.Jobs.Payload.Models;

namespace Candidates.Application.IntegrationEventHandlers.Subscribers.Jobs.Payload
{
    public class JobCandidatesInformation : JobCandidateBase
    {
        public IEnumerable<ArchivedCandidate> ArchivedCandidates { get; set; } = new List<ArchivedCandidate>();
        public IEnumerable<SelectedCandidate> SelectedCandidates { get; set; } = new List<SelectedCandidate>();
    }
}
