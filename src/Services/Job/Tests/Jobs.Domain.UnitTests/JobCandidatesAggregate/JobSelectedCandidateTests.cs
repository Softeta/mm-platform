using Domain.Seedwork.Enums;
using Domain.Seedwork.Exceptions;
using Domain.Seedwork.Shared.ValueObjects;
using Jobs.Domain.Aggregates.JobCandidatesAggregate.Entities;
using System;
using Tests.Shared;
using Xunit;

namespace Jobs.Domain.UnitTests.JobCandidatesAggregate
{
    public class JobSelectedCandidateTests
    {
        [Theory, AutoMoqData]
        public void CreateJobSelectedCandidate_ShouldCreate(
            Guid jobId,
            Guid candidateId,
            string firstName,
            string lastName,
            string email,
            string? phoneNumber,
            string? pictureUri,
            Position position,
            bool isShortListedInOtherJob, 
            bool isHiredInOtherJob,
            string? brief,
            DateTimeOffset? invitedAt,
            SystemLanguage? systemLanguage,
            bool hasApplied)
        {
            // Arrange 
            var expectedSelectedCandidateStage = SelectedCandidateStage.New;

            // Act
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

            // Assert 
            Assert.Equal(jobId, selectedCandidate.JobId);
            Assert.Equal(candidateId, selectedCandidate.CandidateId);
            Assert.Equal(firstName, selectedCandidate.FirstName);
            Assert.Equal(lastName, selectedCandidate.LastName);
            Assert.Equal(email, selectedCandidate.Email);
            Assert.Equal(pictureUri, selectedCandidate.PictureUri);
            Assert.Equal(position.Id, selectedCandidate.Position?.Id);
            Assert.Equal(position.Code, selectedCandidate.Position?.Code);
            Assert.Equal(position.AliasTo?.Id, selectedCandidate.Position?.AliasTo?.Id);
            Assert.Equal(position.AliasTo?.Code, selectedCandidate.Position?.AliasTo?.Code);
            Assert.False(selectedCandidate.IsShortListed);
            Assert.Equal(expectedSelectedCandidateStage, selectedCandidate.Stage);
            Assert.False(selectedCandidate.IsHiredInOtherJob);
            Assert.Equal(brief, selectedCandidate.Brief);
            Assert.Equal(invitedAt, selectedCandidate.InvitedAt);
            Assert.Equal(systemLanguage, selectedCandidate.SystemLanguage);
        }

