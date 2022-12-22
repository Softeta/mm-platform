using Candidates.Domain.Aggregates.CandidateJobsAggregate;
using Candidates.Domain.Aggregates.CandidateJobsAggregate.Entities;
using Domain.Seedwork.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Seedwork.Exceptions;
using Tests.Shared;
using Xunit;
using Candidates.Domain.Events.CandidateJobsAggregate;
using Candidates.Domain.Aggregates.CandidateAggregate;

namespace Candidates.Domain.UnitTests.CandidateJobsAggregate
{
    public class CandidateJobsTests
    {
        [Theory, AutoMoqData]
        public void Constructor_ShouldInitialize(Guid candidateId)
        {
            // Arrange

            // Act
            var candidateJobs = new CandidateJobs(candidateId);

            // Assert
            Assert.Equal(candidateId, candidateJobs.Id);
        }

        [Theory, AutoMoqData]
        public void SyncCandidateSelectedJob_ShouldAddSelectedJob(Guid candidateId, ICollection<CandidateSelectedInJob> jobs)
        {
            // Arrange
            var candidateJobs = new CandidateJobs(candidateId);

            // Act
            foreach (var job in jobs)
            {
                candidateJobs.SyncCandidateSelectedJob(job);
            }

            // Assert
            Assert.Equal(jobs.Count, candidateJobs.SelectedInJobs.Count);
        }

        [Theory, AutoMoqData]
        public void SyncNewlyAddedSelectedJob_ShouldAddSelectedJob(Guid candidateId, List<CandidateSelectedInJob> jobs)
        {
            // Arrange
            var candidateJobs = new CandidateJobs(candidateId);
            jobs.ForEach(x => x.Sync(SelectedCandidateStage.New, DateTimeOffset.UtcNow, false));

            // Act
            foreach (var job in jobs)
            {
                candidateJobs.SyncNewlyAddedSelectedJob(job);
            }

            // Assert
            Assert.Equal(jobs.Count, candidateJobs.SelectedInJobs.Count);
        }


        [Theory, AutoMoqData]
        public void SyncNewlyAddedSelectedJob_ShouldThrowDomainException_WhenSelectedJobStageIsNotNew(Guid candidateId, CandidateSelectedInJob selectedJob)
        {
            // Arrange
            var candidateJobs = new CandidateJobs(candidateId);
            selectedJob.Sync(SelectedCandidateStage.Interested, DateTimeOffset.UtcNow, false);

            // Act
            Action action = () => candidateJobs.SyncNewlyAddedSelectedJob(selectedJob);

            // Assert
            Assert.Throws<DomainException>(action);
        }

        [Theory, AutoMoqData]
        public void SyncNewlyAddedSelectedJob_ShouldThrowDomainException_WhenJobAlreadyExistsInSelectedInJobs(Guid candidateId, List<CandidateSelectedInJob> jobs, CandidateSelectedInJob selectedJob)
        {
            // Arrange
            var candidateJobs = new CandidateJobs(candidateId);
            selectedJob.Sync(SelectedCandidateStage.New, DateTimeOffset.UtcNow, false);
            jobs.Add(selectedJob);

            foreach (var job in jobs)
            {
                candidateJobs.SyncCandidateSelectedJob(job);
            }

            // Act
            Action action = () => candidateJobs.SyncNewlyAddedSelectedJob(selectedJob);

            // Assert
            Assert.Throws<DomainException>(action);
        }

