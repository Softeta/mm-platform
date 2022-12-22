using Domain.Seedwork.Enums;

namespace Jobs.Application.IntegrationEventHandlers.Subscribers.CandidateJobs.Payload.Models.Candidates
{
    public class Candidate
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? PictureUri { get; set; }
        public Position? Position { get; set; } = null!;
        public SystemLanguage? SystemLanguage { get; set; }
    }
}
