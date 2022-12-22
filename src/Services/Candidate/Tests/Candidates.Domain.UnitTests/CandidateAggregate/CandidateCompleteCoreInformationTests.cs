using Candidates.Domain.Aggregates.CandidateAggregate;
using Candidates.Domain.Aggregates.CandidateAggregate.Entities;
using Domain.Seedwork.Enums;
using Domain.Seedwork.Exceptions;
using System;
using System.Collections.Generic;
using Tests.Shared;
using Xunit;

namespace Candidates.Domain.UnitTests.CandidateAggregate
{
    public class CandidateCompleteCoreInformationTests
    {
        [Theory]
        [InlineAutoMoqData("valid@email.com")]
        public void CompleteCoreInformation_ShouldChangeStatusToPending(
            string email,
            Guid externalId,
            SystemLanguage systemLanguage,
            bool acceptTermsAndConditions,
            bool acceptMarketingAcknowledgement,
            string firstName,
            string lastName,
            ICollection<WorkType> workTypes
            )
        {
            // Arrange
            var candidate = new Candidate();
            var activityStatuses = new List<ActivityStatus>() { ActivityStatus.Student };
            var expectedCandidateStatus = CandidateStatus.Pending;

            candidate.RegisterMyself(
                email,
                externalId,
                systemLanguage,
                acceptTermsAndConditions,
                acceptMarketingAcknowledgement);
            candidate.VerifyEmail(candidate.Email!.VerificationKey!.Value, 100);
            candidate.Update(
                email,
                firstName,
                lastName,
                null,
                null,
                null,
                null,
                null,
                false,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                false,
                null,
                null,
                false,
                null,
                false,
                activityStatuses,
                new List<CandidateIndustry>(),
                new List<CandidateSkill>(),
                new List<CandidateDesiredSkill>(),
                new List<CandidateLanguage>(),
                new List<CandidateHobby>(),
                new List<FormatType>(),
                workTypes,
                new List<Guid>());

            // Act
            candidate.CompleteCoreInformation();

            // Assert
            Assert.Equal(expectedCandidateStatus, candidate.Status);
        }

        [Theory]
        [InlineAutoMoqData("", "LastName", "valid@email.com")]
        [InlineAutoMoqData(null, "LastName", "valid@email.com")]
        [InlineAutoMoqData("FirstName", "", "valid@email.com")]
        [InlineAutoMoqData("FirstName", null, "valid@email.com")]
        [InlineAutoMoqData(" ", "  ", "valid@email.com")]
        [InlineAutoMoqData(null, null, "valid@email.com")]
        public void CompleteCoreInformation_ShouldThrowException_WhenIncorrectName(
            string? firstName,
            string? lastName,
            string email,
            Guid externalId,
            SystemLanguage systemLanguage,
            bool acceptTermsAndConditions,
            bool acceptMarketingAcknowledgement,
            ICollection<WorkType> workTypes
            )
        {
            // Arrange
            var candidate = new Candidate();
            var activityStatuses = new List<ActivityStatus>() { ActivityStatus.Freelancer };

            candidate.RegisterMyself(
                email,
                externalId,
                systemLanguage,
                acceptTermsAndConditions,
                acceptMarketingAcknowledgement);
            candidate.VerifyEmail(candidate.Email!.VerificationKey!.Value, 100);
            candidate.Update(
                email,
                firstName,
                lastName,
                null,
                null,
                null,
                null,
                null,
                false,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                false,
                null,
                null,
                false,
                null,
                false,
                activityStatuses,
                new List<CandidateIndustry>(),
                new List<CandidateSkill>(),
                new List<CandidateDesiredSkill>(),
                new List<CandidateLanguage>(),
                new List<CandidateHobby>(),
                new List<FormatType>(),
                workTypes,
                new List<Guid>());

            // Act
            var action = () => candidate.CompleteCoreInformation();

            // Assert
            Assert.Throws<DomainException>(action);
        }

