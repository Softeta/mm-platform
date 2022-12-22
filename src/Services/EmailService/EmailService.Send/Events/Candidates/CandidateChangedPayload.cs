using Domain.Seedwork.Enums;
using EmailService.Send.Events.Candidates.Models;
using System;

namespace EmailService.Send.Events.Candidates
{
    internal class CandidateChangedPayload
    {
        public Guid Id { get; set; }
        public SystemLanguage? SystemLanguage { get; set; }
        public Email Email { get; set; } = null!;
        public string? FirstName { get; set; }
    }
}
