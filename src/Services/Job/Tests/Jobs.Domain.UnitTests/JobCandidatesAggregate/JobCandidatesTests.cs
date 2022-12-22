using Domain.Seedwork.Enums;
using Domain.Seedwork.Exceptions;
using Domain.Seedwork.Shared.ValueObjects;
using Jobs.Domain.Aggregates.JobCandidatesAggregate;
using Jobs.Domain.Aggregates.JobCandidatesAggregate.Entities;
using Jobs.Domain.Events.JobCandidatesAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using Tests.Shared;
using Xunit;

namespace Jobs.Domain.UnitTests.JobCandidatesAggregate
{
    public class JobCandidatesTests
    {     
        [Theory, AutoMoqData]
        public void Constructor_ShouldInitializeNewJobCandidate(
            Guid jobId,
            JobStage jobStage,
            Position position,
            Guid companyId,
            string companyName,
            string companyLogoUri,
            WorkType? freelance,
            WorkType? permanent,
            DateTimeOffset? startDate,
            DateTimeOffset? deadlineDate)
        {
            // Act
            var jobCandidates = new JobCandidates(
                jobId,
                jobStage,
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                companyId,
                companyName,
                companyLogoUri,
                freelance,
                permanent,
                deadlineDate,
                startDate);

            Assert.Equal(jobId, jobCandidates.Id);
            Assert.Equal(jobStage, jobCandidates.Stage);
            Assert.Equal(position.Id, jobCandidates.Position.Id);
            Assert.Equal(position.Code, jobCandidates.Position.Code);
            Assert.Equal(position.AliasTo?.Id, jobCandidates.Position.AliasTo?.Id);
            Assert.Equal(position.AliasTo?.Code, jobCandidates.Position.AliasTo?.Code);
            Assert.Equal(companyId, jobCandidates.Company.Id);
            Assert.Equal(companyName, jobCandidates.Company.Name);
            Assert.Equal(companyLogoUri, jobCandidates.Company.LogoUri);
            Assert.Equal(freelance, jobCandidates.Freelance);
            Assert.Equal(permanent, jobCandidates.Permanent);
            Assert.Equal(startDate, jobCandidates.StartDate);
            Assert.Equal(deadlineDate, jobCandidates.DeadlineDate);
        }
        [Theory, AutoMoqData]
        public void AddSelectedCandidate_ShouldAddOnlyNewCandidatesToTheJob(
            Guid jobId,
            JobStage jobStage,
            Position position,
            Guid companyId,
            string companyName,
            string companyLogoUri,
            Guid candidateId,
            string firstName,
            string lastName,
            string email,
            string? phoneNumber,
            string? pictureUri,
            bool isShortListedInOtherJob,
            bool isHiredInOtherJob,
            WorkType? freelance,
            WorkType? permanent,
            DateTimeOffset? startDate,
            DateTimeOffset? deadlineDate,
            string? brief,
            DateTimeOffset? invitedAt,
            SystemLanguage? systemLanguage,
            bool hasApplied)
        {
            // Arrange
            var jobCandidates = new JobCandidates(
                jobId,
                jobStage,
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                companyId,
                companyName,
                companyLogoUri,
                freelance,
                permanent,
                deadlineDate,
                startDate);

            var selectedCandidate = new JobSelectedCandidate(
                jobId,
                candidateId,
                firstName,
                lastName,
                email,
                phoneNumber,
                pictureUri,
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                systemLanguage,
                isShortListedInOtherJob,
                isHiredInOtherJob,
                brief,
                invitedAt,
                hasApplied);

            var candidates = new List<JobSelectedCandidate>
            {
                selectedCandidate,
                selectedCandidate
            };

            //Act
            jobCandidates.AddSelectedCandidates(candidates);

            //Assert
            Assert.Equal(1, jobCandidates.SelectedCandidates.Count);
        }

