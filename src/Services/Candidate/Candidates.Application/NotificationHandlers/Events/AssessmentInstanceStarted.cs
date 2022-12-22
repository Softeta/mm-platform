namespace Candidates.Application.NotificationHandlers.Events
{
    public class AssessmentInstanceStarted
    {
        public string Notification { get; set; } = null!;
        public string ParticipantId { get; set; } = null!;
        public string PackageTypeId { get; set; } = null!;
        public string PackageInstanceId { get; set; } = null!;
        public string AssessmentTypeId { get; set; } = null!;
        public DateTimeOffset AssessmentInstanceStartTime { get; set; }
    }
}
