using Domain.Seedwork.Enums;
using EmailService.Send.Events.JobCandidates.Models;
using System;

namespace EmailService.Send.Events.JobCandidates
{
    internal class JobCandidatesShortlistActivatedPayload
    {
        public Job? JobCandidates { get; set; }
        public string? ContactEmail { get; set; }
        public string? ContactFirstName { get; set; }
        public SystemLanguage? ContactSystemLanguage { get; set; }
        public Guid? ContactExternalId { get; set; }
    }
}
