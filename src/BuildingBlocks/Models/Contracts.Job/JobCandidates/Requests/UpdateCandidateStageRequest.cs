using Domain.Seedwork.Enums;

namespace Contracts.Job.JobCandidates.Requests
{
    public class UpdateCandidateStageRequest
    {
        public SelectedCandidateStage Stage { get; set; }
        public IEnumerable<Guid> CandidateIds { get; set; } = new List<Guid>();
    }
}
