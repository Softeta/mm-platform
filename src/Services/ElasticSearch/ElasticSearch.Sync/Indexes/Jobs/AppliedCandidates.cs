using ElasticSearch.Sync.Events.Models.JobCandidatesChanged;
using System.Collections.Generic;
using System.Linq;

namespace ElasticSearch.Sync.Indexes.Jobs
{
    internal class AppliedCandidates
    {
        public string Id { get; set; } = null!;
        public ICollection<string> AppliedCandidateIds { get; set; } = new List<string>();

        public static AppliedCandidates FromEvent(JobCandidatesChangedPayload payload)
        {
            return new AppliedCandidates
            {
                Id = payload.JobId.ToString(),
                AppliedCandidateIds = payload.SelectedCandidates?
                    .Where(x => x.HasApplied)
                    .Select(x => x.CandidateId.ToString())?
                    .ToList() ?? new List<string>()
            };
        }

    }
}
