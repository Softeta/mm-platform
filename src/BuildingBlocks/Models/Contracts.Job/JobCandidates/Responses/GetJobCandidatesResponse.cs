using Contracts.Job.ArchivedCandidates.Responses;
using Contracts.Job.SelectedCandidates.Responses;
using Domain.Seedwork.Enums;

namespace Contracts.Job.JobCandidates.Responses
{
    public class GetJobCandidatesResponse
    {
        public JobStage Stage { get; set; }
        public DateTimeOffset? ShortListActivatedAt { get; set; }
        public IEnumerable<JobSelectedCandidateResponse> SelectedCandidates { get; set; } = new List<JobSelectedCandidateResponse>();
        public IEnumerable<JobArchivedCandidateResponse> ArchivedCandidates { get; set; } = new List<JobArchivedCandidateResponse>();
    }
}
