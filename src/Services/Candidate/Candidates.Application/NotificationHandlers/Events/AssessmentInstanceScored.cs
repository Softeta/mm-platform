using Candidates.Infrastructure.Clients.Talogy.Models.Responses.PackageInstance;

namespace Candidates.Application.NotificationHandlers.Events
{
    public class AssessmentInstanceScored : AssessmentInstanceCompleted
    {
        public ICollection<AssessmentInstanceScore> AssessmentInstanceScores { get; set; } = new List<AssessmentInstanceScore>();
    }
}
