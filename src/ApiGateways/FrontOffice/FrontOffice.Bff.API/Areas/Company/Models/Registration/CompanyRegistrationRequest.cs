using Contracts.Shared;
using Contracts.Shared.Services.Company.Requests;
using Contracts.Shared.Services.Company.Requests.Models;
using FrontOffice.Bff.API.Areas.Company.Models.Shared;
using System.ComponentModel.DataAnnotations;

namespace FrontOffice.Bff.API.Areas.Company.Models.Registration
{
    public class CompanyRegistrationRequest
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string RegistrationNumber { get; set; } = null!;

        [Required]
        public AddressWithLocation Address { get; set; } = null!;

        [Required]
        public Person Person { get; set; } = null!;

        public IEnumerable<Industry> Industries { get; set; } = new List<Industry>();

        public InitializeJobRequest Job { get; set; } = null!;

        public RegisterCompanyRequest ToCompanyCreateRequest(string UserEmail)
        {
            return new RegisterCompanyRequest
            {
                Name = Name,
                RegistrationNumber = RegistrationNumber,
                Address = Address,
                Industries = Industries,
                Person = new PersonCreate
                {
                    Email = UserEmail,
                    FirstName = Person.FirstName,
                    LastName = Person.LastName,
                    Position = Person.Position,
                    Phone = Person.Phone,
                }
            };
        }
    }
}
