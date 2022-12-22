using Candidates.Domain.Aggregates.CandidateTestsAggregate.ValueObjects;
using Domain.Seedwork.Enums;
using System;
using Tests.Shared;
using Xunit;

namespace Candidates.Domain.UnitTests.CandidateTestsAggregate
{
    public class LogicalAssessmentTests
    {
        [Theory, AutoMoqData]
        public void CreateInvited_ShouldInitialize(
            string packageInstanceId,
            string packageTypeId,
            string url)
        {
            // Arrange

            // Act
            var logicalAssessment = LogicalAssessment.CreateInvited(packageInstanceId, packageTypeId, url);

            // Assert
            Assert.Equal(packageInstanceId, logicalAssessment.PackageInstanceId);
            Assert.Equal(packageTypeId, logicalAssessment.PackageTypeId);
            Assert.Equal(AssessmentStatus.Invited, logicalAssessment.Status);
            Assert.Equal(url, logicalAssessment.Url);
            Assert.Null(logicalAssessment.StartedAt);
            Assert.Null(logicalAssessment.CompletedAt);
            Assert.Null(logicalAssessment.Scores);
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
            var logicalAssessment = LogicalAssessment.CreateStarted(
                packageInstanceId, 
                packageTypeId, 
                url, 
                startedAt);

            // Assert
            Assert.Equal(packageInstanceId, logicalAssessment.PackageInstanceId);
            Assert.Equal(packageTypeId, logicalAssessment.PackageTypeId);
            Assert.Equal(AssessmentStatus.Started, logicalAssessment.Status);
            Assert.Equal(url, logicalAssessment.Url);
            Assert.Equal(startedAt, logicalAssessment.StartedAt);
            Assert.Null(logicalAssessment.CompletedAt);
            Assert.Null(logicalAssessment.Scores);
        }

        [Theory, AutoMoqData]
        public void CreateCompleted_ShouldInitialize(
            string packageInstanceId,
            string packageTypeId,
            string url,
            DateTimeOffset startedAt,
            DateTimeOffset completedAt,
            LogicScores scores)
        {
            // Arrange

            // Act
            var logicalAssessment = LogicalAssessment.CreateCompleted(
                packageInstanceId,
                packageTypeId,
                url,
                startedAt,
                completedAt,
                scores);

            // Assert
            Assert.Equal(packageInstanceId, logicalAssessment.PackageInstanceId);
            Assert.Equal(packageTypeId, logicalAssessment.PackageTypeId);
            Assert.Equal(AssessmentStatus.Completed, logicalAssessment.Status);
            Assert.Equal(url, logicalAssessment.Url);
            Assert.Equal(startedAt, logicalAssessment.StartedAt);
            Assert.Equal(completedAt, logicalAssessment.CompletedAt);
            Assert.Equal(scores, logicalAssessment.Scores);
        }

        [Theory, AutoMoqData]
        public void CreateScored_ShouldInitialize(
            string packageInstanceId,
            string packageTypeId,
            string url,
            DateTimeOffset startedAt,
            DateTimeOffset completedAt,
            LogicScores scores)
        {
            // Arrange

            // Act
            var logicalAssessment = LogicalAssessment.CreateScored(
                packageInstanceId,
                packageTypeId,
                url,
                startedAt,
                completedAt,
                scores);

            // Assert
            Assert.Equal(packageInstanceId, logicalAssessment.PackageInstanceId);
            Assert.Equal(packageTypeId, logicalAssessment.PackageTypeId);
            Assert.Equal(AssessmentStatus.Scored, logicalAssessment.Status);
            Assert.Equal(url, logicalAssessment.Url);
            Assert.Equal(startedAt, logicalAssessment.StartedAt);
            Assert.Equal(completedAt, logicalAssessment.CompletedAt);
            Assert.Equal(scores, logicalAssessment.Scores);
        }
    }
}
