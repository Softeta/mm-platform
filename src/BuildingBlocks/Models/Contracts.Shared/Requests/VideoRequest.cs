using Custom.Attributes;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Contracts.Shared.Requests
{
    public class VideoRequest
    {
        [Required]
        [MaxVideoSize]
        [SupportedVideoTypes]
        public IFormFile File { get; set; } = null!;
    }
}
