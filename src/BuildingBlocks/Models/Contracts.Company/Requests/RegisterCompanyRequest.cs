using Contracts.Shared.Services.Company.Requests.Models;
using System.ComponentModel.DataAnnotations;

namespace Contracts.Shared.Services.Company.Requests
{
    public class RegisterCompanyRequest
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string RegistrationNumber { get; set; } = null!;

        [Required]
        public AddressWithLocation Address { get; set; } = null!;

        public PersonCreate Person { get; set; } = null!;

        public IEnumerable<Industry> Industries { get; set; } = new List<Industry>();
    }
}