        [Theory, AutoMoqData]
        public void SyncNewlyAddedSelectedJob_ShouldThrowDomainException_WhenJobAlreadyExistsArchivedInJobs(
            Guid candidateId,
            List<CandidateArchivedInJob> archivedJobs,
            CandidateSelectedInJob selectedJob)
        {
            // Arrange
            var candidateJobs = new CandidateJobs(candidateId);
      
            var archivedJob = new CandidateArchivedInJob(
                selectedJob.JobId,
                selectedJob.JobStage,
                selectedJob.Position.Id,
                selectedJob.Position.Code,
                selectedJob.Position.AliasTo?.Id,
                selectedJob.Position.AliasTo?.Code,
                selectedJob.CandidateId, 
                selectedJob.Company.Id,
                selectedJob.Company.Name,
                selectedJob.Company.LogoUri,
                selectedJob.Freelance,
                selectedJob.Permanent,
                selectedJob.StartDate,
                selectedJob.DeadlineDate,
                ArchivedCandidateStage.NotRelevant,
                selectedJob.InvitedAt,
                selectedJob.HasApplied);

            archivedJobs.Add(archivedJob);

            selectedJob.Sync(SelectedCandidateStage.New, DateTimeOffset.UtcNow, false);

            foreach (var job in archivedJobs)
            {
                candidateJobs.SyncCandidateArchivedJob(job);
            }

            // Act
            Action action = () => candidateJobs.SyncNewlyAddedSelectedJob(selectedJob);

            // Assert
            Assert.Throws<DomainException>(action);
        }

        [Theory, AutoMoqData]
        public void SyncCandidateArchivedJob_ShouldAddArchivedJob(Guid candidateId, ICollection<CandidateArchivedInJob> jobs)
        {
            // Arrange
            var candidateJobs = new CandidateJobs(candidateId);

            // Act
            foreach (var job in jobs)
            {
                candidateJobs.SyncCandidateArchivedJob(job);
            }

            // Assert
            Assert.Equal(jobs.Count, candidateJobs.ArchivedInJobs.Count);
        }

        [Theory, AutoMoqData]
        public void SyncCandidateArchivedJob_ShouldUpdateJob(
            Guid candidateId, 
            List<CandidateSelectedInJob> selectedJobs,
            List<CandidateArchivedInJob> archivedJobs)
        {
            // Arrange
            var candidateJobs = new CandidateJobs(candidateId);
            selectedJobs.ForEach(x => candidateJobs.SyncCandidateSelectedJob(x));

            var selectedJob = selectedJobs.FirstOrDefault()!;

            var archivedJob = new CandidateArchivedInJob(
                selectedJob.JobId,
                selectedJob.JobStage,
                selectedJob.Position.Id,
                selectedJob.Position.Code,
                selectedJob.Position.AliasTo?.Id,
                selectedJob.Position.AliasTo?.Code,
                selectedJob.CandidateId, 
                selectedJob.Company.Id,
                selectedJob.Company.Name,
                selectedJob.Company.LogoUri,
                selectedJob.Freelance,
                selectedJob.Permanent,
                selectedJob.StartDate,
                selectedJob.DeadlineDate,
                ArchivedCandidateStage.NotRelevant,
                selectedJob.InvitedAt,
                selectedJob.HasApplied);

            archivedJobs.Add(archivedJob);

            var expectedSelectedJobsCount = selectedJobs.Count - 1;
            var expectedArchivedJobsCount = archivedJobs.Count;

            // Act
            archivedJobs.ForEach(x => candidateJobs.SyncCandidateArchivedJob(x));

            // Assert
            Assert.Equal(expectedSelectedJobsCount, candidateJobs.SelectedInJobs.Count);
            Assert.Equal(expectedArchivedJobsCount, candidateJobs.ArchivedInJobs.Count);
        }

        [Theory, AutoMoqData]
        public void SyncCandidateSelectedJob_ShouldUpdateJob(
            Guid candidateId,
            List<CandidateSelectedInJob> selectedJobs,
            List<CandidateArchivedInJob> archivedJobs)
        {
            // Arrange
            var candidateJobs = new CandidateJobs(candidateId);
            archivedJobs.ForEach(x => candidateJobs.SyncCandidateArchivedJob(x));

            var archivedJob = archivedJobs.FirstOrDefault()!;

            var selectedJob = new CandidateSelectedInJob(
                archivedJob.JobId,
                archivedJob.JobStage,
                archivedJob.Position.Id,
                archivedJob.Position.Code,
                archivedJob.Position.AliasTo?.Id,
                archivedJob.Position.AliasTo?.Code,
                archivedJob.CandidateId,
                archivedJob.Company.Id,
                archivedJob.Company.Name,
                archivedJob.Company.LogoUri,
                archivedJob.Freelance,
                archivedJob.Permanent,
                archivedJob.StartDate,
                archivedJob.DeadlineDate,
                SelectedCandidateStage.NoInterview,
                archivedJob.InvitedAt,
                archivedJob.HasApplied);

            selectedJobs.Add(selectedJob);

            var expectedSelectedJobsCount = selectedJobs.Count;
            var expectedArchivedJobsCount = archivedJobs.Count - 1;

            // Act
            selectedJobs.ForEach(x => candidateJobs.SyncCandidateSelectedJob(x));

            // Assert
            Assert.Equal(expectedSelectedJobsCount, candidateJobs.SelectedInJobs.Count);
            Assert.Equal(expectedArchivedJobsCount, candidateJobs.ArchivedInJobs.Count);
            Assert.True(candidateJobs.IsShortlisted);
        }

