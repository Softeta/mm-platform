using Candidates.Domain.Aggregates.CandidateTestsAggregate.ValueObjects;
using Domain.Seedwork;
using Domain.Seedwork.Enums;
using Domain.Seedwork.Exceptions;

namespace Candidates.Domain.Aggregates.CandidateTestsAggregate
{
    public class CandidateTests : AggregateRoot
    {
        public Guid ExternalId { get; init; }
        public long? CandidateOldPlatformId { get; init; }
        public LogicalAssessment? LogicalAssessment { get; private set; }
        public PersonalityAssessment? PersonalityAssessment { get; private set; }
        public Raport? PapiDynamicWheel { get; private set; }
        public Raport? PapiGeneralFeedback { get; private set; }
        public Raport? LgiGeneralFeedback { get; private set; }

        private CandidateTests() { }

        public CandidateTests(Guid candidateId, Guid externalId, long? candidateOldPlatformId)
        {
            Id = candidateId;
            ExternalId = externalId;
            CandidateOldPlatformId = candidateOldPlatformId;
            CreatedAt = DateTimeOffset.UtcNow;
        }

        public void CreateLogicalAssessment(
            string packageInstanceId, 
            string packageTypeId, 
            string testUrl)
        {
            if (LogicalAssessment is not null)
            {
                throw new ConflictException($"Logical test {packageInstanceId} already exists", ErrorCodes.Conflict.LogicalTestAlreadyExists);
            }

            LogicalAssessment = LogicalAssessment.CreateInvited(packageInstanceId, packageTypeId, testUrl);
        }

        public void CreatePersonalityAssessment(
            string packageInstanceId,
            string packageTypeId,
            string testUrl)
        {
            if (PersonalityAssessment is not null)
            {
                throw new ConflictException($"Personality test {packageInstanceId} already exists", ErrorCodes.Conflict.PersonalityTestAlreadyExists);
            }

            PersonalityAssessment = PersonalityAssessment.CreateInvited(packageInstanceId, packageTypeId, testUrl);
        }

        public void StartLogicalAssessment(
            string packageInstanceId, 
            string packageTypeId,
            DateTimeOffset startedAt)
        {
            if (LogicalAssessment is null) return;
            if (LogicalAssessment.Status != AssessmentStatus.Invited) return;
         
            LogicalAssessment = LogicalAssessment.CreateStarted(
                packageInstanceId, 
                packageTypeId,
                LogicalAssessment.Url, 
                startedAt);
            return;
        }

        public void StartPersonalityAssessment(
            string packageInstanceId,
            string packageTypeId,
            DateTimeOffset startedAt)
        {
            if (PersonalityAssessment is null) return;
            if (PersonalityAssessment.Status != AssessmentStatus.Invited) return;

            PersonalityAssessment = PersonalityAssessment.CreateStarted(
                packageInstanceId,
                packageTypeId,
                PersonalityAssessment.Url,
                startedAt);
            return;     
        }

        public void CompleteLogicalAssessment(
            string packageInstanceId,
            string packageTypeId,
            DateTimeOffset startedAt,
            DateTimeOffset completedAt,
            LogicScores? scores)
        {
            if (LogicalAssessment is null) return;
            if (LogicalAssessment.Status == AssessmentStatus.Completed) return;
            if (LogicalAssessment.Status == AssessmentStatus.Scored) return;

            LogicalAssessment = LogicalAssessment.CreateCompleted(
                packageInstanceId,
                packageTypeId,
                LogicalAssessment.Url,
                startedAt,
                completedAt,
                scores);
        }

        public void CompletePersonalityAssessment(
            string packageInstanceId,
            string packageTypeId,
            DateTimeOffset startedAt,
            DateTimeOffset completedAt,
            PersonalityScores? scores)
        {
            if (PersonalityAssessment is null) return;
            if (PersonalityAssessment.Status == AssessmentStatus.Completed) return;
            if (PersonalityAssessment.Status == AssessmentStatus.Scored) return;

            PersonalityAssessment = PersonalityAssessment.CreateCompleted(
                packageInstanceId,
                packageTypeId,
                PersonalityAssessment.Url,
                startedAt,
                completedAt,
                scores);
        }

        public void ScoreLogicalAssessment(
            string packageInstanceId,
            string packageTypeId,
            DateTimeOffset startedAt,
            DateTimeOffset completedAt,
            LogicScores? scores)
        {
            if (LogicalAssessment is null) return;
            if (LogicalAssessment.Status == AssessmentStatus.Scored) return;
            
            LogicalAssessment = LogicalAssessment.CreateScored(
                packageInstanceId,
                packageTypeId,
                LogicalAssessment.Url,
                startedAt,
                completedAt,
                scores);
        }

        public void ScorePersonalityAssessment(
            string packageInstanceId,
            string packageTypeId,
            DateTimeOffset startedAt,
            DateTimeOffset completedAt,
            PersonalityScores? scores)
        {
            if (PersonalityAssessment is null) return;
            if (PersonalityAssessment.Status == AssessmentStatus.Scored) return;

            PersonalityAssessment = PersonalityAssessment.CreateScored(
                packageInstanceId,
                packageTypeId,
                PersonalityAssessment.Url,
                startedAt,
                completedAt,
                scores);
        }

        public void AddPapiDynamicWheelRaport(string reportInstanceId, string url)
        {
            PapiDynamicWheel = new Raport(reportInstanceId, url);
        }

        public void AddPapiGeneralFeedbackRaport(string reportInstanceId, string url)
        {
            PapiGeneralFeedback = new Raport(reportInstanceId, url);
        }

        public void AddLgiGeneralFeedbackRaport(string reportInstanceId, string url)
        {
            LgiGeneralFeedback = new Raport(reportInstanceId, url);
        }

        public void RemovePersonalityTest()
        {
            PersonalityAssessment = null;
            PapiDynamicWheel = null;
            PapiGeneralFeedback = null;
        }

        public void RemoveLogicalTest()
        {
            LogicalAssessment = null;
            LgiGeneralFeedback = null;
        }
    }
}
