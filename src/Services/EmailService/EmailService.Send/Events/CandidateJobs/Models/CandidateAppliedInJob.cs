using System;

namespace EmailService.Send.Events.CandidateJobs.Models
{
    internal class CandidateAppliedInJob
    {
        public Guid JobId { get; set; }
        public Position? Position { get; set; }
        public Guid CandidateId { get; set; }
        public Company? Company { get; set; }
    }
}
