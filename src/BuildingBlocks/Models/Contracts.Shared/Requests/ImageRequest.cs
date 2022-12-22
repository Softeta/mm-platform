using Custom.Attributes;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Contracts.Shared.Requests
{
    public class ImageRequest
    {
        [Required]
        [MaxImageSize]
        [SupportedImageTypes]
        public IFormFile File { get; set; } = null!;
    }
}
