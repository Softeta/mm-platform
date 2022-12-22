using Contracts.Shared;
using Domain.Seedwork.Enums;

namespace Contracts.Candidate.CandidateJobs.Responses
{
    public class GetCandidateSelectedInJobBriefResponse
    {
        public Guid Id { get; set; }
        public Guid JobId { get; set; }
        public CompanyResponse Company { get; set; } = null!;
        public Position Position { get; set; } = null!;
        public bool HasMotivationVideo { get; set; }
        public string? CoverLetter { get; set; }
        public bool HasApplied { get; set; }
        public JobStage JobStage { get; set; }
        public WorkType? Freelance { get; set; }
        public WorkType? Permanent { get; set; }
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? DeadlineDate { get; set; }
        public SelectedCandidateStage Stage { get; set; }
    }
}
