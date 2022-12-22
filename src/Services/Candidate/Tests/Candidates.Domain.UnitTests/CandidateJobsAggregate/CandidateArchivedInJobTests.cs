using Candidates.Domain.Aggregates.CandidateJobsAggregate.Entities;
using Candidates.Domain.Aggregates.CandidateJobsAggregate.ValueObjects;
using Domain.Seedwork.Enums;
using Domain.Seedwork.Shared.ValueObjects;
using System;
using Tests.Shared;
using Xunit;

namespace Candidates.Domain.UnitTests.CandidateJobsAggregate
{
    public class CandidateArchivedInJobTests
    {
        [Theory, AutoMoqData]
        public void Constructor_ShouldInitialize(
            Guid jobId,
            JobStage jobStage,
            Position position,
            Guid candidateId,
            Company company,
            ArchivedCandidateStage stage,
            WorkType? freelance,
            WorkType? permanent,
            DateTimeOffset? startDate,
            DateTimeOffset? deadlineDate,
            DateTimeOffset? invitedAt,
            bool hasApplied)
        {
            // Arrange

            // Act
            var job = new CandidateArchivedInJob(
                jobId, 
                jobStage,
                position.Id, 
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                candidateId, 
                company.Id,
                company.Name,
                company.LogoUri,
                freelance,
                permanent,
                startDate,
                deadlineDate,
                stage,
                invitedAt,
                hasApplied);

            // Assert
            AssertCandidateArchivedInJob(
                jobId,
                position,
                candidateId,
                company,
                stage,
                freelance,
                permanent,
                startDate,
                deadlineDate,
                job,
                invitedAt,
                hasApplied);
        }

        [Theory, AutoMoqData]
        public void Sync_ShouldSyncArchivedCandidate(
            Guid jobId,
            JobStage jobStage,
            Position position,
            Guid candidateId,
            Company company,
            ArchivedCandidateStage stage,
            ArchivedCandidateStage newStage,
            WorkType? freelance,
            WorkType? permanent,
            DateTimeOffset? startDate,
            DateTimeOffset? deadlineDate,
            DateTimeOffset? invitedAt,
            DateTimeOffset? newInvitedAt,
            bool hasApplied,
            bool newhasApplied)
        {
            // Arrange
            var job = new CandidateArchivedInJob(
                jobId,
                jobStage,
                position.Id,
                position.Code, 
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                candidateId, 
                company.Id, 
                company.Name,
                company.LogoUri,
                freelance,
                permanent,
                startDate,
                deadlineDate,
                stage,
                invitedAt,
                hasApplied);

            // Act
            job.Sync(newStage, newInvitedAt, newhasApplied);

            // Assert
            AssertCandidateArchivedInJob(
                jobId, 
                position,
                candidateId,
                company,
                newStage,
                freelance, 
                permanent, 
                startDate, 
                deadlineDate, 
                job,
                newInvitedAt,
                newhasApplied);
        }

        [Theory, AutoMoqData]
        public void SyncJobPosition_ShouldSync(
            Guid newAliasId,
            string newAliasCode,
            CandidateArchivedInJob candidateArchivedInJob)
        {
            // Arrange 

            // Act
            candidateArchivedInJob.SyncJobPosition(newAliasId, newAliasCode);

            // Assert
            Assert.Equal(newAliasId, candidateArchivedInJob.Position.AliasTo?.Id);
            Assert.Equal(newAliasCode, candidateArchivedInJob.Position.AliasTo?.Code);
        }

        private void AssertCandidateArchivedInJob(
            Guid jobId,
            Position position,
            Guid candidateId,
            Company company,
            ArchivedCandidateStage stage,
            WorkType? freelance,
            WorkType? permanent,
            DateTimeOffset? startDate,
            DateTimeOffset? deadlineDate,
            CandidateArchivedInJob job,
            DateTimeOffset? invitedAt,
            bool hasApplied)
        {
            Assert.NotEqual(Guid.Empty, job.Id);
            Assert.Equal(jobId, job.JobId);
            Assert.Equal(position.Id, job.Position.Id);
            Assert.Equal(position.Code, job.Position.Code);
            Assert.Equal(candidateId, job.CandidateId);
            Assert.Equal(company.Id, job.Company.Id);
            Assert.Equal(company.Name, job.Company.Name);
            Assert.Equal(company.LogoUri, job.Company.LogoUri);
            Assert.Equal(freelance, job.Freelance);
            Assert.Equal(permanent, job.Permanent);
            Assert.Equal(startDate, job.StartDate);
            Assert.Equal(deadlineDate, job.DeadlineDate);
            Assert.Equal(stage, job.Stage);
            Assert.Equal(invitedAt, job.InvitedAt);
            Assert.Equal(hasApplied, job.HasApplied);
        }
    }
}
