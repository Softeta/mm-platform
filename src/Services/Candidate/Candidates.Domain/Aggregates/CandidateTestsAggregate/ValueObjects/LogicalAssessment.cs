using Domain.Seedwork;
using Domain.Seedwork.Enums;
using Newtonsoft.Json;

namespace Candidates.Domain.Aggregates.CandidateTestsAggregate.ValueObjects
{
    public class LogicalAssessment : ValueObject<LogicalAssessment>
    {
        public string PackageInstanceId { get; init; } = null!;
        public string PackageTypeId { get; init; } = null!;
        public AssessmentStatus Status { get; init; }
        public string Url { get; init; } = null!;
        public DateTimeOffset? StartedAt { get; init; }
        public DateTimeOffset? CompletedAt { get; init; }
        public LogicScores? Scores { get; init; }

        private LogicalAssessment() { }

        private LogicalAssessment(
            string packageInstanceId,
            string packageTypeId,
            AssessmentStatus status,
            string url,
            DateTimeOffset? startedAt,
            DateTimeOffset? completedAt,
            LogicScores? scores)
        {
            PackageInstanceId = packageInstanceId;
            PackageTypeId = packageTypeId;
            Status = status;
            Url = url;
            StartedAt = startedAt;
            CompletedAt = completedAt;
            Scores = scores;
        }

        public static LogicalAssessment CreateInvited(
            string packageInstanceId,
            string packageTypeId,
            string url)
        {
            return new LogicalAssessment(
                packageInstanceId,
                packageTypeId,
                AssessmentStatus.Invited,
                url,
                null,
                null,
                null);
        }

        public static LogicalAssessment CreateStarted(
            string packageInstanceId,
            string packageTypeId,
            string url,
            DateTimeOffset startedAt)
        {
            return new LogicalAssessment(
                packageInstanceId, 
                packageTypeId, 
                AssessmentStatus.Started, 
                url, 
                startedAt,
                null,
                null);
        }

        public static LogicalAssessment CreateCompleted(
            string packageInstanceId,
            string packageTypeId,
            string url,
            DateTimeOffset startedAt,
            DateTimeOffset completedAt,
            LogicScores? scores)
        {
            return new LogicalAssessment(
                packageInstanceId,
                packageTypeId,
                AssessmentStatus.Completed,
                url,
                startedAt,
                completedAt,
                scores);
        }

        public static LogicalAssessment CreateScored(
            string packageInstanceId,
            string packageTypeId,
            string url,
            DateTimeOffset startedAt,
            DateTimeOffset completedAt,
            LogicScores? scores)
        {
            return new LogicalAssessment(
                packageInstanceId,
                packageTypeId,
                AssessmentStatus.Scored,
                url,
                startedAt,
                completedAt,
                scores);
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return PackageInstanceId;
            yield return PackageTypeId;
            yield return Status;
            yield return Url; 
            yield return StartedAt;
            yield return CompletedAt;
            yield return Scores;
        }
    }
}
