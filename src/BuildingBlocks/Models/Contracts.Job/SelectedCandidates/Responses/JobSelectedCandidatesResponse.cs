namespace Contracts.Job.SelectedCandidates.Responses
{
    public record JobSelectedCandidatesResponse(
        int Count,
        List<JobSelectedCandidateResponse> Candidates);
}
