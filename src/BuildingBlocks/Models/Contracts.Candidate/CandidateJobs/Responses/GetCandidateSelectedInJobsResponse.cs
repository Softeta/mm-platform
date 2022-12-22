namespace Contracts.Candidate.CandidateJobs.Responses
{
    public record GetCandidateSelectedInJobsResponse(
        int Count,
        List<GetCandidateSelectedInJobBriefResponse> SelectedInJobs);
}
