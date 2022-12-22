using Contracts.Shared;
using Contracts.Shared.Responses;

namespace Contracts.Job.Companies.Responses
{
    public class CompanyResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public Address? Address { get; set; }

        public string? Description { get; set; }

        public ImageResponse? Logo { get; set; }

        public IEnumerable<JobContactPersonResponse> ContactPersons { get; set; } = new List<JobContactPersonResponse>();
    }
}
