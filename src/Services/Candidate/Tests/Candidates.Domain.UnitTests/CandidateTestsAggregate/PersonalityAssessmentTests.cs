using Candidates.Domain.Aggregates.CandidateTestsAggregate.ValueObjects;
using Domain.Seedwork.Enums;
using System;
using Tests.Shared;
using Xunit;

namespace Candidates.Domain.UnitTests.CandidateTestsAggregate
{
    public class PersonalityAssessmentTests
    {
        [Theory, AutoMoqData]
        public void CreateInvited_ShouldInitialize(
            string packageInstanceId,
            string packageTypeId,
            string url)
        {
            // Arrange

            // Act
            var personalityAssessment = PersonalityAssessment.CreateInvited(packageInstanceId, packageTypeId, url);

            // Assert
            Assert.Equal(packageInstanceId, personalityAssessment.PackageInstanceId);
            Assert.Equal(packageTypeId, personalityAssessment.PackageTypeId);
            Assert.Equal(AssessmentStatus.Invited, personalityAssessment.Status);
            Assert.Equal(url, personalityAssessment.Url);
            Assert.Null(personalityAssessment.StartedAt);
            Assert.Null(personalityAssessment.CompletedAt);
            Assert.Null(personalityAssessment.Scores);
        }

        [Theory, AutoMoqData]
        public void CreateStarted_ShouldInitialize(
            string packageInstanceId,
            string packageTypeId,
            string url,
            DateTimeOffset startedAt)
        {
            // Arrange

            // Act
            var personalityAssessment = PersonalityAssessment.CreateStarted(
                packageInstanceId, 
                packageTypeId, 
                url, 
                startedAt);

            // Assert
            Assert.Equal(packageInstanceId, personalityAssessment.PackageInstanceId);
            Assert.Equal(packageTypeId, personalityAssessment.PackageTypeId);
            Assert.Equal(AssessmentStatus.Started, personalityAssessment.Status);
            Assert.Equal(url, personalityAssessment.Url);
            Assert.Equal(startedAt, personalityAssessment.StartedAt);
            Assert.Null(personalityAssessment.CompletedAt);
            Assert.Null(personalityAssessment.Scores);
        }

        [Theory, AutoMoqData]
        public void CreateCompleted_ShouldInitialize(
            string packageInstanceId,
            string packageTypeId,
            string url,
            DateTimeOffset startedAt,
            DateTimeOffset completedAt,
            PersonalityScores scores)
        {
            // Arrange

            // Act
            var personalityAssessment = PersonalityAssessment.CreateCompleted(
                packageInstanceId,
                packageTypeId,
                url,
                startedAt,
                completedAt,
                scores);

            // Assert
            Assert.Equal(packageInstanceId, personalityAssessment.PackageInstanceId);
            Assert.Equal(packageTypeId, personalityAssessment.PackageTypeId);
            Assert.Equal(AssessmentStatus.Completed, personalityAssessment.Status);
            Assert.Equal(url, personalityAssessment.Url);
            Assert.Equal(startedAt, personalityAssessment.StartedAt);
            Assert.Equal(completedAt, personalityAssessment.CompletedAt);
            Assert.Equal(scores, personalityAssessment.Scores);
        }

        [Theory, AutoMoqData]
        public void CreateScored_ShouldInitialize(
            string packageInstanceId,
            string packageTypeId,
            string url,
            DateTimeOffset startedAt,
            DateTimeOffset completedAt,
            PersonalityScores scores)
        {
            // Arrange

            // Act
            var personalityAssessment = PersonalityAssessment.CreateScored(
                packageInstanceId,
                packageTypeId,
                url,
                startedAt,
                completedAt,
                scores);

            // Assert
            Assert.Equal(packageInstanceId, personalityAssessment.PackageInstanceId);
            Assert.Equal(packageTypeId, personalityAssessment.PackageTypeId);
            Assert.Equal(AssessmentStatus.Scored, personalityAssessment.Status);
            Assert.Equal(url, personalityAssessment.Url);
            Assert.Equal(startedAt, personalityAssessment.StartedAt);
            Assert.Equal(completedAt, personalityAssessment.CompletedAt);
            Assert.Equal(scores, personalityAssessment.Scores);
        }
    }
}
