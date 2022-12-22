using System;

namespace EmailService.Send.Events.JobCandidates.Models
{
    internal class Job
    {
        public Guid JobId { get; set; }
        public Company? Company { get; set; }
        public Position? Position { get; set; }
    }
}
