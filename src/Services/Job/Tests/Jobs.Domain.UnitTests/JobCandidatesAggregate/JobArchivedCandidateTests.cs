using Domain.Seedwork.Enums;
using Domain.Seedwork.Exceptions;
using Domain.Seedwork.Shared.ValueObjects;
using Jobs.Domain.Aggregates.JobCandidatesAggregate.Entities;
using System;
using Tests.Shared;
using Xunit;

namespace Jobs.Domain.UnitTests.JobCandidatesAggregate
{
    public class JobArchivedCandidateTests
    {
        [Theory, AutoMoqData]
        public void CreateJobArchivedCandidate_ShouldCreate(
            Guid jobId,
            Guid candidateId,
            string firstName,
            string lastName,
            string email,
            string? phoneNumber,
            string? pictureUri,
            Position position,
            ArchivedCandidateStage stage,
            string? brief,
            DateTimeOffset? invitedAt,
            SystemLanguage? systemLanguage,
            bool hasApplied)
        {
            // Act
            var archivedCandidate = new JobArchivedCandidate(
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
                stage,
                brief,
                invitedAt,
                hasApplied);

            // Assert 
            Assert.Equal(jobId, archivedCandidate.JobId);
            Assert.Equal(candidateId, archivedCandidate.CandidateId);
            Assert.Equal(firstName, archivedCandidate.FirstName);
            Assert.Equal(lastName, archivedCandidate.LastName);
            Assert.Equal(email, archivedCandidate.Email);
            Assert.Equal(pictureUri, archivedCandidate.PictureUri);
            Assert.Equal(position.Id, archivedCandidate.Position?.Id);
            Assert.Equal(position.Code, archivedCandidate.Position?.Code);
            Assert.Equal(position.AliasTo?.Id, archivedCandidate.Position?.AliasTo?.Id);
            Assert.Equal(position.AliasTo?.Code, archivedCandidate.Position?.AliasTo?.Code);
            Assert.Equal(stage, archivedCandidate.Stage);
            Assert.Equal(brief, archivedCandidate.Brief);
            Assert.Equal(systemLanguage, archivedCandidate.SystemLanguage);
        }

        [Theory]
        [InlineAutoMoqData(default)]
        public void CreateJobArchivedCandidate_ShouldThrowDomainException_WhenDefaultCandidateId(
            Guid candidateId,
            Guid jobId,
            string firstName,
            string lastName,
            string email,
            string? phoneNumber,
            string? pictureUri,
            Position position,
            ArchivedCandidateStage stage,
            string? brief,
            DateTimeOffset? invitedAt,
            SystemLanguage? systemLanguage,
            bool hasApplied)
        {
            // Act
            var action = () => new JobArchivedCandidate(
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
                stage,
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
        public void CreateJobArchivedCandidate_ShouldThrowDomainException_WhenNoFirstName(
            string firstName,
            Guid candidateId,
            Guid jobId,
            string lastName,
            string email,
            string? phoneNumber,
            string? pictureUri,
            Position position,
            ArchivedCandidateStage stage,
            string? brief,
            DateTimeOffset? invitedAt,
            SystemLanguage? systemLanguage,
            bool hasApplied)
        {
            // Act
            var action = () => new JobArchivedCandidate(
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
                stage,
                brief,
                invitedAt,
                hasApplied);

            // Assert
            Assert.Throws<DomainException>(action);
        }

        [Theory, AutoMoqData]
        public void UpdateBrief_ShouldUpdateBrief(
            Guid jobId,
            Guid candidateId,
            string firstName,
            string lastName,
            string email,
            string? phoneNumber,
            string? pictureUri,
            Position position,
            ArchivedCandidateStage stage,
            string? brief,
            DateTimeOffset? invitedAt,
            SystemLanguage? systemLanguage,
            bool hasApplied)
        {
            // Arrange 
            var archivedCandidate = new JobArchivedCandidate(
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
                stage,
                null,
                invitedAt,
                hasApplied);

            // Act
            archivedCandidate.UpdateBrief(brief);

            // Arrange 
            Assert.Equal(brief, archivedCandidate.Brief);
        }

        [Theory, AutoMoqData]
        public void SyncJobPosition_ShouldSync(
            Guid newPositionAliasId,
            string newPositionAliasCode,
            Guid jobId,
            Guid candidateId,
            string firstName,
            string lastName,
            string email,
            string? phoneNumber,
            string? pictureUri,
            Position position,
            ArchivedCandidateStage stage,
            DateTimeOffset? invitedAt,
            SystemLanguage? systemLanguage,
            bool hasApplied)
        {
            // Arrange 
            var archivedCandidate = new JobArchivedCandidate(
                jobId,
                candidateId,
                firstName,
                lastName,
                email,
                phoneNumber,
                pictureUri,
                position.Id,
                position.Code,
                null,
                null,
                systemLanguage,
                stage,
                null,
                invitedAt,
                hasApplied);

            // Act
            archivedCandidate.SyncJobPosition(newPositionAliasId, newPositionAliasCode);

            // Arrange 
            Assert.Equal(position.Id, archivedCandidate.Position?.Id);
            Assert.Equal(position.Code, archivedCandidate.Position?.Code);
            Assert.Equal(newPositionAliasId, archivedCandidate?.Position?.AliasTo?.Id);
            Assert.Equal(newPositionAliasCode, archivedCandidate?.Position?.AliasTo?.Code);
        }
    }
}
