using Contracts.Shared.Requests;
using System.ComponentModel.DataAnnotations;

namespace Contracts.Shared.Services.Company.Requests.Models
{
    public class Person
    {
        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        public Position? Position { get; set; }

        public PhoneRequest? Phone { get; set; }
    }
}
