using Contracts.Shared.Requests;

namespace FrontOffice.Bff.API.Areas.Candidate.Models.Candidate.Requests
{
    public class UpdateCandidatePictureRequest
    {
        public UpdateFileCacheRequest Picture { get; set; } = null!;

    }
}
