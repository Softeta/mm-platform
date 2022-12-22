using Contracts.Shared.Requests;

namespace FrontOffice.Bff.API.Areas.Candidate.Models.Candidate.Requests
{
    public class UpdateCandidateVideoRequest
    {
        public UpdateFileCacheRequest Video { get; set; } = null!;
    }
}
