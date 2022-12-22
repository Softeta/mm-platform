using EmailService.Send.Events.CandidateJobs.Models;
using System;
using System.Collections.Generic;

namespace EmailService.Send.Events.CandidateJobs
{
    internal class CandidateJobPayload
    {
        public Guid CandidateId { get; set; }
        public Guid JobId { get; set; }
        public Candidate? Candidate { get; set; }
        public IEnumerable<CandidateSelectedInJob> SelectedInJobs = new List<CandidateSelectedInJob>();
        public IEnumerable<CandidateAppliedInJob> AppliedInJobs = new List<CandidateAppliedInJob>();
    }
}
