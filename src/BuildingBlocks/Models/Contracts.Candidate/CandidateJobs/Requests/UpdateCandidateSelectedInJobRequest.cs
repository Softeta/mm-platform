using Contracts.Shared.Requests;

namespace Contracts.Candidate.CandidateJobs.Requests
{
    public class UpdateCandidateSelectedInJobRequest
    {
        public UpdateFileCacheRequest? MotivationVideo { get; set; }
        public string? CoverLetter { get; set; }
    }
}
