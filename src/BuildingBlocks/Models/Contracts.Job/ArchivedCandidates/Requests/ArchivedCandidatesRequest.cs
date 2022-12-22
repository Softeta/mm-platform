using Domain.Seedwork.Enums;

namespace Contracts.Job.ArchivedCandidates.Requests
{
    public class ArchivedCandidatesRequest
    {
        public ArchivedCandidateStage Stage { get; set; }
        public List<Guid> CandidateIds { get; set; } = new();
    }
}
