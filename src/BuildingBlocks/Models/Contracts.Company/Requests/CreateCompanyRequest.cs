using Contracts.Shared.Requests;
using Contracts.Shared.Services.Company.Requests.Models;
using System.ComponentModel.DataAnnotations;

namespace Contracts.Shared.Services.Company.Requests
{
    public class CreateCompanyRequest
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string RegistrationNumber { get; set; } = null!;

        public string? WebsiteUrl { get; set; }

        public string? LinkedInUrl { get; set; }

        public string? GlassdoorUrl { get; set; }

        public AddFileCacheRequest? Logo { get; set; }

        [Required]
        public AddressWithLocation Address { get; set; } = null!;

        public PersonCreate Person { get; set; } = null!;

        public IEnumerable<Industry> Industries { get; set; } = new List<Industry>();
    }
}
