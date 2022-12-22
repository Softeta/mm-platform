using Contracts.Company.Responses.ContactPersons;
using Contracts.Shared;
using Contracts.Shared.Responses;
using Domain.Seedwork.Enums;

namespace Contracts.Company.Responses
{
    public class GetCompanyResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? RegistrationNumber { get; set; }
        public CompanyStatus Status { get; set; }
        public string? WebsiteUrl { get; set; }
        public string? LinkedInUrl { get; set; }
        public string? GlassdoorUrl { get; set; }
        public ImageResponse? Logo { get; set; }
        public AddressWithLocation? Address { get; set; }
        public ICollection<GetContactPersonResponse> ContactPersons { get; set; } = new List<GetContactPersonResponse>();
        public ICollection<Industry> Industries { get; set; } = new List<Industry>();
    }
}
