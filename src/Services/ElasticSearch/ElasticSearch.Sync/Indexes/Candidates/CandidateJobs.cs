using System.Collections.Generic;
using System.Linq;
using ElasticSearch.Sync.Events.Models.CandidateJobsChanged;

namespace ElasticSearch.Sync.Indexes.Candidates
{
    internal class CandidateJobs
    {
        public string Id { get; set; } = null!;
        public ICollection<string> JobIds { get; set; } = new List<string>();

        public static CandidateJobs FromEvent(CandidateJobsChangedPayload payload)
        {
            return new CandidateJobs
            {
                Id = payload.CandidateId.ToString(),
                JobIds = GetAllJobIds(payload.SelectedInJobs, payload.ArchivedInJobs)
            };
        }

        private static List<string> GetAllJobIds(
            IEnumerable<CandidateInJob>? selectedInJobs,
            IEnumerable<CandidateInJob>? archivedInJobs)
        {
            var result = new List<string>();

            if (selectedInJobs != null)
            {
                result.AddRange(selectedInJobs.Select(
                    selectedInJob => selectedInJob.JobId.ToString())
                );
            }

            if (archivedInJobs != null)
            {
                result.AddRange(archivedInJobs.Select(
                    archivedInJob => archivedInJob.JobId.ToString())
                );
            }

            return result;
        }
    }
}
