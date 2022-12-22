using System;

namespace ElasticSearch.Sync.Events.Models.JobCandidatesChanged
{
    internal class SelectedCandidate
    {
        public Guid CandidateId { get; set; }
        public bool HasApplied { get; set; }
    }
}