        [Theory]
        [InlineAutoMoqData("valid@email.com")]
        public void CompleteCoreInformation_ShouldThrowException_WhenNoWorkType(
            string email,
            string firstName,
            string lastName,
            Guid externalId,
            SystemLanguage systemLanguage,
            bool acceptTermsAndConditions,
            bool acceptMarketingAcknowledgement
            )
        {
            // Arrange
            var candidate = new Candidate();
            var activityStatuses = new List<ActivityStatus>() { ActivityStatus.Student };
            var workTypes = new List<WorkType>();

            candidate.RegisterMyself(
                email,
                externalId,
                systemLanguage,
                acceptTermsAndConditions,
                acceptMarketingAcknowledgement);
            candidate.VerifyEmail(candidate.Email!.VerificationKey!.Value, 100);
            candidate.Update(
                email,
                firstName,
                lastName,
                null,
                null,
                null,
                null,
                null,
                false,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                false,
                null,
                null,
                false,
                null,
                false,
                activityStatuses,
                new List<CandidateIndustry>(),
                new List<CandidateSkill>(),
                new List<CandidateDesiredSkill>(),
                new List<CandidateLanguage>(),
                new List<CandidateHobby>(),
                new List<FormatType>(),
                workTypes,
                new List<Guid>());

            // Act
            var action = () => candidate.CompleteCoreInformation();

            // Assert
            Assert.Throws<DomainException>(action);
        }

        [Theory]
        [InlineAutoMoqData("valid@email.com")]
        public void CompleteCoreInformation_ShouldThrowException_WhenFreelancerOrPermanentAndNoWorkExperience(
            string email,
            string firstName,
            string lastName,
            Guid externalId,
            SystemLanguage systemLanguage,
            bool acceptTermsAndConditions,
            bool acceptMarketingAcknowledgement,
            ICollection<WorkType> workTypes
            )
        {
            // Arrange
            var candidate = new Candidate();
            var activityStatuses = new List<ActivityStatus>() { ActivityStatus.Freelancer };

            candidate.RegisterMyself(
                email,
                externalId,
                systemLanguage,
                acceptTermsAndConditions,
                acceptMarketingAcknowledgement);
            candidate.VerifyEmail(candidate.Email!.VerificationKey!.Value, 100);
            candidate.Update(
                email,
                firstName,
                lastName,
                null,
                null,
                null,
                null,
                null,
                false,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                false,
                null,
                null,
                false,
                null,
                false,
                activityStatuses,
                new List<CandidateIndustry>(),
                new List<CandidateSkill>(),
                new List<CandidateDesiredSkill>(),
                new List<CandidateLanguage>(),
                new List<CandidateHobby>(),
                new List<FormatType>(),
                workTypes,
                new List<Guid>());

            // Act
            var action = () => candidate.CompleteCoreInformation();

            // Assert
            Assert.Throws<DomainException>(action);
        }

        [Theory]
        [InlineAutoMoqData("valid@email.com")]
        public void CompleteCoreInformation_ShouldThrowException_WhenNoActivityStatuses(
            string email,
            string firstName,
            string lastName,
            Guid externalId,
            SystemLanguage systemLanguage,
            bool acceptTermsAndConditions,
            bool acceptMarketingAcknowledgement,
            ICollection<WorkType> workTypes
            )
        {
            // Arrange
            var candidate = new Candidate();
            var activityStatuses = new List<ActivityStatus>();

            candidate.RegisterMyself(
                email,
                externalId,
                systemLanguage,
                acceptTermsAndConditions,
                acceptMarketingAcknowledgement);
            candidate.VerifyEmail(candidate.Email!.VerificationKey!.Value, 100);
            candidate.Update(
                email,
                firstName,
                lastName,
                null,
                null,
                null,
                null,
                null,
                false,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                false,
                null,
                null,
                false,
                null,
                false,
                activityStatuses,
                new List<CandidateIndustry>(),
                new List<CandidateSkill>(),
                new List<CandidateDesiredSkill>(),
                new List<CandidateLanguage>(),
                new List<CandidateHobby>(),
                new List<FormatType>(),
                workTypes,
                new List<Guid>());

            // Act
            var action = () => candidate.CompleteCoreInformation();

            // Assert
            Assert.Throws<DomainException>(action);
        }
    }
}
