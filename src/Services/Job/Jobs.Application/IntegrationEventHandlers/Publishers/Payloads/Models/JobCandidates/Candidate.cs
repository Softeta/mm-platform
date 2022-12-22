using Domain.Seedwork.Enums;

namespace Jobs.Application.IntegrationEventHandlers.Publishers.Payloads.Models.JobCandidates
{
    public abstract class Candidate
    {
        public Guid CandidateId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? PictureUri { get; set; }
        public DateTimeOffset? InvitedAt { get; set; }
        public bool HasApplied { get; set; }
        public SystemLanguage? SystemLanguage { get; set; }
    }
}
