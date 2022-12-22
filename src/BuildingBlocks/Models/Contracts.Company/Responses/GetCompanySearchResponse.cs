using Contracts.Company.Responses.ContactPersons;
using Contracts.Shared;
using Contracts.Shared.Responses;

namespace Contracts.Company.Responses
{
    public class GetCompanySearchResponse
    {
        public Guid Id { get; set; }
        public string RegistrationNumber { get; set; } = null!;
        public string Name { get; set; } = null!;
        public ImageResponse? Logo { get; set; }
        public AddressWithLocation? Address { get; set; }
        public ICollection<GetContactPersonResponse> ContactPersons { get; set; } = new List<GetContactPersonResponse>();
    }
}
