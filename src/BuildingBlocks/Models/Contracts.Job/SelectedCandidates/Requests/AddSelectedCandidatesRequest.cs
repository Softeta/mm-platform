namespace Contracts.Job.SelectedCandidates.Requests
{
    public class AddSelectedCandidatesRequest
    {
        public List<JobSelectedCandidateRequest> SelectedCandidates { get; set; } = null!;
    }
}
