using Candidates.Application.IntegrationEventHandlers.Publishers.Payloads.Models.Shared;
using Domain.Seedwork.Enums;

namespace Candidates.Application.IntegrationEventHandlers.Publishers.Payloads.Models.CandidateJobs
{
    internal class Candidate
    {
        public string? FirstName { get; set; } = null!;
        public string? LastName { get; set; } = null!;
        public string? Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? PictureUri { get; set; }
        public Position? Position { get; set; }
        public SystemLanguage? SystemLanguage { get; set; }
    }
}
