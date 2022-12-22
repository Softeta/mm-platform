namespace Contracts.Candidate.Candidates.Responses
{
    public record GetCandidatesResponse(
        int Count,
        List<GetCandidateBriefResponse> Candidates);
}