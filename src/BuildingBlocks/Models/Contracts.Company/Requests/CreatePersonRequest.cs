using Contracts.Shared.Services.Company.Requests.Models;
using Domain.Seedwork.Enums;
using System.ComponentModel.DataAnnotations;

namespace Contracts.Shared.Services.Company.Requests
{
    public class CreatePersonRequest : PersonCreate
    {
        [Required]
        public ContactPersonRole Role { get; set; }
    }
}
