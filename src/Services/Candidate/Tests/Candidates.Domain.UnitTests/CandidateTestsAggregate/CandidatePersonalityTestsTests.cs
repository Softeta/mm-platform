using Candidates.Domain.Aggregates.CandidateTestsAggregate;
using Candidates.Domain.Aggregates.CandidateTestsAggregate.ValueObjects;
using Domain.Seedwork.Enums;
using Domain.Seedwork.Exceptions;
using System;
using Tests.Shared;
using Xunit;

namespace Candidates.Domain.UnitTests.CandidateTestsAggregate
{
    public class CandidatePersonalityTestsTests
    {
        [Theory, AutoMoqData]
        public void CreatePersonalityAssessment_ShouldInitializePersonalityAssessment(
           Guid candidateId,
           Guid externalId,
           long? candidateOldPlatformId,
           string packageInstanceId,
           string packageTypeId,
           string testUrl)
        {
            // Arrange
            var candidateTests = new CandidateTests(candidateId, externalId, candidateOldPlatformId);
            var expectedStatus = AssessmentStatus.Invited;

            // Act
            candidateTests.CreatePersonalityAssessment(packageInstanceId, packageTypeId, testUrl);

            // Assert
            Assert.NotNull(candidateTests.PersonalityAssessment);
            Assert.Equal(expectedStatus, candidateTests.PersonalityAssessment!.Status);
        }

        [Theory, AutoMoqData]
        public void CreatePersonalityAssessment_ShouldThrowConflictException(
            Guid candidateId,
            Guid externalId,
            long? candidateOldPlatformId,
            string packageInstanceId,
            string packageTypeId,
            string testUrl)
        {
            // Arrange
            var candidateTests = new CandidateTests(candidateId, externalId, candidateOldPlatformId);
            candidateTests.CreatePersonalityAssessment(packageInstanceId, packageTypeId, testUrl);

            // Act
            Action action = () => candidateTests.CreatePersonalityAssessment(packageInstanceId, packageTypeId, testUrl);

            // Assert
            Assert.Throws<ConflictException>(action);
        }

        [Theory, AutoMoqData]
        public void StartPersonalityAssessment_ShouldInitializeLogicalAssessment(
            Guid candidateId,
            Guid externalId,
            long? candidateOldPlatformId,
            string packageInstanceId,
            string packageTypeId,
            string testUrl,
            DateTimeOffset startedAt)
        {
            // Arrange
            var candidateTests = new CandidateTests(candidateId, externalId, candidateOldPlatformId);
            candidateTests.CreatePersonalityAssessment(packageInstanceId, packageTypeId, testUrl);
            var expectedStatus = AssessmentStatus.Started;

            // Act
            candidateTests.StartPersonalityAssessment(packageInstanceId, packageTypeId, startedAt);

            // Assert
            Assert.NotNull(candidateTests.PersonalityAssessment);
            Assert.Equal(expectedStatus, candidateTests.PersonalityAssessment!.Status);
        }

        [Theory, AutoMoqData]
        public void StartPersonalityAssessment_ShouldNotChangeIfWasStarted(
            Guid candidateId,
            Guid externalId,
            long? candidateOldPlatformId,
            string packageInstanceId,
            string packageTypeId,
            string testUrl,
            DateTimeOffset startedAt,
            DateTimeOffset secondStartedAt)
        {
            // Arrange
            var candidateTests = new CandidateTests(candidateId, externalId, candidateOldPlatformId);
            candidateTests.CreatePersonalityAssessment(packageInstanceId, packageTypeId, testUrl);
            candidateTests.StartPersonalityAssessment(packageInstanceId, packageTypeId, startedAt);
            var expectedStatus = AssessmentStatus.Started;

            // Act
            candidateTests.StartPersonalityAssessment(packageInstanceId, packageTypeId, secondStartedAt);

            // Assert
            Assert.NotNull(candidateTests.PersonalityAssessment);
            Assert.Equal(expectedStatus, candidateTests.PersonalityAssessment!.Status);
            Assert.Equal(startedAt, candidateTests.PersonalityAssessment!.StartedAt);
        }

        [Theory, AutoMoqData]
        public void CompletePersonalityAssessment_ShouldInitializePersonalityAssessment(
          Guid candidateId,
          Guid externalId,
          long? candidateOldPlatformId,
          string packageInstanceId,
          string packageTypeId,
          string testUrl,
          DateTimeOffset startedAt,
          DateTimeOffset completedAt,
          PersonalityScores scores)
        {
            // Arrange
            var candidateTests = new CandidateTests(candidateId, externalId, candidateOldPlatformId);
            candidateTests.CreatePersonalityAssessment(packageInstanceId, packageTypeId, testUrl);
            var expectedStatus = AssessmentStatus.Completed;

            // Act
            candidateTests.CompletePersonalityAssessment(
                packageInstanceId,
                packageTypeId,
                startedAt,
                completedAt,
                scores);

            // Assert
            Assert.NotNull(candidateTests.PersonalityAssessment);
            Assert.Equal(expectedStatus, candidateTests.PersonalityAssessment!.Status);
        }