        [Theory, AutoMoqData]
        public void UpdateSelectedCandidate_ShouldAddEvent_WhenCandidateExist(
            Guid jobId,
            JobStage jobStage,
            Position position,
            Guid companyId,
            string companyName,
            string companyLogoUri,
            Guid candidateId,
            string firstName,
            string lastName,
            string email,
            string? phoneNumber,
            string? pictureUri,
            bool isShortListedInOtherJob,
            bool isHiredInOtherJob,
            WorkType? freelance,
            WorkType? permanent,
            DateTimeOffset? startDate,
            DateTimeOffset? deadlineDate,
            string? brief,
            DateTimeOffset? invitedAt,
            SystemLanguage? systemLanguage,
            bool hasApplied)
        {
            // Arrange
            var jobCandidates = new JobCandidates(
                jobId,
                jobStage,
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                companyId,
                companyName,
                companyLogoUri,
                freelance,
                permanent,
                deadlineDate,
                startDate);

            var selectedCandidate = new JobSelectedCandidate(
                jobId,
                candidateId,
                firstName,
                lastName,
                email,
                phoneNumber,
                pictureUri,
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                systemLanguage,
                isShortListedInOtherJob, 
                isHiredInOtherJob,
                brief,
                invitedAt,
                hasApplied);

            var candidates = new List<JobSelectedCandidate>
            {
                selectedCandidate
            };

            jobCandidates.AddSelectedCandidates(candidates);

            var expectedStage = SelectedCandidateStage.Interested;

            //Act
            jobCandidates.UpdateSelectedCandidates (candidates.Select(x => x.CandidateId), expectedStage);

            //Assert
            var domainEvent = jobCandidates.DomainEvents.Where(x => x.GetType() == typeof(SelectedCandidatesUpdatedDomainEvent));
            Assert.Single(domainEvent);
        }

        [Theory, AutoMoqData]
        public void AddSelectedCandidates_ShouldAddEvent(
            Guid jobId,
            JobStage jobStage,
            Position position,
            Guid companyId,
            string companyName,
            string companyLogoUri,
            Guid candidateId,
            string firstName,
            string lastName,
            string email,
            string? phoneNumber,
            string? pictureUri,
            bool isShortListedInOtherJob,
            bool isHiredInOtherJob,
            WorkType? freelance,
            WorkType? permanent,
            DateTimeOffset? startDate,
            DateTimeOffset? deadlineDate,
            string? brief,
            DateTimeOffset? invitedAt,
            SystemLanguage? systemLanguage,
            bool hasApplied)
        {
            // Arrange
            var jobCandidates = new JobCandidates(
                jobId,
                jobStage,
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                companyId,
                companyName,
                companyLogoUri,
                freelance,
                permanent,
                deadlineDate,
                startDate);

            var selectedCandidate = new JobSelectedCandidate(
                jobId,
                candidateId,
                firstName,
                lastName,
                email,
                phoneNumber,
                pictureUri,
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                systemLanguage,
                isShortListedInOtherJob, 
                isHiredInOtherJob,
                brief,
                invitedAt,
                hasApplied);

            var candidates = new List<JobSelectedCandidate>
            {
                selectedCandidate
            };

            //Act
            jobCandidates.AddSelectedCandidates(candidates);

            //Assert
            var domainEvent = Assert.Single(jobCandidates.DomainEvents);
            Assert.IsType<SelectedCandidatesAddedDomainEvent>(domainEvent);
        }

        [Theory, AutoMoqData]
        public void ActivateShortlist_ShouldThrowDomainException_WhenThereIsNoCandidateWithShortListedStage(
            Guid jobId,
            JobStage jobStage,
            Position position,
            Guid companyId,
            string companyName,
            string companyLogoUri,
            Guid candidateId,
            string firstName,
            string lastName,
            string email,
            string? phoneNumber,
            string? pictureUri,
            string clientEmail,
            string clientFirstName,
            bool isShortListedInOtherJob,
            bool isHiredInOtherJob,
            WorkType? freelance,
            WorkType? permanent,
            DateTimeOffset? startDate,
            DateTimeOffset? deadlineDate,
            string? brief,
            DateTimeOffset? invitedAt,
            SystemLanguage? systemLanguage,
            bool hasApplied,
            Guid? contactExternalId)
        {
            // Arrange
            var jobCandidates = new JobCandidates(
                jobId,
                jobStage,
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                companyId,
                companyName,
                companyLogoUri,
                freelance,
                permanent,
                deadlineDate,
                startDate);

            var selectedCandidate = new JobSelectedCandidate(
                jobId,
                candidateId,
                firstName,
                lastName,
                email,
                phoneNumber,
                pictureUri,
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                systemLanguage,
                isShortListedInOtherJob,
                isHiredInOtherJob,
                brief,
                invitedAt,
                hasApplied);

            var candidates = new List<JobSelectedCandidate>
            {
                selectedCandidate
            };

            jobCandidates.AddSelectedCandidates(candidates);

            // Act
            var action = () => jobCandidates.ShareShortlistViaEmail(
                clientEmail, 
                clientFirstName, 
                systemLanguage,
                contactExternalId);

            // Assert
            Assert.Throws<DomainException>(action);
        }

