using Domain.Seedwork.Enums;

namespace EmailService.Send.Events.CandidateJobs.Models
{
    internal class Candidate
    {
        public string? FirstName { get; set; }
        public string? Email { get; set; }
        public SystemLanguage? SystemLanguage { get; set; }
    }
}
