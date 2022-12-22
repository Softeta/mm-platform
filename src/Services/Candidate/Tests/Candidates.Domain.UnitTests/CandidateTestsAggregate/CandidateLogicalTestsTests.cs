using Candidates.Domain.Aggregates.CandidateTestsAggregate;
using Candidates.Domain.Aggregates.CandidateTestsAggregate.ValueObjects;
using Domain.Seedwork.Enums;
using Domain.Seedwork.Exceptions;
using System;
using Tests.Shared;
using Xunit;

namespace Candidates.Domain.UnitTests.CandidateTestsAggregate
{
    public class CandidateLogicalTestsTests
    {
        [Theory, AutoMoqData]
        public void Constructor_ShouldInitialize(Guid candidateId, Guid externalId, long? candidateOldPlatformId)
        {
            // Arrange

            // Act
            var candidateTests = new CandidateTests(candidateId, externalId, candidateOldPlatformId);

            // Assert
            Assert.Equal(candidateId, candidateTests.Id);
            Assert.Equal(externalId, candidateTests.ExternalId);
            Assert.Equal(candidateOldPlatformId, candidateTests.CandidateOldPlatformId);
        }

        [Theory, AutoMoqData]
        public void CreateLogicalAssessment_ShouldInitializeLogicalAssessment(
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
            candidateTests.CreateLogicalAssessment(packageInstanceId, packageTypeId, testUrl);

            // Assert
            Assert.NotNull(candidateTests.LogicalAssessment);
            Assert.Equal(expectedStatus, candidateTests.LogicalAssessment!.Status);
        }

        [Theory, AutoMoqData]
        public void CreateLogicalAssessment_ShouldThrowConflictException(
            Guid candidateId,
            Guid externalId,
            long? candidateOldPlatformId,
            string packageInstanceId,
            string packageTypeId,
            string testUrl)
        {
            // Arrange
            var candidateTests = new CandidateTests(candidateId, externalId, candidateOldPlatformId);
            candidateTests.CreateLogicalAssessment(packageInstanceId, packageTypeId, testUrl);

            // Act
            Action action = () => candidateTests.CreateLogicalAssessment(packageInstanceId, packageTypeId, testUrl);

            // Assert
            Assert.Throws<ConflictException>(action);
        }

        [Theory, AutoMoqData]
        public void StartLogicalAssessment_ShouldInitializeLogicalAssessment(
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
            candidateTests.CreateLogicalAssessment(packageInstanceId, packageTypeId, testUrl);
            var expectedStatus = AssessmentStatus.Started;

            // Act
            candidateTests.StartLogicalAssessment(packageInstanceId, packageTypeId, startedAt);

            // Assert
            Assert.NotNull(candidateTests.LogicalAssessment);
            Assert.Equal(expectedStatus, candidateTests.LogicalAssessment!.Status);
        }

        [Theory, AutoMoqData]
        public void StartLogicalAssessment_ShouldNotChangeIfWasStarted(
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
            candidateTests.CreateLogicalAssessment(packageInstanceId, packageTypeId, testUrl);
            candidateTests.StartLogicalAssessment(packageInstanceId, packageTypeId, startedAt);
            var expectedStatus = AssessmentStatus.Started;

            // Act
            candidateTests.StartLogicalAssessment(packageInstanceId, packageTypeId, secondStartedAt);

            // Assert
            Assert.NotNull(candidateTests.LogicalAssessment);
            Assert.Equal(expectedStatus, candidateTests.LogicalAssessment!.Status);
            Assert.Equal(startedAt, candidateTests.LogicalAssessment!.StartedAt);
        }

        [Theory, AutoMoqData]
        public void CompleteLogicalAssessment_ShouldInitializeLogicalAssessment(
           Guid candidateId,
           Guid externalId,
           long? candidateOldPlatformId,
           string packageInstanceId,
           string packageTypeId,
           string testUrl,
           DateTimeOffset startedAt,
           DateTimeOffset completedAt, 
           LogicScores scores)
        {
            // Arrange
            var candidateTests = new CandidateTests(candidateId, externalId, candidateOldPlatformId);
            candidateTests.CreateLogicalAssessment(packageInstanceId, packageTypeId, testUrl);
            var expectedStatus = AssessmentStatus.Completed;

            // Act
            candidateTests.CompleteLogicalAssessment(
                packageInstanceId, 
                packageTypeId, 
                startedAt, 
                completedAt, 
                scores);

            // Assert
            Assert.NotNull(candidateTests.LogicalAssessment);
            Assert.Equal(expectedStatus, candidateTests.LogicalAssessment!.Status);
        }