        [Theory, AutoMoqData]
        public void CompletePersonalityAssessment_ShouldNotChangeIfWasCompleted(
           Guid candidateId,
           Guid externalId,
           long? candidateOldPlatformId,
           string packageInstanceId,
           string packageTypeId,
           string testUrl,
           DateTimeOffset startedAt,
           DateTimeOffset completedAt,
           PersonalityScores scores,
           DateTimeOffset secondCompletedAt)
        {
            // Arrange
            var candidateTests = new CandidateTests(candidateId, externalId, candidateOldPlatformId);

            candidateTests.CreatePersonalityAssessment(packageInstanceId, packageTypeId, testUrl);
            candidateTests.CompletePersonalityAssessment(
                packageInstanceId,
                packageTypeId,
                startedAt,
                completedAt,
                scores);

            var expectedStatus = AssessmentStatus.Completed;

            // Act
            candidateTests.CompletePersonalityAssessment(
                packageInstanceId,
                packageTypeId,
                startedAt,
                secondCompletedAt,
                scores);

            // Assert
            Assert.NotNull(candidateTests.PersonalityAssessment);
            Assert.Equal(completedAt, candidateTests.PersonalityAssessment!.CompletedAt);
            Assert.Equal(expectedStatus, candidateTests.PersonalityAssessment!.Status);
        }

        [Theory, AutoMoqData]
        public void ScorePersonalityAssessment_ShouldInitializePersonalityAssessment(
           Guid candidateId,
           Guid externalId,
           long? candidateOldPlatformId,
           string packageInstanceId,
           string packageTypeId,
           string testUrl,
           DateTimeOffset startedAt,
           DateTimeOffset completedAt,
           PersonalityScores scores)
        {
            // Arrange
            var candidateTests = new CandidateTests(candidateId, externalId, candidateOldPlatformId);
            candidateTests.CreatePersonalityAssessment(packageInstanceId, packageTypeId, testUrl);
            var expectedStatus = AssessmentStatus.Scored;

            // Act
            candidateTests.ScorePersonalityAssessment(
                packageInstanceId,
                packageTypeId,
                startedAt,
                completedAt,
                scores);

            // Assert
            Assert.NotNull(candidateTests.PersonalityAssessment);
            Assert.Equal(expectedStatus, candidateTests.PersonalityAssessment!.Status);
        }

        [Theory, AutoMoqData]
        public void ScorePersonalityAssessment_ShouldNotChangeIfWasScored(
          Guid candidateId,
          Guid externalId,
          long? candidateOldPlatformId,
          string packageInstanceId,
          string packageTypeId,
          string testUrl,
          DateTimeOffset startedAt,
          DateTimeOffset completedAt,
          PersonalityScores scores,
          DateTimeOffset secondCompletedAt)
        {
            // Arrange
            var candidateTests = new CandidateTests(candidateId, externalId, candidateOldPlatformId);

            candidateTests.CreatePersonalityAssessment(packageInstanceId, packageTypeId, testUrl);
            candidateTests.ScorePersonalityAssessment(
                packageInstanceId,
                packageTypeId,
                startedAt,
                completedAt,
                scores);

            var expectedStatus = AssessmentStatus.Scored;

            // Act
            candidateTests.ScorePersonalityAssessment(
                packageInstanceId,
                packageTypeId,
                startedAt,
                secondCompletedAt,
                scores);

            // Assert
            Assert.NotNull(candidateTests.PersonalityAssessment);
            Assert.Equal(completedAt, candidateTests.PersonalityAssessment!.CompletedAt);
            Assert.Equal(expectedStatus, candidateTests.PersonalityAssessment!.Status);
        }

        [Theory, AutoMoqData]
        public void AddPapiDynamicWheelRaport_ShouldInitializePapiDynamicWheelRaport(
           Guid candidateId,
           Guid externalId,
           long? candidateOldPlatformId,
           string reportInstanceId,
           string url)
        {
            // Arrange
            var candidateTests = new CandidateTests(candidateId, externalId, candidateOldPlatformId);

            // Act
            candidateTests.AddPapiDynamicWheelRaport(reportInstanceId, url);

            // Assert
            Assert.NotNull(candidateTests.PapiDynamicWheel);
        }

        [Theory, AutoMoqData]
        public void AddPapiGeneralFeedbackRaport_ShouldInitializePapiGeneralFeedbackRaport(
           Guid candidateId,
           Guid externalId,
           long? candidateOldPlatformId,
           string reportInstanceId,
           string url)
        {
            // Arrange
            var candidateTests = new CandidateTests(candidateId, externalId, candidateOldPlatformId);

            // Act
            candidateTests.AddPapiGeneralFeedbackRaport(reportInstanceId, url);

            // Assert
            Assert.NotNull(candidateTests.PapiGeneralFeedback);
        }

        [Theory, AutoMoqData]
        public void RemovePersonalityTest_ShouldRemovePersonalityAssessment(
           Guid candidateId,
           Guid externalId,
           long? candidateOldPlatformId,
           string packageInstanceId,
           string packageTypeId,
           string testUrl,
           string reportInstanceId,
           string url)
        {
            // Arrange
            var candidateTests = new CandidateTests(candidateId, externalId, candidateOldPlatformId);
            candidateTests.CreatePersonalityAssessment(packageInstanceId, packageTypeId, testUrl);
            candidateTests.AddPapiDynamicWheelRaport(reportInstanceId, url);
            candidateTests.AddPapiGeneralFeedbackRaport(reportInstanceId, url);

            // Act
            candidateTests.RemovePersonalityTest();

            // Assert
            Assert.Null(candidateTests.PersonalityAssessment);
            Assert.Null(candidateTests.PapiDynamicWheel);
            Assert.Null(candidateTests.PapiGeneralFeedback);
        }
    }
}