        [Theory, AutoMoqData]
        public void ArchiveCandidate_ShouldRemoveCandidateFromSelectedAndAddToArchived_WhenCandidateExist(
            Guid jobId,
            JobStage jobStage,
            Position position,
            Guid companyId,
            string companyName,
            string companyLogoUri,
            Guid candidateIdFirst,
            Guid candidateIdSecond,
            Guid candidateIdThird,
            string firstName,
            string lastName,
            string email,
            string? phoneNumber,
            string? pictureUri,
            bool isShortListedInOtherJob,
            bool isHiredInOtherJob,
            WorkType? freelance,
            WorkType? permanent,
            DateTimeOffset? startDate,
            DateTimeOffset? deadlineDate,
            string? brief,
            DateTimeOffset? invitedAt,
            SystemLanguage? systemLanguage,
            bool hasApplied)
        {
            // Arrange
            var jobCandidates = new JobCandidates(
                 jobId,
                jobStage,
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                companyId,
                companyName,
                companyLogoUri,
                freelance,
                permanent,
                deadlineDate,
                startDate);

            var selectedCandidateFirst = new JobSelectedCandidate(
                jobId,
                candidateIdFirst,
                firstName,
                lastName,
                email,
                phoneNumber,
                pictureUri,
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                systemLanguage,
                isShortListedInOtherJob, 
                isHiredInOtherJob,
                brief,
                invitedAt,
                hasApplied);

            var selectedCandidateSecond = new JobSelectedCandidate(
                jobId,
                candidateIdSecond,
                firstName,
                lastName,
                email,
                phoneNumber,
                pictureUri,
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                systemLanguage,
                isShortListedInOtherJob, 
                isHiredInOtherJob,
                brief,
                invitedAt,
                hasApplied);

            var selectedCandidateThird = new JobSelectedCandidate(
                jobId,
                candidateIdThird,
                firstName,
                lastName,
                email,
                phoneNumber,
                pictureUri,
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                systemLanguage,
                isShortListedInOtherJob, 
                isHiredInOtherJob,
                brief,
                invitedAt,
                hasApplied);

            var candidates = new List<JobSelectedCandidate>
            {
                selectedCandidateFirst,
                selectedCandidateSecond,
                selectedCandidateThird
            };

            jobCandidates.AddSelectedCandidates(candidates);

            var candidatesToArchive = new List<JobSelectedCandidate>
            {
                selectedCandidateFirst,
                selectedCandidateThird
            };

            var archivedStage = ArchivedCandidateStage.NotRelevant;

            //Act
            jobCandidates.ArchiveCandidates(candidatesToArchive.Select(x => x.CandidateId), archivedStage);

            //Assert
            Assert.Single(jobCandidates.SelectedCandidates);
            Assert.Equal(2, jobCandidates.ArchivedCandidates.Count);
        }

