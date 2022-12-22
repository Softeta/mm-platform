using Contracts.Shared.Requests;

namespace Contracts.Candidate.Educations.Requests
{
    public class AddCandidateEducationRequest : CandidateEducationRequestBase
    {
        public AddFileCacheRequest? Certificate { get; set; }
    }
}
