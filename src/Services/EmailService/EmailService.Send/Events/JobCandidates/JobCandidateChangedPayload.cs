using Domain.Seedwork.Enums;
using EmailService.Send.Events.JobCandidates.Models;
using System;

namespace EmailService.Send.Events.JobCandidates
{
    internal class JobCandidateChangedPayload
    {
        public Job? Job { get; set; } 
        public Guid CandidateId { get; set; }
        public string? FirstName { get; set; }
        public string? Email { get; set; }
        public SystemLanguage? SystemLanguage { get; set; }
    }
}
