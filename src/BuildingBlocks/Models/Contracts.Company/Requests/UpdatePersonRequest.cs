using Contracts.Shared.Requests;
using Contracts.Shared.Services.Company.Requests.Models;
using Domain.Seedwork.Enums;
using System.ComponentModel.DataAnnotations;

namespace Contracts.Shared.Services.Company.Requests
{
    public class UpdatePersonRequest : Person
    {
        [Required]
        public ContactPersonRole Role { get; set; }

        [Required]
        public UpdateFileCacheRequest Picture { get; set; } = null!;
    }
}
