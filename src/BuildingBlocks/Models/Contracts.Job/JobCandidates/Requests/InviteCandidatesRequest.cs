namespace Contracts.Job.JobCandidates.Requests
{
    public class InviteCandidatesRequest
    {
        public IEnumerable<Guid> CandidateIds { get; set; } = new List<Guid>();
    }
}