        [Theory, AutoMoqData]
        public void ArchiveCandidate_ShouldThrowDomainException_WhenJobDoesNotContainCandidates(
            Guid jobId,
            JobStage jobStage,
            Position position,
            Guid companyId,
            string companyName,
            string companyLogoUri,
            Guid candidateId,
            string firstName,
            string lastName,
            string email,
            string? phoneNumber,
            string? pictureUri,
            ArchivedCandidateStage archivedCandidateStage,
            Guid notExistingCandidateId,
            bool isShortListedInOtherJob,
            bool isHiredInOtherJob,
            WorkType? freelance,
            WorkType? permanent,
            DateTimeOffset? startDate,
            DateTimeOffset? deadlineDate,
            string? brief,
            DateTimeOffset? invitedAt,
            SystemLanguage? systemLanguage,
            bool hasApplied)
        {
            // Arrange
            var jobCandidates = new JobCandidates(
                jobId,
                jobStage,
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                companyId,
                companyName,
                companyLogoUri,
                freelance,
                permanent,
                deadlineDate,
                startDate);

            var selectedCandidate = new JobSelectedCandidate(
                jobId,
                candidateId,
                firstName,
                lastName,
                email,
                phoneNumber,
                pictureUri,
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                systemLanguage,
                isShortListedInOtherJob, 
                isHiredInOtherJob,
                brief,
                invitedAt,
                hasApplied);

            var candidates = new List<JobSelectedCandidate>
            {
                selectedCandidate
            };

            jobCandidates.AddSelectedCandidates(candidates);

            // Act
            var action = () => jobCandidates.ArchiveCandidates(new List<Guid> { notExistingCandidateId }, archivedCandidateStage);

            // Assert
            Assert.Throws<DomainException>(action);
        }

        [Theory, AutoMoqData]
        public void ActivateArchiveCandidate_ShouldRemoveCandidateFromArchivedCandidates(
            Guid jobId,
            JobStage jobStage,
            Position position,
            Guid companyId,
            string companyName,
            string companyLogoUri,
            Guid candidateIdFirst,
            Guid candidateIdSecond,
            string firstName,
            string lastName,
            string email,
            string? phoneNumber,
            string? pictureUri,
            bool isShortListedInOtherJob,
            bool isHiredInOtherJob,
            WorkType? freelance,
            WorkType? permanent,
            DateTimeOffset? startDate,
            DateTimeOffset? deadlineDate,
            string? brief,
            DateTimeOffset? invitedAt,
            SystemLanguage? systemLanguage,
            bool hasApplied)
        {
            // Arrange
            var jobCandidates = new JobCandidates(
                jobId,
                jobStage,
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                companyId,
                companyName,
                companyLogoUri,
                freelance,
                permanent,
                deadlineDate,
                startDate);

            var selectedCandidateFirst = new JobSelectedCandidate(
                jobId,
                candidateIdFirst,
                firstName,
                lastName,
                email,
                phoneNumber,
                pictureUri,
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                systemLanguage,
                isShortListedInOtherJob,
                isHiredInOtherJob,
                brief,
                invitedAt,
                hasApplied);

            var selectedCandidateSecond = new JobSelectedCandidate(
                jobId,
                candidateIdSecond,
                firstName,
                lastName,
                email,
                phoneNumber,
                pictureUri,
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                systemLanguage,
                isShortListedInOtherJob,
                isHiredInOtherJob,
                brief,
                invitedAt,
                hasApplied);

            var candidates = new List<JobSelectedCandidate>
            {
                selectedCandidateFirst,
                selectedCandidateSecond
            };

            jobCandidates.AddSelectedCandidates(candidates);

            var candidatesToArchive = new List<JobSelectedCandidate>
            {
                selectedCandidateFirst,
                selectedCandidateSecond
            };

            var archivedStage = ArchivedCandidateStage.NotRelevant;
            jobCandidates.ArchiveCandidates(candidatesToArchive.Select(x => x.CandidateId), archivedStage);

            //Act
            jobCandidates.ActivateArchivedCandidates(new List<(Guid, bool, bool)> { (selectedCandidateFirst.CandidateId, false, false) });

            //Assert
            Assert.Single(jobCandidates.ArchivedCandidates);
            Assert.Single(jobCandidates.SelectedCandidates);
        }

