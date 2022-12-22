using Contracts.Candidate.Notes.Responses;
using Contracts.Shared;
using Contracts.Shared.Responses;
using Domain.Seedwork.Enums;

namespace Contracts.Candidate.Candidates.Responses
{
    public class GetCandidateBriefResponse
    {
        public Guid Id { get; set; }
        public ImageResponse? Picture { get; set; }
        public string? FullName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public bool? IsEmailVerified { get; set; }
        public PhoneFullResponse? Phone { get; set; }
        public string? LinkedInUrl { get; set; }
        public Position? CurrentPosition { get; set; }
        public AddressWithLocation? Address { get; set; }
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public string? Currency { get; set; }
        public CandidateFreelance? Freelance { get; set; }
        public CandidatePermanent? Permanent { get; set; }
        public bool OpenForOpportunities { get; set; }
        public bool IsShortlisted { get; set; }
        public NoteResponse? Note { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public bool IsHired { get; set; }
        public SystemLanguage? SystemLanguage { get; set; }
    }
}
