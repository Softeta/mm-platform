using Contracts.Shared.Requests;

namespace FrontOffice.Bff.API.Areas.Candidate.Models.Candidate.Requests
{
    public class UpdateCandidateCurriculumVitaeRequest
    {
        public string? Bio { get; set; }

        public UpdateFileCacheRequest CurriculumVitae { get; set; } = null!;

    }
}