        [Theory, AutoMoqData]
        public void ActivateArchiveCandidate_ShouldThrowDomainException_WhenJobDoesNotContainCandidates(
            Guid jobId,
            JobStage jobStage,
            Position position,
            Guid companyId,
            string companyName,
            string companyLogoUri,
            Guid candidateId,
            string firstName,
            string lastName,
            string email,
            string? phoneNumber,
            string? pictureUri,
            Guid notExistingCandidateId,
            bool isShortListedInOtherJob,
            bool isHiredInOtherJob,
            WorkType? freelance,
            WorkType? permanent,
            DateTimeOffset? startDate,
            DateTimeOffset? deadlineDate,
            string? brief,
            DateTimeOffset? invitedAt,
            SystemLanguage? systemLanguage,
            bool hasApplied)
        {
            // Arrange
            var jobCandidates = new JobCandidates(
                 jobId,
                jobStage,
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                companyId,
                companyName,
                companyLogoUri,
                freelance,
                permanent,
                deadlineDate,
                startDate);

            var selectedCandidate = new JobSelectedCandidate(
                jobId,
                candidateId,
                firstName,
                lastName,
                email,
                phoneNumber,
                pictureUri,
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                systemLanguage,
                isShortListedInOtherJob, 
                isHiredInOtherJob,
                brief,
                invitedAt,
                hasApplied);

            var candidates = new List<JobSelectedCandidate>
            {
                selectedCandidate
            };

            jobCandidates.AddSelectedCandidates(candidates);

            var archivedCandidates = candidates.Select(x => x.CandidateId);
            jobCandidates.ArchiveCandidates(archivedCandidates, ArchivedCandidateStage.NotInterested);

            // Act
            var action = () => jobCandidates.ActivateArchivedCandidates(new List<(Guid, bool, bool)> { (notExistingCandidateId,  false, false) });

            // Assert
            Assert.Throws<DomainException>(action);
        }

        [Theory, AutoMoqData]
        public void UpdateSelectedCandidate_ShouldThrowDomainException_WhenJobDoesNotContainCandidates(
            Guid jobId,
            JobStage jobStage,
            Position position,
            Guid companyId,
            string companyName,
            string companyLogoUri,
            Guid candidateId,
            string firstName,
            string lastName,
            string email,
            string? phoneNumber,
            string? pictureUri,
            SelectedCandidateStage stage,
            Guid notExistingCandidateId,
            bool isShortListedInOtherJob,
            bool isHiredInOtherJob,
            WorkType? freelance,
            WorkType? permanent,
            DateTimeOffset? startDate,
            DateTimeOffset? deadlineDate,
            string? brief,
            DateTimeOffset? invitedAt,
            SystemLanguage? systemLanguage,
            bool hasApplied)
        {
            // Arrange
            var jobCandidates = new JobCandidates(
                 jobId,
                jobStage,
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                companyId,
                companyName,
                companyLogoUri,
                freelance,
                permanent,
                deadlineDate,
                startDate);

            var selectedCandidate = new JobSelectedCandidate(
                jobId,
                candidateId,
                firstName,
                lastName,
                email,
                phoneNumber,
                pictureUri,
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                systemLanguage,
                isShortListedInOtherJob, 
                isHiredInOtherJob,
                brief,
                invitedAt,
                hasApplied);

            var candidates = new List<JobSelectedCandidate>
            {
                selectedCandidate
            };

            jobCandidates.AddSelectedCandidates(candidates);

            // Act
            var action = () => jobCandidates.UpdateSelectedCandidates(new List<Guid> { notExistingCandidateId }, stage);

            // Assert
            Assert.Throws<DomainException>(action);
        }

        [Theory, AutoMoqData]
        public void SyncJob_ShouldSyncJobDataToJobCandidates(
            JobCandidates jobCandidates,
            Position position,
            WorkType? freelance,
            WorkType? permanent,
            DateTimeOffset? deadlineDate,
            DateTimeOffset? startDate)
        {
            // Arrange

            // Act
            jobCandidates.SyncJob(position, freelance, permanent, deadlineDate, startDate);

            // Assert
            Assert.Equal(position, jobCandidates.Position);
            Assert.Equal(freelance, jobCandidates.Freelance);
            Assert.Equal(permanent, jobCandidates.Permanent);
            Assert.Equal(deadlineDate, jobCandidates.DeadlineDate);
            Assert.Equal(startDate, jobCandidates.StartDate);
        }

