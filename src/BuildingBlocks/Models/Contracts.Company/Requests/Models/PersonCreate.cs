using Contracts.Shared.Requests;
using Custom.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Contracts.Shared.Services.Company.Requests.Models
{
    public class PersonCreate : Person
    {
        [Required]
        public string Email { get; set; } = null!;

        [MaxImageSize]
        [SupportedImageTypes]
        public AddFileCacheRequest? Picture { get; set; }
    }
}
