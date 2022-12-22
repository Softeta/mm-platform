using Contracts.Shared;

namespace FrontOffice.Bff.API.Candidate.Models.Candidate.Requests
{
    public class UpdateCandidateCompetenciesRequest
    {
        public ICollection<Skill>? Skills { get; set; }

        public ICollection<Industry>? Industries { get; set; }

        public ICollection<Language>? Languages { get; set; }
    }
}
