using System;
using System.Collections.Generic;

namespace ElasticSearch.Sync.Events.Models.CandidateJobsChanged
{
    internal class CandidateJobsChangedPayload
    {
        public Guid CandidateId { get; set; }
        public bool IsShortlisted { get; set; }
        public IEnumerable<CandidateInJob>? SelectedInJobs { get; set; }
        public IEnumerable<CandidateInJob>? ArchivedInJobs { get; set; }
    }
}
