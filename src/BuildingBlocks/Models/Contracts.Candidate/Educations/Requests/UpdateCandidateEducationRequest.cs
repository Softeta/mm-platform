using Contracts.Shared.Requests;

namespace Contracts.Candidate.Educations.Requests
{
    public class UpdateCandidateEducationRequest : CandidateEducationRequestBase
    {
        public UpdateFileCacheRequest Certificate { get; set; } = null!;
    }
}
