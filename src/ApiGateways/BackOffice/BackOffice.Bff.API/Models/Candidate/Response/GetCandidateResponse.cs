using Common = Contracts.Candidate.Candidates.Responses;

namespace BackOffice.Bff.API.Models.Candidate.Response
{
    public class GetCandidateResponse : Common.GetCandidateBriefResponse
    {
        public double Score { get; set; }
    }
}
