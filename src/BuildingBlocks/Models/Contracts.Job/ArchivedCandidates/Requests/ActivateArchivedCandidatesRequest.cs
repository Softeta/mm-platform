namespace Contracts.Job.ArchivedCandidates.Requests
{
    public class ActivateArchivedCandidatesRequest
    {
        public List<Guid> CandidateIds { get; set; } = new();
    }
}