        [Theory]
        [InlineAutoMoqData(false)]
        public void UpdateSelectedCandidate_ShouldGenerateRankingsConsistently_WhenNewStageIsShortlisted(
            bool isShortListedInOtherJob,
            Guid jobId,
            JobStage jobStage,
            Position position,
            Guid companyId,
            string companyName,
            string companyLogoUri,
            string firstName,
            string lastName,
            string email,
            string? phoneNumber,
            string? pictureUri,
            bool isHiredInOtherJob,
            WorkType? freelance,
            WorkType? permanent,
            DateTimeOffset? startDate,
            DateTimeOffset? deadlineDate,
            string? brief,
            DateTimeOffset? invitedAt,
            SystemLanguage? systemLanguage,
            bool hasApplied)
        {
            // Arrange
            var candidate1ExpectedRanking = 1;
            var candidate2ExpectedRanking = 2;

            var jobCandidates = new JobCandidates(
                jobId,
                jobStage,
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                companyId,
                companyName,
                companyLogoUri,
                freelance,
                permanent,
                deadlineDate,
                startDate);

            var candidate1 = new JobSelectedCandidate(
                jobId,
                Guid.NewGuid(),
                firstName,
                lastName,
                email,
                phoneNumber,
                pictureUri,
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                systemLanguage,
                isShortListedInOtherJob,
                isHiredInOtherJob,
                brief,
                invitedAt,
                hasApplied);

            var candidate2 = new JobSelectedCandidate(
                jobId,
                Guid.NewGuid(),
                firstName,
                lastName,
                email,
                phoneNumber,
                pictureUri,
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                systemLanguage,
                isShortListedInOtherJob,
                isHiredInOtherJob,
                brief,
                invitedAt,
                hasApplied);

            jobCandidates.AddSelectedCandidates(
                new List<JobSelectedCandidate> { candidate1, candidate2 }
             );

            // Act
            jobCandidates.UpdateSelectedCandidates(
                new List<Guid> { candidate1.CandidateId, candidate2.CandidateId },
                SelectedCandidateStage.FirstInterview);

            // Assert 
            Assert.Equal(candidate1ExpectedRanking, candidate1.Ranking);
            Assert.Equal(candidate2ExpectedRanking, candidate2.Ranking);
        }

        [Theory, AutoMoqData]
        public void UpdateRaning_ShouldThrowException_WhenDuplicatedRankings(
            Guid jobId,
            JobStage jobStage,
            Position position,
            Guid companyId,
            string companyName,
            string companyLogoUri,
            WorkType? freelance,
            WorkType? permanent,
            DateTimeOffset? startDate,
            DateTimeOffset? deadlineDate)
        {
            // Arrange
            var rankings = new List<(Guid, int)> { (Guid.NewGuid(), 1), (Guid.NewGuid(), 2), (Guid.NewGuid(), 2) };

            var jobCandidates = new JobCandidates(
                jobId,
                jobStage,
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                companyId,
                companyName,
                companyLogoUri,
                freelance,
                permanent,
                deadlineDate,
                startDate);

            // Act
            var action = () => jobCandidates.UpdateSelectedCandidatesRanking(rankings);

            // Assert
            Assert.Throws<DomainException>(action);
        }

