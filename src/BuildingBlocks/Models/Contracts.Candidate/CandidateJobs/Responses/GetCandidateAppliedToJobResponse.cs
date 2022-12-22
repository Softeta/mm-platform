using Contracts.Shared;
using Domain.Seedwork.Enums;

namespace Contracts.Candidate.CandidateJobs.Responses
{
    public class GetCandidateAppliedToJobResponse
    {
        public Guid JobId { get; set; }
        public CompanyResponse Company { get; set; } = null!;
        public Position Position { get; set; } = null!;
        public JobStage JobStage { get; set; }
        public WorkType? Freelance { get; set; }
        public WorkType? Permanent { get; set; }
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? DeadlineDate { get; set; }
    }
}