        [Theory]
        [InlineAutoMoqData(default)]
        public void CreateJobSelectedCandidate_ShouldThrowDomainException_WhenDefaultCandidateId(
            Guid candidateId,
            Guid jobId,
            string firstName,
            string lastName,
            string email,
            string? phoneNumber,
            string? pictureUri,
            Position position,
            bool isShortListedInOtherJob, 
            bool isHiredInOtherJob,
            string? brief,
            DateTimeOffset? invitedAt,
            SystemLanguage? systemLanguage,
            bool hasApplied)
        {
            // Act
            var action = () => new JobSelectedCandidate(
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

            // Assert
            Assert.Throws<DomainException>(action);
        }

        [Theory]
        [InlineAutoMoqData(null)]
        [InlineAutoMoqData("")]
        [InlineAutoMoqData("   ")]
        public void CreateJobSelectedCandidate_ShouldThrowDomainException_WhenNoFirstName(
            string firstName,
            Guid candidateId,
            Guid jobId,
            string lastName,
            string email,
            string? phoneNumber,
            string? pictureUri,
            Position position,
            bool isShortListedInOtherJob,
            bool isHiredInOtherJob,
            string? brief,
            DateTimeOffset? invitedAt,
            SystemLanguage? systemLanguage,
            bool hasApplied)
        {
            // Act
            var action = () => new JobSelectedCandidate(
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

            // Assert
            Assert.Throws<DomainException>(action);
        }

        [Theory]
        [InlineAutoMoqData(SelectedCandidateStage.New)]
        [InlineAutoMoqData(SelectedCandidateStage.Interested)]
        [InlineAutoMoqData(SelectedCandidateStage.InvitePending)]
        [InlineAutoMoqData(SelectedCandidateStage.ThirdInterview)]
        public void UpdateStage_ShouldUpdateEntityWithNewStage(
            SelectedCandidateStage stage,
            Guid jobId,
            Guid candidateId,
            string firstName,
            string lastName,
            string email,
            string? phoneNumber,
            string? pictureUri,
            Position position,
            string? brief,
            DateTimeOffset? invitedAt,
            SystemLanguage? systemLanguage,
            bool hasApplied)
        {
            //Arrange
            var isShortListedInOtherJob = false;
            var isHiredInOtherJob = false;

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

            // Act
            selectedCandidate.UpdateStage(stage);

            // Assert 
            Assert.Equal(stage, selectedCandidate.Stage);
        }

        [Theory, AutoMoqData]
        public void UpdateIsShortlistedInOtherJob_ShouldUpdateIsShortlistedInOtherJob(
            Guid jobId,
            Guid candidateId,
            string firstName,
            string lastName,
            string email,
            string? phoneNumber,
            string? pictureUri,
            Position position,
            bool isShortListedInOtherJob,
            bool expectedIsShortListedInOtherJob,
            bool isHiredInOtherJob,
            string? brief,
            DateTimeOffset? invitedAt,
            SystemLanguage? systemLanguage,
            bool hasApplied)
        {
            //Arrange
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

            // Act
            selectedCandidate.UpdateIsShortlistedInOtherJob(expectedIsShortListedInOtherJob);

            // Assert 
            Assert.Equal(expectedIsShortListedInOtherJob, selectedCandidate.IsShortListedInOtherJob);
        }

        [Theory]
        [InlineAutoMoqData(SelectedCandidateStage.Hired, false)]
        [InlineAutoMoqData(SelectedCandidateStage.Interested, false)]
        [InlineAutoMoqData(SelectedCandidateStage.InvitePending, false)]
        [InlineAutoMoqData(SelectedCandidateStage.NoInterview, true)]
        [InlineAutoMoqData(SelectedCandidateStage.FirstInterview, true)]
        [InlineAutoMoqData(SelectedCandidateStage.SecondInterview, true)]
        [InlineAutoMoqData(SelectedCandidateStage.ThirdInterview, true)]
        public void IsShortListedGetter_ShouldReturnCorrectValue(
            SelectedCandidateStage newStage,
            bool expected,
            Position position,
            string brief,
            DateTimeOffset? invitedAt,
            SystemLanguage? systemLanguage,
            bool hasApplied)
        {
            //Arrange
            var isShortlistedInOtherJob = false;
            var isHiredInOtherJob = false;

            var selectedCandidate = new JobSelectedCandidate(
                Guid.NewGuid(),
                Guid.NewGuid(),
                "firstName",
                "lastName",
                "email",
                "phoneNumber",
                "pictureUri",
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                systemLanguage,
                isShortlistedInOtherJob,
                isHiredInOtherJob,
                brief,
                invitedAt,
                hasApplied);

            // Act
            selectedCandidate.UpdateStage(newStage);

            //Assert 
            Assert.Equal(expected, selectedCandidate.IsShortListed);
        }

        [Theory]
        [InlineAutoMoqData(1, false)]
        [InlineAutoMoqData(5, false)]
        [InlineAutoMoqData(10, false)]
        public void UpdateRanking_ShouldUpdateRanking_WhenStageIsShortlisted(
            int ranking,
            bool isShortListedInOtherJob,
            Guid jobId,
            Guid candidateId,
            string firstName,
            string lastName,
            string email,
            string? phoneNumber,
            string? pictureUri,
            Position position,
            bool isHiredInOtherJob,
            string? brief,
            DateTimeOffset? invitedAt,
            SystemLanguage? systemLanguage,
            bool hasApplied)
        {
            // Arrange 
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

            selectedCandidate.UpdateStage(SelectedCandidateStage.FirstInterview);

            // Act
            selectedCandidate.UpdateRanking(ranking);

            // Assert
            Assert.Equal(ranking, selectedCandidate.Ranking);
        }

        [Theory]
        [InlineAutoMoqData(false)]
        public void UpdateRanking_ShouldThrowException_WhenStageNotShortlisted(
            bool isShortListedInOtherJob,
            int ranking,
            Guid jobId,
            Guid candidateId,
            string firstName,
            string lastName,
            string email,
            string? phoneNumber,
            string? pictureUri,
            Position position,
            bool isHiredInOtherJob,
            string? brief,
            DateTimeOffset? invitedAt,
            SystemLanguage? systemLanguage,
            bool hasApplied)
        {
            // Arrange 
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

            // Act
            var action = () => selectedCandidate.UpdateRanking(ranking);

            // Assert
            Assert.Throws<DomainException>(action);
        }

        [Theory]
        [InlineAutoMoqData(0, false)]
        [InlineAutoMoqData(-1, false)]
        [InlineAutoMoqData(-5, false)]
        public void UpdateRanking_ShouldThrowException_WhenRankingLessThan1(
            int ranking,
            bool isShortListedInOtherJob,
            Guid jobId,
            Guid candidateId,
            string firstName,
            string lastName,
            string email,
            string? phoneNumber,
            string? pictureUri,
            Position position,
            bool isHiredInOtherJob,
            string? brief,
            DateTimeOffset? invitedAt,
            SystemLanguage? systemLanguage,
            bool hasApplied)
        {
            // Arrange 
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

            // Act
            var action = () => selectedCandidate.UpdateRanking(ranking);

            // Assert
            Assert.Throws<DomainException>(action);
        }

        [Theory, AutoMoqData]
        public void UpdateBrief_ShouldUpdateBrief(
            bool isShortListedInOtherJob,
            Guid jobId,
            Guid candidateId,
            string firstName,
            string lastName,
            string email,
            string? phoneNumber,
            string? pictureUri,
            Position position,
            bool isHiredInOtherJob,
            string? brief,
            DateTimeOffset? invitedAt,
            SystemLanguage? systemLanguage,
            bool hasApplied)
        {
            // Arrange 
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
                 null,
                 invitedAt,
                 hasApplied);

            // Act
            selectedCandidate.UpdateBrief(brief);

            // Arrange 
            Assert.Equal(brief, selectedCandidate.Brief);
        }

        [Theory, AutoMoqData]
        public void Invite_ShouldUpdateSelectedCandidate(
            SelectedCandidateStage stage,
            bool isShortListedInOtherJob,
            Guid jobId,
            Guid candidateId,
            string firstName,
            string lastName,
            string email,
            string? phoneNumber,
            string? pictureUri,
            Position position,
            bool isHiredInOtherJob,
            DateTimeOffset? invitedAt,
            SystemLanguage? systemLanguage,
            bool hasApplied)
        {
            // Arrange 
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
                 null,
                 invitedAt,
                 hasApplied);

            selectedCandidate.UpdateStage(stage);

            // Act
            selectedCandidate.Invite();

            // Arrange 
            Assert.NotNull(selectedCandidate.InvitedAt);
            if (stage == SelectedCandidateStage.New)
            {
                Assert.Equal(SelectedCandidateStage.InvitePending, selectedCandidate.Stage);
            }
            else
            {
                Assert.Equal(stage, selectedCandidate.Stage);
            }
        }

        [Theory, AutoMoqData]
        public void SyncJobPosition_ShouldSync(
            Guid newPositionAliasId,
            string newPositionAliasCode,
            bool isShortListedInOtherJob,
            Guid jobId,
            Guid candidateId,
            string firstName,
            string lastName,
            string email,
            string? phoneNumber,
            string? pictureUri,
            Position position,
            bool isHiredInOtherJob,
            DateTimeOffset? invitedAt,
            SystemLanguage? systemLanguage,
            bool hasApplied)
        {
            // Arrange 
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
                 null,
                 invitedAt,
                 hasApplied);

            // Act
            selectedCandidate.SyncJobPosition(newPositionAliasId, newPositionAliasCode);

            // Arrange 
            Assert.Equal(position.Id, selectedCandidate.Position?.Id);
            Assert.Equal(position.Code, selectedCandidate.Position?.Code);
            Assert.Equal(newPositionAliasId, selectedCandidate?.Position?.AliasTo?.Id);
            Assert.Equal(newPositionAliasCode, selectedCandidate?.Position?.AliasTo?.Code);
        }
    }
}