        [Theory]
        [InlineAutoMoqData(null)]
        public void InviteJobCandidate_ShouldNotAddInvitedDomainEvent_WhenEmailNotProvided(
            string? email,
            bool isShortListedInOtherJob,
            Guid jobId,
            JobStage jobStage,
            Position position,
            Guid companyId,
            string companyName,
            string companyLogoUri,
            string firstName,
            string lastName,
            string? phoneNumber,
            string? pictureUri,
            bool isHiredInOtherJob,
            WorkType? freelance,
            WorkType? permanent,
            DateTimeOffset? startDate,
            DateTimeOffset? deadlineDate,
            string? brief,
            DateTimeOffset? invitedAt,
            SystemLanguage? systemLanguage,
            bool hasApplied)
        {
            // Arrange
            var jobCandidates = new JobCandidates(
                jobId,
                jobStage,
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                companyId,
                companyName,
                companyLogoUri,
                freelance,
                permanent,
                deadlineDate,
                startDate);

            var candidate1 = new JobSelectedCandidate(
                jobId,
                Guid.NewGuid(),
                firstName,
                lastName,
                email,
                phoneNumber,
                pictureUri,
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                systemLanguage,
                isShortListedInOtherJob,
                isHiredInOtherJob,
                brief,
                invitedAt,
                hasApplied);

            jobCandidates.AddSelectedCandidates(
                new List<JobSelectedCandidate> { candidate1 }
             );
            // Act
            jobCandidates.InviteSelectedCandidatesViaEmail(
                new List<Guid> { candidate1.CandidateId });

            // Assert
            var domainEvent = jobCandidates
                .DomainEvents
                .Where(x => x as JobSelectedCandidateInvitedDomainEvent != null)
                .SingleOrDefault();
            Assert.Null(domainEvent);
        }

        [Theory, AutoMoqData]
        public void SyncJobRejection_ShouldAddCandidateToArchivedFromSelected(
            string email,
            bool isShortListedInOtherJob,
            Guid jobId,
            JobStage jobStage,
            Position position,
            Guid companyId,
            string companyName,
            string companyLogoUri,
            string firstName,
            string lastName,
            string? phoneNumber,
            string? pictureUri,
            bool isHiredInOtherJob,
            WorkType? freelance,
            WorkType? permanent,
            DateTimeOffset? startDate,
            DateTimeOffset? deadlineDate,
            string? brief,
            DateTimeOffset? invitedAt,
            SystemLanguage? systemLanguage,
            bool hasApplied)
        {
            // Arrange
            var candidateId = Guid.NewGuid();
            var expectedSelectedCandidatesCount = 0;
            var expectedArchivedCandidatesCount = 1;
            var expectedArchivedCandidateStage = ArchivedCandidateStage.NotInterested;

            var jobCandidates = new JobCandidates(
                jobId,
                jobStage,
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                companyId,
                companyName,
                companyLogoUri,
                freelance,
                permanent,
                deadlineDate,
                startDate);

            var selectedCandidate = new JobSelectedCandidate(
                jobId,
                candidateId,
                firstName,
                lastName,
                email,
                phoneNumber,
                pictureUri,
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                systemLanguage,
                isShortListedInOtherJob,
                isHiredInOtherJob,
                brief,
                invitedAt,
                hasApplied);

            jobCandidates.AddSelectedCandidates(new List<JobSelectedCandidate> { selectedCandidate });

            // Act
            jobCandidates.SyncJobRejection(candidateId, ArchivedCandidateStage.NotInterested);

            // Assert
            Assert.Equal(expectedSelectedCandidatesCount, jobCandidates.SelectedCandidates.Count);
            Assert.Equal(expectedArchivedCandidatesCount, jobCandidates.ArchivedCandidates.Count);
            Assert.Equal(expectedArchivedCandidateStage, jobCandidates.ArchivedCandidates.First().Stage);
        }

        [Theory, AutoMoqData]
        public void SyncApplyToJob_ShouldCreateNewJobCandidate_WhenNotExist(
            Guid candidateId,
            string candidateFirstName,
            string candidateLastName,
            string? candidateEmail,
            string? candidatePhoneNumber,
            string? candidatePictureUri,
            Position? candidatePosition,
            SystemLanguage? candidateSystemLanguage,
            Guid jobId,
            JobStage jobStage,
            Position position,
            Guid companyId,
            string companyName,
            string companyLogoUri,
            WorkType? freelance,
            WorkType? permanent,
            DateTimeOffset? startDate,
            DateTimeOffset? deadlineDate)
        {
            // Arrange
            var expectedSelectedCandidateCount = 1;
            var expectedSelectedCandidatehasApplied = true;

            var jobCandidates = new JobCandidates(
                jobId,
                jobStage,
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                companyId,
                companyName,
                companyLogoUri,
                freelance,
                permanent,
                deadlineDate,
                startDate);

            // Act
            jobCandidates.SyncApplyToJob(
                candidateId,
                candidateFirstName,
                candidateLastName,
                candidateEmail,
                candidatePhoneNumber,
                candidatePictureUri,
                candidatePosition,
                candidateSystemLanguage);

            // Assert
            Assert.Equal(expectedSelectedCandidateCount, jobCandidates.SelectedCandidates.Count);
            Assert.Equal(expectedSelectedCandidatehasApplied, jobCandidates.SelectedCandidates.First().HasApplied);
        }

