using Contracts.Shared;
using Contracts.Shared.Responses;

namespace FrontOffice.Bff.API.Areas.Candidate.Models.Candidate.Requests
{
    public class UpdateCandidateContactInformationRequest
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public PhoneFullResponse? Phone { get; set; }

        public AddressWithLocation? Address { get; set; }
    }
}
