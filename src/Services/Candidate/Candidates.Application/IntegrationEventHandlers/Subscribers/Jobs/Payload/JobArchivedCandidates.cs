using Candidates.Application.IntegrationEventHandlers.Subscribers.Jobs.Payload.Models;
using Domain.Seedwork.Enums;

namespace Candidates.Application.IntegrationEventHandlers.Subscribers.Jobs.Payload
{
    public class JobArchivedCandidates : JobCandidateBase
    {
        public IEnumerable<ArchivedCandidate> ArchivedCandidates { get; set; } = new List<ArchivedCandidate>();
    }
}
