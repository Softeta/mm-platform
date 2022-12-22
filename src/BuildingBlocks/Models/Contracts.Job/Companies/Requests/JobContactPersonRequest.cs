using Contracts.Shared;
using Domain.Seedwork.Enums;

namespace Contracts.Job.Companies.Requests
{
    public class JobContactPersonRequest
    {
        public Guid Id { get; set; }
        public bool IsMainContact { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public Position? Position { get; set; }
        public string? PhoneNumber { get; set; }
        public string Email { get; set; } = null!;
        public string? PictureUri { get; set; }
        public SystemLanguage? SystemLanguage { get; set; }
        public Guid? ExternalId { get; set; }
    }
}
