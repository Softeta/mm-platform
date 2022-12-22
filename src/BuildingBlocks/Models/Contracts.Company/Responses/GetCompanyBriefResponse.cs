using Contracts.Shared;
using Contracts.Shared.Responses;

namespace Contracts.Company.Responses
{
    public class GetCompanyBriefResponse
    {
        public Guid Id { get; set; }
        public string RegistrationNumber { get; set; } = null!;
        public string Name { get; set; } = null!;
        public ImageResponse? Logo { get; set; }
        public AddressWithLocation? Address { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public IEnumerable<Industry> Industries { get; set; } = new List<Industry>();
    }
}
