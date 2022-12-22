namespace Contracts.Candidate.CandidateJobs.Responses
{
    public record GetCandidateAppliedToJobsResponse(
       int Count,
       List<GetCandidateAppliedToJobResponse> AppliedToJobs);
}
