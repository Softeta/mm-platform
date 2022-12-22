using Contracts.Shared;

namespace Contracts.Job.Companies.Requests
{
    public class UpdateJobCompanyRequest
    {
        public Address? Address { get; set; }
        public string? Description { get; set; }
        public Guid MainContactId { get; set; }
        public IEnumerable<JobContactPersonRequest> ContactPersonsToAdd { get; set; } = new List<JobContactPersonRequest>();
        public IEnumerable<Guid> ContactPersonsToRemove { get; set; } = new List<Guid>();
    }
}
