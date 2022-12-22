using Domain.Seedwork.Enums;

namespace Contracts.Candidate.CandidateJobs.Requests
{
    public class CandidateApplyToJobRequest
    {
        public JobStage JobStage { get; set; }
        public Guid PositionId { get; set; }
        public string PositionCode { get; set; } = null!;
        public Guid? PositionAliasToId { get; set; }
        public string? PositionAliasToCode { get; set; }
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; } = null!;
        public string? CompanyLogo { get; set; }
        public WorkType? Freelance { get; set; }
        public WorkType? Permanent { get; set; }
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? DeadlineDate { get; set; }
    }
}
