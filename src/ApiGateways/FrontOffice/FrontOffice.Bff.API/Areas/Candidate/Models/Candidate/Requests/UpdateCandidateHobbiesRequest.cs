using Contracts.Shared;

namespace FrontOffice.Bff.API.Candidate.Models.Candidate.Requests
{
    public class UpdateCandidateHobbiesRequest
    {
        public ICollection<Hobby> Hobbies { get; set; } = new List<Hobby>();
    }
}