        [Theory]
        [InlineAutoMoqData(JobStage.OnHold)]
        [InlineAutoMoqData(JobStage.Lost)]
        [InlineAutoMoqData(JobStage.Successful)]
        public void SyncJobStage_ShouldAddDomainEvent_WhenJobStageArchived(
            JobStage jobStage,
            CandidateSelectedInJob selectedJob,
            Guid candidateId)
        {
            // Arrange
            var candidateJobs = new CandidateJobs(candidateId);
            var candidateSelectedInJob = new CandidateSelectedInJob(
                selectedJob.JobId,
                selectedJob.JobStage,
                selectedJob.Position.Id,
                selectedJob.Position.Code,
                selectedJob.Position.AliasTo?.Id,
                selectedJob.Position.AliasTo?.Code,
                selectedJob.CandidateId,
                selectedJob.Company.Id,
                selectedJob.Company.Name,
                selectedJob.Company.LogoUri,
                selectedJob.Freelance,
                selectedJob.Permanent,
                selectedJob.StartDate,
                selectedJob.DeadlineDate,
                SelectedCandidateStage.NoInterview,
                selectedJob.InvitedAt,
                selectedJob.HasApplied);

            candidateJobs.SyncCandidateSelectedJob(candidateSelectedInJob);

            // Act
            candidateJobs.SyncJobStage(candidateSelectedInJob.JobId, jobStage);

            // Assert
            var @event = Assert.Single(candidateJobs.DomainEvents.Where(x => 
                !x.Published && 
                x as CandidateInJobJobArchivedDomainEvent != null));
        }

        [Theory, AutoMoqData]
        public void RejectJob_ShouldRemoveFromSelectedJobAndAddToArchived(
            Guid candidateId,
            CandidateSelectedInJob selectedJobRequest)
        {
            // Arrange
            var expectedSelectedJobCount = 0;
            var expectedArchivedJobCount = 1;
            var expectedArchivedJobStage = ArchivedCandidateStage.NotInterested;

            var candidateJobs = new CandidateJobs(candidateId);
            
            var selectedJob = new CandidateSelectedInJob(
                selectedJobRequest.JobId,
                selectedJobRequest.JobStage,
                selectedJobRequest.Position.Id,
                selectedJobRequest.Position.Code,
                selectedJobRequest.Position.AliasTo?.Id,
                selectedJobRequest.Position.AliasTo?.Code,
                selectedJobRequest.CandidateId,
                selectedJobRequest.Company.Id,
                selectedJobRequest.Company.Name,
                selectedJobRequest.Company.LogoUri,
                selectedJobRequest.Freelance,
                selectedJobRequest.Permanent,
                selectedJobRequest.StartDate,
                selectedJobRequest.DeadlineDate,
                SelectedCandidateStage.NoInterview,
                selectedJobRequest.InvitedAt,
                selectedJobRequest.HasApplied);

            var selectedJobs = new List<CandidateSelectedInJob> { selectedJob };
            selectedJobs.ForEach(x => candidateJobs.SyncCandidateSelectedJob(x));

            // Act
            candidateJobs.RejectJob(selectedJobRequest.JobId);

            // Assert
            Assert.Equal(expectedSelectedJobCount, candidateJobs.SelectedInJobs.Count);
            Assert.Equal(expectedArchivedJobCount, candidateJobs.ArchivedInJobs.Count);
            Assert.Equal(expectedArchivedJobStage, candidateJobs.ArchivedInJobs.First().Stage);
        }

