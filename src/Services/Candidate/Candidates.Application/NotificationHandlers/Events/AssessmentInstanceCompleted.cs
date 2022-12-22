namespace Candidates.Application.NotificationHandlers.Events
{
    public class AssessmentInstanceCompleted : AssessmentInstanceStarted
    {
        public DateTimeOffset AssessmentInstanceCompleteTime { get; set; }
    }
}
