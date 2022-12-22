using Domain.Seedwork;
using Domain.Seedwork.Enums;

namespace Candidates.Domain.Aggregates.CandidateTestsAggregate.ValueObjects
{
    public class PersonalityAssessment : ValueObject<PersonalityAssessment>
    {
        public string PackageInstanceId { get; init; } = null!;
        public string PackageTypeId { get; init; } = null!;
        public AssessmentStatus Status { get; init; }
        public string Url { get; init; } = null!;
        public DateTimeOffset? StartedAt { get; init; }
        public DateTimeOffset? CompletedAt { get; init; }
        public PersonalityScores? Scores { get; init; }

        private PersonalityAssessment() { }

        public PersonalityAssessment(
            string packageInstanceId,
            string packageTypeId,
            AssessmentStatus status,
            string url,
            DateTimeOffset? startedAt,
            DateTimeOffset? completedAt,
            PersonalityScores? scores)
        {
            PackageInstanceId = packageInstanceId;
            PackageTypeId = packageTypeId;
            Status = status;
            Url = url;
            StartedAt = startedAt;
            CompletedAt = completedAt;
            Scores = scores;
        }

        public static PersonalityAssessment CreateInvited(
            string packageInstanceId,
            string packageTypeId,
            string url)
        {
            return new PersonalityAssessment(
                packageInstanceId,
                packageTypeId,
                AssessmentStatus.Invited,
                url,
                null,
                null,
                null);
        }

        public static PersonalityAssessment CreateStarted(
            string packageInstanceId,
            string packageTypeId,
            string url,
            DateTimeOffset startedAt)
        {
            return new PersonalityAssessment(
                packageInstanceId,
                packageTypeId,
                AssessmentStatus.Started,
                url,
                startedAt,
                null,
                null);
        }

        public static PersonalityAssessment CreateCompleted(
            string packageInstanceId,
            string packageTypeId,
            string url,
            DateTimeOffset startedAt,
            DateTimeOffset completedAt,
            PersonalityScores? scores)
        {
            return new PersonalityAssessment(
                packageInstanceId,
                packageTypeId,
                AssessmentStatus.Completed,
                url,
                startedAt,
                completedAt,
                scores);
        }

        public static PersonalityAssessment CreateScored(
            string packageInstanceId,
            string packageTypeId,
            string url,
            DateTimeOffset startedAt,
            DateTimeOffset completedAt,
            PersonalityScores? scores)
        {
            return new PersonalityAssessment(
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
