using Contracts.Shared;

namespace BackOffice.Bff.API.Models.Job.Requests
{
    public class JobCompanyRequest
    {
        public Guid Id { get; set; }

        public Address? Address { get; set; }

        public string? Description { get; set; }

        public IEnumerable<AddJobContactPersonRequest> ContactPersons { get; set; } = new List<AddJobContactPersonRequest>();
    }
}
