using Contracts.Shared;
using Domain.Seedwork.Enums;

namespace Contracts.Job.Companies.Requests
{
    public class CreateJobCompanyRequest
    {
        public Guid Id { get; set; }
        public CompanyStatus Status { get; set; }
        public string Name { get; set; } = null!;
        public Address? Address { get; set; }
        public string? Description { get; set; }
        public string? LogoUri { get; set; }
        public IEnumerable<JobContactPersonRequest> ContactPersons { get; set; } = new List<JobContactPersonRequest>();
    }
}