        [Theory, AutoMoqData]
        public void SyncApplyToJob_ShouldChangeStageToInterested_WhenCurrentStageInvitePendingAndInvitationSent(
            Guid candidateId,
            string candidateFirstName,
            string candidateLastName,
            string? candidateEmail,
            string? candidatePhoneNumber,
            string? candidatePictureUri,
            Position? candidatePosition,
            SystemLanguage? candidateSystemLanguage,
            Guid jobId,
            JobStage jobStage,
            Position position,
            Guid companyId,
            string companyName,
            string companyLogoUri,
            WorkType? freelance,
            WorkType? permanent,
            DateTimeOffset? startDate,
            DateTimeOffset? deadlineDate,
            string firstName,
            string lastName,
            string? phoneNumber,
            string? pictureUri,
            bool isHiredInOtherJob,
            string email,
            SystemLanguage? systemLanguage,
            bool isShortListedInOtherJob,
            string? brief)
        {
            // Arrange
            var expectedSelectedCandidateCount = 1;
            var expectedSelectedCandidatehasApplied = true;
            var expectedSelectedCandidateStage = SelectedCandidateStage.Interested;

            var jobCandidates = new JobCandidates(
                jobId,
                jobStage,
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                companyId,
                companyName,
                companyLogoUri,
                freelance,
                permanent,
                deadlineDate,
                startDate);

            var selectedCandidate = new JobSelectedCandidate(
                jobId,
                candidateId,
                firstName,
                lastName,
                email,
                phoneNumber,
                pictureUri,
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                systemLanguage,
                isShortListedInOtherJob,
                isHiredInOtherJob,
                brief,
                DateTimeOffset.UtcNow,
                false);

            selectedCandidate.UpdateStage(SelectedCandidateStage.InvitePending);

            jobCandidates.AddSelectedCandidates(new List<JobSelectedCandidate> { selectedCandidate });

            // Act
            jobCandidates.SyncApplyToJob(
                candidateId,
                candidateFirstName,
                candidateLastName,
                candidateEmail,
                candidatePhoneNumber,
                candidatePictureUri,
                candidatePosition,
                candidateSystemLanguage);

            // Assert
            Assert.Equal(expectedSelectedCandidateCount, jobCandidates.SelectedCandidates.Count);
            Assert.Equal(expectedSelectedCandidatehasApplied, jobCandidates.SelectedCandidates.First().HasApplied);
            Assert.Equal(expectedSelectedCandidateStage, jobCandidates.SelectedCandidates.First().Stage);
        }

        [Theory, AutoMoqData]
        public void SyncJobPositions_ShouldSyncJobPosition(
            Guid newPositionAliasId,
            string newPositionAliasCode,
            Guid jobId,
            JobStage jobStage,
            Position position,
            Guid companyId,
            string companyName,
            string companyLogoUri,
            WorkType? freelance,
            WorkType? permanent,
            DateTimeOffset? startDate,
            DateTimeOffset? deadlineDate)
        {
            // Arrange
            var jobCandidates = new JobCandidates(
                jobId,
                jobStage,
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                companyId,
                companyName,
                companyLogoUri,
                freelance,
                permanent,
                deadlineDate,
                startDate);

            // Act
            jobCandidates.SyncJobPositions(position.Id, newPositionAliasId, newPositionAliasCode);

            // Assert
            Assert.Equal(position.Id, jobCandidates.Position.Id);
            Assert.Equal(position.Code, jobCandidates.Position.Code);
            Assert.Equal(newPositionAliasId, jobCandidates.Position.AliasTo?.Id);
            Assert.Equal(newPositionAliasCode, jobCandidates.Position.AliasTo?.Code);
        }
    }
}
