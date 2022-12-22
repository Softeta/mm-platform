using Candidates.Domain.Aggregates.CandidateAggregate.Entities;
using Candidates.Domain.Aggregates.CandidateAggregate.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Tests.Shared;
using Xunit;

namespace Candidates.Domain.UnitTests.CandidateAggregate.Calibrate
{
    public class CalibrateCandidateWorkExperienceSkillsTests
    {
        [Theory, AutoMoqData]
        public void CalibrateCandidateWorkExperienceSkills_ShouldNotChange_WhenRequestedSkillIdsSameAsCurrent(
           Guid candidateId,
           CandidateWorkExperienceSkill skill1,
           CandidateWorkExperienceSkill skill2)
        {
            // Arrange
            var expected = new List<CandidateWorkExperienceSkill>() {
                new CandidateWorkExperienceSkill(skill1.SkillId, candidateId, skill1.Code, skill1.AliasTo?.Id, skill1.AliasTo?.Code),
                new CandidateWorkExperienceSkill(skill2.SkillId, candidateId, skill2.Code, skill2.AliasTo?.Id, skill2.AliasTo?.Code)
            };

            var expectedCandidateWorkExperienceSkillIds = expected.Select(x => x.SkillId).ToHashSet();

            var current = new List<CandidateWorkExperienceSkill>() {
                new CandidateWorkExperienceSkill(skill1.SkillId, candidateId, skill1.Code, skill1.AliasTo?.Id, skill1.AliasTo?.Code),
                new CandidateWorkExperienceSkill(skill2.SkillId, candidateId, skill2.Code, skill2.AliasTo?.Id, skill2.AliasTo?.Code)
            };

            var request = new List<CandidateWorkExperienceSkill>() {
                new CandidateWorkExperienceSkill(skill1.SkillId, candidateId, skill1.Code, skill1.AliasTo?.Id, skill1.AliasTo?.Code),
                new CandidateWorkExperienceSkill(skill2.SkillId, candidateId, skill2.Code, skill2.AliasTo?.Id, skill2.AliasTo?.Code)
            };

            // Act
            current.Calibrate(request, candidateId);

            // Assert
            Assert.Equal(expectedCandidateWorkExperienceSkillIds, current.Select(x => x.SkillId));
        }

        [Theory, AutoMoqData]
        public void CalibrateCandidateWorkExperienceSkills_ShouldChange_WhenPartOfSkillIdsDifferent(
            Guid candidateId,
            CandidateWorkExperienceSkill skill1,
            CandidateWorkExperienceSkill skill2,
            CandidateWorkExperienceSkill skill3)
        {
            // Arrange
            var expected = new List<CandidateWorkExperienceSkill>() {
                new CandidateWorkExperienceSkill(skill2.SkillId, candidateId, skill2.Code, skill2.AliasTo?.Id, skill2.AliasTo?.Code),
                new CandidateWorkExperienceSkill(skill3.SkillId, candidateId, skill3.Code, skill3.AliasTo?.Id, skill3.AliasTo?.Code)
            };

            var expectedCandidateWorkExperienceSkillIds = expected.Select(x => x.SkillId).ToHashSet();

            var current = new List<CandidateWorkExperienceSkill>() {
                new CandidateWorkExperienceSkill(skill1.SkillId, candidateId, skill1.Code, skill1.AliasTo?.Id, skill1.AliasTo?.Code),
                new CandidateWorkExperienceSkill(skill2.SkillId, candidateId, skill2.Code, skill2.AliasTo?.Id, skill2.AliasTo?.Code)
            };

            var request = new List<CandidateWorkExperienceSkill>() {
                new CandidateWorkExperienceSkill(skill2.SkillId, candidateId, skill2.Code, skill2.AliasTo?.Id, skill2.AliasTo?.Code),
                new CandidateWorkExperienceSkill(skill3.SkillId, candidateId, skill3.Code, skill3.AliasTo?.Id, skill3.AliasTo?.Code)
            };

            // Act
            current.Calibrate(request, candidateId);

            // Assert
            Assert.Equal(expectedCandidateWorkExperienceSkillIds, current.Select(x => x.SkillId));
        }

        [Theory, AutoMoqData]
        public void CalibrateCandidateWorkExperienceSkills_ShouldAdd_WhenCurrentSkillIdsEmpty(
            Guid candidateId,
            CandidateWorkExperienceSkill skill1,
            CandidateWorkExperienceSkill skill2)
        {
            // Arrange
            var expected = new List<CandidateWorkExperienceSkill>() {
                new CandidateWorkExperienceSkill(skill1.SkillId, candidateId, skill1.Code, skill1.AliasTo?.Id, skill1.AliasTo?.Code),
                new CandidateWorkExperienceSkill(skill2.SkillId, candidateId, skill2.Code, skill2.AliasTo?.Id, skill2.AliasTo?.Code)
            };

            var expectedCandidateWorkExperienceSkillIds = expected.Select(x => x.SkillId).ToHashSet();

            var current = new List<CandidateWorkExperienceSkill>();

            var request = new List<CandidateWorkExperienceSkill>() {
                new CandidateWorkExperienceSkill(skill1.SkillId, candidateId, skill1.Code, skill1.AliasTo?.Id, skill1.AliasTo?.Code),
                new CandidateWorkExperienceSkill(skill2.SkillId, candidateId, skill2.Code, skill2.AliasTo?.Id, skill2.AliasTo?.Code)
            };

            // Act
            current.Calibrate(request, candidateId);

            // Assert
            Assert.Equal(expectedCandidateWorkExperienceSkillIds, current.Select(x => x.SkillId));
        }
    }
}
