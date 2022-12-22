namespace Contracts.Job.JobCandidates.Requests
{
    public class UpdateCandidatesRankingRequest
    {
        public IEnumerable<CandidateRanking> CandidatesRanking { get; set; } = new List<CandidateRanking>();
    }
}
