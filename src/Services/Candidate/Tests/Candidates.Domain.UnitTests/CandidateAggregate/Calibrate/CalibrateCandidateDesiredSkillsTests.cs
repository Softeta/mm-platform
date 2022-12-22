using Candidates.Domain.Aggregates.CandidateAggregate.Entities;
using Candidates.Domain.Aggregates.CandidateAggregate.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Tests.Shared;
using Xunit;

namespace Candidates.Domain.UnitTests.CandidateAggregate.Calibrate
{
    public class CalibrateCandidateDesiredSkillsTests
    {
        [Theory, AutoMoqData]
        public void CalibrateCandidateDesiredSkills_ShouldNotChange_WhenRequestedSkillIdsSameAsCurrent(
           Guid candidateId,
           CandidateDesiredSkill skill1,
           CandidateDesiredSkill skill2)
        {
            // Arrange
            var expected = new List<CandidateDesiredSkill>() {
                new CandidateDesiredSkill(skill1.SkillId, candidateId, skill1.Code, skill1.AliasTo?.Id, skill1.AliasTo?.Code),
                new CandidateDesiredSkill(skill2.SkillId, candidateId, skill2.Code, skill2.AliasTo?.Id, skill2.AliasTo?.Code)
            };

            var expectedCandidateDesiredSkillIds = expected.Select(x => x.SkillId).ToHashSet();

            var current = new List<CandidateDesiredSkill>() {
                new CandidateDesiredSkill(skill1.SkillId, candidateId, skill1.Code, skill1.AliasTo?.Id, skill1.AliasTo?.Code),
                new CandidateDesiredSkill(skill2.SkillId, candidateId, skill2.Code, skill2.AliasTo?.Id, skill2.AliasTo?.Code)
            };

            var request = new List<CandidateDesiredSkill>() {
                new CandidateDesiredSkill(skill1.SkillId, candidateId, skill1.Code, skill1.AliasTo?.Id, skill1.AliasTo?.Code),
                new CandidateDesiredSkill(skill2.SkillId, candidateId, skill2.Code, skill2.AliasTo?.Id, skill2.AliasTo?.Code)
            };

            // Act
            current.Calibrate(request, candidateId);

            // Assert
            Assert.Equal(expectedCandidateDesiredSkillIds, current.Select(x => x.SkillId));
        }

        [Theory, AutoMoqData]
        public void CalibrateCandidateDesiredSkills_ShouldChange_WhenPartOfSkillIdsDifferent(
            Guid candidateId,
            CandidateDesiredSkill skill1,
            CandidateDesiredSkill skill2,
            CandidateDesiredSkill skill3)
        {
            // Arrange
            var expected = new List<CandidateDesiredSkill>() {
                new CandidateDesiredSkill(skill2.SkillId, candidateId, skill2.Code, skill2.AliasTo?.Id, skill2.AliasTo?.Code),
                new CandidateDesiredSkill(skill3.SkillId, candidateId, skill3.Code, skill3.AliasTo?.Id, skill3.AliasTo?.Code)
            };

            var expectedCandidateDesiredSkillIds = expected.Select(x => x.SkillId).ToHashSet();

            var current = new List<CandidateDesiredSkill>() {
                new CandidateDesiredSkill(skill1.SkillId, candidateId, skill1.Code, skill1.AliasTo?.Id, skill1.AliasTo?.Code),
                new CandidateDesiredSkill(skill2.SkillId, candidateId, skill2.Code, skill2.AliasTo?.Id, skill2.AliasTo?.Code)
            };

            var request = new List<CandidateDesiredSkill>() {
                new CandidateDesiredSkill(skill2.SkillId, candidateId, skill2.Code, skill2.AliasTo?.Id, skill2.AliasTo?.Code),
                new CandidateDesiredSkill(skill3.SkillId, candidateId, skill3.Code, skill3.AliasTo?.Id, skill3.AliasTo?.Code)
            };

            // Act
            current.Calibrate(request, candidateId);

            // Assert
            Assert.Equal(expectedCandidateDesiredSkillIds, current.Select(x => x.SkillId));
        }

        [Theory, AutoMoqData]
        public void CalibrateCandidateDesiredSkills_ShouldAdd_WhenCurrentSkillIdsEmpty(
            Guid candidateId,
            CandidateDesiredSkill skill1,
            CandidateDesiredSkill skill2)
        {
            // Arrange
            var expected = new List<CandidateDesiredSkill>() {
                new CandidateDesiredSkill(skill1.SkillId, candidateId, skill1.Code, skill1.AliasTo?.Id, skill1.AliasTo?.Code),
                new CandidateDesiredSkill(skill2.SkillId, candidateId, skill2.Code, skill2.AliasTo?.Id, skill2.AliasTo?.Code)
            };

            var expectedCandidateDesiredSkillIds = expected.Select(x => x.SkillId).ToHashSet();

            var current = new List<CandidateDesiredSkill>();

            var request = new List<CandidateDesiredSkill>() {
                new CandidateDesiredSkill(skill1.SkillId, candidateId, skill1.Code, skill1.AliasTo?.Id, skill1.AliasTo?.Code),
                new CandidateDesiredSkill(skill2.SkillId, candidateId, skill2.Code, skill2.AliasTo?.Id, skill2.AliasTo?.Code)
            };

            // Act
            current.Calibrate(request, candidateId);

            // Assert
            Assert.Equal(expectedCandidateDesiredSkillIds, current.Select(x => x.SkillId));
        }
    }
}
