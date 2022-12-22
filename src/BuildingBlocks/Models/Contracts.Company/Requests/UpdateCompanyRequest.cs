using Contracts.Shared.Requests;
using System.ComponentModel.DataAnnotations;

namespace Contracts.Shared.Services.Company.Requests
{
    public class UpdateCompanyRequest
    {
        public string? WebsiteUrl { get; set; }

        public string? LinkedInUrl { get; set; }

        public string? GlassdoorUrl { get; set; }

        [Required]
        public UpdateFileCacheRequest Logo { get; set; } = null!;

        [Required]
        public AddressWithLocation Address { get; set; } = null!;

        public IEnumerable<Industry> Industries { get; set; } = new List<Industry>();
    }
}
