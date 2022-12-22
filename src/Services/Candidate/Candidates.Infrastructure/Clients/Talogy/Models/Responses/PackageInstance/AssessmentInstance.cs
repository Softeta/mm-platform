namespace Candidates.Infrastructure.Clients.Talogy.Models.Responses.PackageInstance
{
    public class AssessmentInstance
    {
        public string AssessmentTypeId { get; set; } = null!;
        public TalogyAssessmentStatus? Status { get; set; }
        public DateTimeOffset? AssessmentInstanceStartTime { get; set; } = null!;
        public DateTimeOffset? AssessmentInstanceCompleteTime { get; set; } = null!;
        public ICollection<AssessmentInstanceScore> AssessmentInstanceScores { get; set; } = new List<AssessmentInstanceScore>();
    }

    public enum TalogyAssessmentStatus
    {
        Invited = 0,
        Started = 1,
        Completed = 2,
        Locked = 3
    }
}
