using System;

namespace EmailService.Send.Events.CandidateJobs.Models
{
    internal class CandidateSelectedInJob
    {
        public Guid JobId { get; set; }
        public DateTimeOffset? InvitedAt { get; set; }
    }
}
