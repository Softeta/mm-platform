using Candidates.Application.IntegrationEventHandlers.Subscribers.Jobs.Payload.Models;

namespace Candidates.Application.IntegrationEventHandlers.Subscribers.Jobs.Payload
{
    public class JobSelectedCandidates : JobCandidateBase
    {
        public IEnumerable<SelectedCandidate> SelectedCandidates { get; set; } = new List<SelectedCandidate>();
    }
}