        [Theory, AutoMoqData]
        public void CompleteLogicalAssessment_ShouldNotChangeIfWasCompleted(
           Guid candidateId,
           Guid externalId,
           long? candidateOldPlatformId,
           string packageInstanceId,
           string packageTypeId,
           string testUrl,
           DateTimeOffset startedAt,
           DateTimeOffset completedAt,
           LogicScores scores,
           DateTimeOffset secondCompletedAt)
        {
            // Arrange
            var candidateTests = new CandidateTests(candidateId, externalId, candidateOldPlatformId);

            candidateTests.CreateLogicalAssessment(packageInstanceId, packageTypeId, testUrl);
            candidateTests.CompleteLogicalAssessment(
                packageInstanceId,
                packageTypeId,
                startedAt,
                completedAt,
                scores);

            var expectedStatus = AssessmentStatus.Completed;

            // Act
            candidateTests.CompleteLogicalAssessment(
                packageInstanceId,
                packageTypeId,
                startedAt,
                secondCompletedAt,
                scores);

            // Assert
            Assert.NotNull(candidateTests.LogicalAssessment);
            Assert.Equal(completedAt, candidateTests.LogicalAssessment!.CompletedAt);
            Assert.Equal(expectedStatus, candidateTests.LogicalAssessment!.Status);
        }

        [Theory, AutoMoqData]
        public void ScoreLogicalAssessment_ShouldInitializeLogicalAssessment(
           Guid candidateId,
           Guid externalId,
           long? candidateOldPlatformId,
           string packageInstanceId,
           string packageTypeId,
           string testUrl,
           DateTimeOffset startedAt,
           DateTimeOffset completedAt,
           LogicScores scores)
        {
            // Arrange
            var candidateTests = new CandidateTests(candidateId, externalId, candidateOldPlatformId);
            candidateTests.CreateLogicalAssessment(packageInstanceId, packageTypeId, testUrl);
            var expectedStatus = AssessmentStatus.Scored;

            // Act
            candidateTests.ScoreLogicalAssessment(
                packageInstanceId,
                packageTypeId,
                startedAt,
                completedAt,
                scores);

            // Assert
            Assert.NotNull(candidateTests.LogicalAssessment);
            Assert.Equal(expectedStatus, candidateTests.LogicalAssessment!.Status);
        }

        [Theory, AutoMoqData]
        public void ScoreLogicalAssessment_ShouldNotChangeIfWasScored(
          Guid candidateId,
          Guid externalId,
          long? candidateOldPlatformId,
          string packageInstanceId,
          string packageTypeId,
          string testUrl,
          DateTimeOffset startedAt,
          DateTimeOffset completedAt,
          LogicScores scores,
          DateTimeOffset secondCompletedAt)
        {
            // Arrange
            var candidateTests = new CandidateTests(candidateId, externalId, candidateOldPlatformId);

            candidateTests.CreateLogicalAssessment(packageInstanceId, packageTypeId, testUrl);
            candidateTests.ScoreLogicalAssessment(
                packageInstanceId,
                packageTypeId,
                startedAt,
                completedAt,
                scores);

            var expectedStatus = AssessmentStatus.Scored;

            // Act
            candidateTests.ScoreLogicalAssessment(
                packageInstanceId,
                packageTypeId,
                startedAt,
                secondCompletedAt,
                scores);

            // Assert
            Assert.NotNull(candidateTests.LogicalAssessment);
            Assert.Equal(completedAt, candidateTests.LogicalAssessment!.CompletedAt);
            Assert.Equal(expectedStatus, candidateTests.LogicalAssessment!.Status);
        }

        [Theory, AutoMoqData]
        public void AddLgiGeneralFeedbackRaport_ShouldInitializeLgiGeneralFeedbackRaport(
           Guid candidateId,
           Guid externalId,
           long? candidateOldPlatformId,
           string reportInstanceId, 
           string url)
        {
            // Arrange
            var candidateTests = new CandidateTests(candidateId, externalId, candidateOldPlatformId);

            // Act
            candidateTests.AddLgiGeneralFeedbackRaport(reportInstanceId, url);

            // Assert
            Assert.NotNull(candidateTests.LgiGeneralFeedback);
        }

        [Theory, AutoMoqData]
        public void RemoveLogicalTest_ShouldRemoveLogicalAssessment(
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
            candidateTests.CreateLogicalAssessment(packageInstanceId, packageTypeId, testUrl);
            candidateTests.AddLgiGeneralFeedbackRaport(reportInstanceId, url);

            // Act
            candidateTests.RemoveLogicalTest();

            // Assert
            Assert.Null(candidateTests.LogicalAssessment);
            Assert.Null(candidateTests.LgiGeneralFeedback);
        } 
    }
}
