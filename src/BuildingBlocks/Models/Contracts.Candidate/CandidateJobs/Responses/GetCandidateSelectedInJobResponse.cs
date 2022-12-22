using Contracts.Shared;
using Contracts.Shared.Responses;
using Domain.Seedwork.Enums;

namespace Contracts.Candidate.CandidateJobs.Responses
{
    public class GetCandidateSelectedInJobResponse
    {
        public Guid Id { get; set; }
        public Guid JobId { get; set; }
        public Guid CandidateId { get; set; }
        public CompanyResponse Company { get; set; } = null!;
        public Position Position { get; set; } = null!;
        public JobStage JobStage { get; set; }
        public WorkType? Freelance { get; set; }
        public WorkType? Permanent { get; set; }
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? DeadlineDate { get; set; }
        public DocumentResponse? MotivationVideo { get; set; }
        public string? CoverLetter { get; set; }
        public SelectedCandidateStage Stage { get; set; }
        public bool IsJobArchived { get; set; }
        public bool HasApplied { get; set; }
    }
}
