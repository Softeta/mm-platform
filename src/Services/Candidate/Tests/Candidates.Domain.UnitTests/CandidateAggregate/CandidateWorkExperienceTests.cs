using Candidates.Domain.Aggregates.CandidateAggregate.Entities;
using Domain.Seedwork.Enums;
using Domain.Seedwork.Exceptions;
using Domain.Seedwork.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using Tests.Shared;
using Xunit;

namespace Candidates.Domain.UnitTests.CandidateAggregate
{
    public class CandidateWorkExperienceTests
    {
        [Theory, AutoMoqData]
        public void Constructor_ShouldInitCandidateWorkExperience(
            Guid candidateId,
            string companyName,
            Position position,
            string jobDescription,
            bool isCurrentJob,
            WorkExperienceType type,
            IEnumerable<CandidateWorkExperienceSkill> skills)
        {
            // Arrange
            var date = DateTimeOffset.Parse("2022-04-27T07:03:09.506Z");

            // Act
            var workExperience = new CandidateWorkExperience();

            workExperience.Create(
                candidateId,
                type,
                companyName,
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                date,
                date,
                jobDescription,
                isCurrentJob,
                skills);


            // Assert
            AssertWorkExperience(type, companyName, position, date, jobDescription, workExperience);
        }

        [Theory]
        [InlineAutoMoqData("")]
        [InlineAutoMoqData(" ")]
        public void Initialize_ShouldThrowException_WheCompanyNameEmpty(
            string companyName,
            Position position,
            DateTimeOffset from,
            DateTimeOffset? to,
            string? jobDescription,
            bool isCurrentJob,
            WorkExperienceType type,
            IEnumerable<CandidateWorkExperienceSkill> skills)
        {
            // Arrange
            var workExperience = new CandidateWorkExperience();

            // Act
            Action action = () => workExperience.Create(
                Guid.NewGuid(),
                type,
                companyName,
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                from,
                to,
                jobDescription,
                isCurrentJob,
                skills);

            // Assert
            Assert.Throws<DomainException>(action);
        }

        [Theory]
        [InlineAutoMoqData("")]
        [InlineAutoMoqData(" ")]
        public void Initialize_ShouldThrowException_WhePositionEmpty(
            string positionCode,
            Position position,
            string companyName,
            DateTimeOffset from,
            DateTimeOffset? to,
            string? jobDescription,
            bool isCurrentJob,
            WorkExperienceType type,
            IEnumerable<CandidateWorkExperienceSkill> skills)
        {
            // Arrange
            var workExperience = new CandidateWorkExperience();

            // Act
            Action action = () => workExperience.Create(
                Guid.NewGuid(),
                type,
                companyName,
                position.Id,
                positionCode,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                from,
                to,
                jobDescription,
                isCurrentJob,
                skills);

            // Assert
            Assert.Throws<DomainException>(action);
        }

        [Theory, AutoMoqData]
        public void Initialize_ShouldThrowException_WhenFromIsDefaultValue(
            Position position,
            string companyName,
            string? jobDescription,
            bool isCurrentJob,
            WorkExperienceType type,
            IEnumerable<CandidateWorkExperienceSkill> skills)
        {
            // Arrange
            var workExperience = new CandidateWorkExperience();

            // Act
            Action action = () => workExperience.Create(
                Guid.NewGuid(),
                type,
                companyName,
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                DateTimeOffset.MinValue,
                null,
                jobDescription,
                isCurrentJob,
                skills);

            // Assert
            Assert.Throws<DomainException>(action);
        }

        [Theory, AutoMoqData]
        public void Initialize_ShouldThrowException_WhenFromIsMaxValue(
            Position position,
            string companyName,
            string? jobDescription,
            bool isCurrentJob,
            WorkExperienceType type,
            IEnumerable<CandidateWorkExperienceSkill> skills)
        {
            // Arrange
            var workExperience = new CandidateWorkExperience();

            // Act
            Action action = () => workExperience.Create(
                Guid.NewGuid(),
                type,
                companyName,
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                DateTimeOffset.MaxValue,
                null,
                jobDescription,
                isCurrentJob,
                skills);

            // Assert
            Assert.Throws<DomainException>(action);
        }

        [Theory, AutoMoqData]
        public void Update_ShouldUpdateCandidateWorkExperience(
            CandidateWorkExperience workExperience,
            string companyName,
            Position position,
            string jobDescription,
            bool isCurrentJob,
            WorkExperienceType type,
            IEnumerable<CandidateWorkExperienceSkill> skills)
        {
            // Arrange
            var date = DateTimeOffset.Parse("2022-04-27T07:03:09.506Z");

            // Act
            workExperience.Update(
                type,
                companyName,
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                date,
                date,
                jobDescription,
                isCurrentJob,
                skills);

            // Assert
            AssertWorkExperience(type, companyName, position, date, jobDescription, workExperience);
        }

        [Theory, AutoMoqData]
        public void SyncJobPosition_ShouldSyncJobPosition(
            Guid positionAliasId,
            string positionAliasCode,
            Guid candidateId,
            WorkExperienceType type,
            string companyName,
            Guid positionId,
            string positionCode,
            DateTimeOffset from,
            DateTimeOffset? to,
            string? jobDescription,
            bool isCurrentJob,
            IEnumerable<CandidateWorkExperienceSkill> skills)
        {
            // Arrange 
            var workExperience = new CandidateWorkExperience();
            workExperience.Create(
                candidateId,
                type,
                companyName,
                positionId, 
                positionCode,
                null,
                null,
                from ,
                to, 
                jobDescription, 
                isCurrentJob,
                skills);

            // Act
            workExperience.SyncJobPosition(positionAliasId, positionAliasCode);

            // Assert
            Assert.Equal(positionId, workExperience.Position.Id);
            Assert.Equal(positionCode, workExperience.Position.Code);
            Assert.Equal(positionAliasId, workExperience.Position.AliasTo?.Id);
            Assert.Equal(positionAliasCode, workExperience.Position.AliasTo?.Code);
        }

        private void AssertWorkExperience(
            WorkExperienceType type,
            string companyName,
            Position position,
            DateTimeOffset date,
            string jobDescription,
            CandidateWorkExperience workExperience)
        {
            Assert.Equal(type, workExperience.Type);
            Assert.Equal(companyName, workExperience.CompanyName);
            Assert.Equal(position.Id, workExperience.Position.Id);
            Assert.Equal(position.Code, workExperience.Position.Code);
            Assert.Equal(position.AliasTo?.Id, workExperience.Position?.AliasTo?.Id);
            Assert.Equal(position.AliasTo?.Code, workExperience.Position?.AliasTo?.Code);
            Assert.Equal(date, workExperience.Period.From);
            Assert.Equal(date, workExperience.Period.To);
            Assert.Equal(jobDescription, workExperience.JobDescription);
        }
    }
}
