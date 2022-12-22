using Domain.Seedwork.Enums;
using System.ComponentModel.DataAnnotations;

namespace Contracts.Candidate.Candidates.Requests
{
    public class RegisterMyselfRequest
    {
        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public Guid ExternalId { get; set; }

        public SystemLanguage? SystemLanguage { get; set; }

        [Required]
        public bool AcceptTermsAndConditions { get; set; }

        [Required]
        public bool AcceptMarketingAcknowledgement { get; set; }
    }
}