        [Theory, AutoMoqData]
        public void ApplyToJob_ShouldThrowException_WhenCandidateIsArchived(
            Guid candidateId,
            CandidateArchivedInJob archivedInJob)
        {
            // Arrange
            var candidateJobs = new CandidateJobs(candidateId);

            var archivedJobs = new List<CandidateArchivedInJob> { archivedInJob };
            archivedJobs.ForEach(x => candidateJobs.SyncCandidateArchivedJob(x));

            // Act
            var action = () => candidateJobs.ApplyToJob(
                archivedInJob.JobId,
                archivedInJob.JobStage,
                archivedInJob.Position.Id,
                archivedInJob.Position.Code,
                archivedInJob.Position.AliasTo?.Id,
                archivedInJob.Position.AliasTo?.Code,
                archivedInJob.Company.Id,
                archivedInJob.Company.Name,
                archivedInJob.Company.LogoUri,
                archivedInJob.Freelance,
                archivedInJob.Permanent,
                archivedInJob.StartDate,
                archivedInJob.DeadlineDate);

            // Assert
            Assert.Throws<DomainException>(action);
        }

        [Theory, AutoMoqData]
        public void ApplyToJob_ShouldThrowException_WhenCandidateAlreadyAppliedToJob(
            Guid candidateId,
            CandidateSelectedInJob selectedInJob)
        {
            // Arrange
            var candidateJobs = new CandidateJobs(candidateId);

            candidateJobs.ApplyToJob(
                selectedInJob.JobId,
                selectedInJob.JobStage,
                selectedInJob.Position.Id,
                selectedInJob.Position.Code,
                selectedInJob.Position.AliasTo?.Id,
                selectedInJob.Position.AliasTo?.Code,
                selectedInJob.Company.Id,
                selectedInJob.Company.Name,
                selectedInJob.Company.LogoUri,
                selectedInJob.Freelance,
                selectedInJob.Permanent,
                selectedInJob.StartDate,
                selectedInJob.DeadlineDate);

            // Act
            var action = () => candidateJobs.ApplyToJob(
                selectedInJob.JobId,
                selectedInJob.JobStage,
                selectedInJob.Position.Id,
                selectedInJob.Position.Code,
                selectedInJob.Position.AliasTo?.Id,
                selectedInJob.Position.AliasTo?.Code,
                selectedInJob.Company.Id,
                selectedInJob.Company.Name,
                selectedInJob.Company.LogoUri,
                selectedInJob.Freelance,
                selectedInJob.Permanent,
                selectedInJob.StartDate,
                selectedInJob.DeadlineDate);

            // Assert
            Assert.Throws<DomainException>(action);
        }

        [Theory, AutoMoqData]
        public void ApplyToJob_ShouldAddApplicationToList(
            Guid candidateId,
            CandidateSelectedInJob selectedInJob)
        {
            // Arrange
            var candidateJobs = new CandidateJobs(candidateId);
            var expectedAppliedToJobsCount = 1;

            // Act
            candidateJobs.ApplyToJob(
                selectedInJob.JobId,
                selectedInJob.JobStage,
                selectedInJob.Position.Id,
                selectedInJob.Position.Code,
                selectedInJob.Position.AliasTo?.Id,
                selectedInJob.Position.AliasTo?.Code,
                selectedInJob.Company.Id,
                selectedInJob.Company.Name,
                selectedInJob.Company.LogoUri,
                selectedInJob.Freelance,
                selectedInJob.Permanent,
                selectedInJob.StartDate,
                selectedInJob.DeadlineDate);

            // Assert
            var domainEvent = Assert.Single(candidateJobs.DomainEvents);
            var candidateAppliedToJobEvent = domainEvent as CandidateAppliedToJobDomainEvent;

            Assert.NotNull(candidateAppliedToJobEvent);
            Assert.Equal(expectedAppliedToJobsCount, candidateJobs.AppliedInJobs.Count);
        }
    }
}
