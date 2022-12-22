using Candidates.Domain.Aggregates.CandidateJobsAggregate.Entities;
using Candidates.Domain.Aggregates.CandidateJobsAggregate.ValueObjects;
using Domain.Seedwork.Enums;
using Domain.Seedwork.Shared.ValueObjects;
using System;
using Tests.Shared;
using Xunit;

namespace Candidates.Domain.UnitTests.CandidateJobsAggregate
{
    public class CandidateSelectedInJobTests
    {
        [Theory, AutoMoqData]
        public void Constructor_ShouldInitialize(
            Guid jobId,
            JobStage jobStage,
            Position jobPosition,
            Guid candidateId,
            Company company,
            SelectedCandidateStage stage,
            WorkType? freelance,
            WorkType? permanent,
            DateTimeOffset? startDate,
            DateTimeOffset? deadlineDate,
            DateTimeOffset? invitedAt,
            bool hasApplied)
        {
            // Arrange

            // Act
            var job = new CandidateSelectedInJob(
                jobId, 
                jobStage, 
                jobPosition.Id,
                jobPosition.Code,
                jobPosition.AliasTo?.Id,
                jobPosition.AliasTo?.Code,
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
            Assert.NotEqual(Guid.Empty, job.Id);
            Assert.Equal(jobId, job.JobId);
            Assert.Equal(jobPosition.Id, job.Position.Id);
            Assert.Equal(jobPosition.Code, job.Position.Code);
            Assert.Equal(jobPosition.AliasTo?.Id, job.Position.AliasTo?.Id);
            Assert.Equal(jobPosition.AliasTo?.Code, job.Position.AliasTo?.Code);
            Assert.Equal(candidateId, job.CandidateId);
            Assert.Equal(company.Id, job.Company.Id);
            Assert.Equal(company.Name, job.Company.Name);
            Assert.Equal(company.LogoUri, job.Company.LogoUri);
            Assert.Equal(freelance, job.Freelance);
            Assert.Equal(permanent, job.Permanent);
            Assert.Equal(startDate, job.StartDate);
            Assert.Equal(deadlineDate, job.DeadlineDate);
            Assert.Equal(stage, job.Stage);
        }

        [Theory, AutoMoqData]
        public void Sync_ShouldUpdateSelectedCandidate(
            Guid jobId,
            JobStage jobStage,
            Position position,
            Guid candidateId,
            Company company,
            SelectedCandidateStage stage,
            SelectedCandidateStage newStage,
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
            var job = new CandidateSelectedInJob(
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
            Assert.NotEqual(Guid.Empty, job.Id);
            Assert.Equal(jobId, job.JobId);
            Assert.Equal(position.Id, job.Position.Id);
            Assert.Equal(position.Code, job.Position.Code);
            Assert.Equal(position.AliasTo?.Id, job.Position.AliasTo?.Id);
            Assert.Equal(position.AliasTo?.Code, job.Position.AliasTo?.Code);
            Assert.Equal(candidateId, job.CandidateId);
            Assert.Equal(company.Id, job.Company.Id);
            Assert.Equal(company.Name, job.Company.Name);
            Assert.Equal(company.LogoUri, job.Company.LogoUri);
            Assert.Equal(freelance, job.Freelance);
            Assert.Equal(permanent, job.Permanent);
            Assert.Equal(startDate, job.StartDate);
            Assert.Equal(deadlineDate, job.DeadlineDate);
            Assert.Equal(newStage, job.Stage);
            Assert.Equal(newhasApplied, job.HasApplied);
        }

        [Theory, AutoMoqData]
        public void Update_ShouldUpdateCandidateJobInfo(
            string motivationVideoUri,
            string motivationVideoFileName,
            bool isMotivationVideoChanged,
            string? coverLetter,
            CandidateSelectedInJob candidateSelectedInJob)
        {
            // Arrange 

            // Act
            candidateSelectedInJob.Update(
                motivationVideoUri, 
                motivationVideoFileName, 
                isMotivationVideoChanged, 
                coverLetter);

            // Assert
            Assert.Equal(coverLetter, candidateSelectedInJob.CoverLetter);
            if (isMotivationVideoChanged)
            {
                Assert.Equal(motivationVideoUri, candidateSelectedInJob?.MotivationVideo?.Uri);
                Assert.Equal(motivationVideoFileName, candidateSelectedInJob?.MotivationVideo?.FileName);
            }
        }

        [Theory, AutoMoqData]
        public void RemoveMotivationVideo_ShouldSetMotivationVideoToNull(
            string motivationVideoUri,
            string motivationVideoFileName,
            string? coverLetter,
            CandidateSelectedInJob candidateSelectedInJob)
        {
            // Arrange 
            candidateSelectedInJob.Update(
                motivationVideoUri,
                motivationVideoFileName,
                true,
                coverLetter);
            // Act
            candidateSelectedInJob.RemoveMotivationVideo();

            // Assert
            Assert.Null(candidateSelectedInJob.MotivationVideo);
        }

        [Theory, AutoMoqData]
        public void SyncJobPosition_ShouldSync(
            Guid newAliasId,
            string newAliasCode,
            CandidateSelectedInJob candidateSelectedInJob)
        {
            // Arrange 
            
            // Act
            candidateSelectedInJob.SyncJobPosition(newAliasId, newAliasCode);

            // Assert
            Assert.Equal(newAliasId, candidateSelectedInJob.Position.AliasTo?.Id);
            Assert.Equal(newAliasCode, candidateSelectedInJob.Position.AliasTo?.Code);
        }
    }
}
