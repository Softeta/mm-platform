using Custom.Attributes;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Contracts.Shared.Requests
{
    public class DocumentRequest
    {
        [Required]
        [MaxDocumentSize]
        [SupportedDocumentTypes]
        public IFormFile File { get; set; } = null!;
    }
}
