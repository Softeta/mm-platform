using Candidates.Domain.Aggregates.CandidateAggregate.Entities;
using Candidates.Domain.Aggregates.CandidateAggregate.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Tests.Shared;
using Xunit;

namespace Candidates.Domain.UnitTests.CandidateAggregate.Calibrate
{
    public class CalibrateCandidateSkillsTests
    {
        [Theory, AutoMoqData]
        public void CalibrateCandidateSkills_ShouldNotChange_WhenRequestedSkillIdsSameAsCurrent(
           Guid candidateId,
           CandidateSkill skill1,
           CandidateSkill skill2)
        {
            // Arrange
            var expected = new List<CandidateSkill>() {
                new CandidateSkill(skill1.SkillId, candidateId, skill1.Code, skill1.AliasTo?.Id, skill1.AliasTo?.Code),
                new CandidateSkill(skill2.SkillId, candidateId, skill2.Code, skill2.AliasTo?.Id, skill2.AliasTo?.Code)
            };

            var expectedCandidateSkillIds = expected.Select(x => x.SkillId).ToHashSet();

            var current = new List<CandidateSkill>() {
                new CandidateSkill(skill1.SkillId, candidateId, skill1.Code, skill1.AliasTo?.Id, skill1.AliasTo?.Code),
                new CandidateSkill(skill2.SkillId, candidateId, skill2.Code, skill2.AliasTo?.Id, skill2.AliasTo?.Code)
            };

            var request = new List<CandidateSkill>() {
                new CandidateSkill(skill1.SkillId, candidateId, skill1.Code, skill1.AliasTo?.Id, skill1.AliasTo?.Code),
                new CandidateSkill(skill2.SkillId, candidateId, skill2.Code, skill2.AliasTo?.Id, skill2.AliasTo?.Code)
            };

            // Act
            current.Calibrate(request, candidateId);

            // Assert
            Assert.Equal(expectedCandidateSkillIds, current.Select(x => x.SkillId));
        }

        [Theory, AutoMoqData]
        public void CalibrateCandidateSkills_ShouldChange_WhenPartOfSkillIdsDifferent(
            Guid candidateId,
            CandidateSkill skill1,
            CandidateSkill skill2,
            CandidateSkill skill3)
        {
            // Arrange
            var expected = new List<CandidateSkill>() {
                new CandidateSkill(skill2.SkillId, candidateId, skill2.Code, skill2.AliasTo?.Id, skill2.AliasTo?.Code),
                new CandidateSkill(skill3.SkillId, candidateId, skill3.Code, skill3.AliasTo?.Id, skill3.AliasTo?.Code)
            };

            var expectedCandidateSkillIds = expected.Select(x => x.SkillId).ToHashSet();

            var current = new List<CandidateSkill>() {
                new CandidateSkill(skill1.SkillId, candidateId, skill1.Code, skill1.AliasTo?.Id, skill1.AliasTo?.Code),
                new CandidateSkill(skill2.SkillId, candidateId, skill2.Code, skill2.AliasTo?.Id, skill2.AliasTo?.Code)
            };

            var request = new List<CandidateSkill>() {
                new CandidateSkill(skill2.SkillId, candidateId, skill2.Code, skill2.AliasTo?.Id, skill2.AliasTo?.Code),
                new CandidateSkill(skill3.SkillId, candidateId, skill3.Code, skill3.AliasTo?.Id, skill3.AliasTo?.Code)
            };

            // Act
            current.Calibrate(request, candidateId);

            // Assert
            Assert.Equal(expectedCandidateSkillIds, current.Select(x => x.SkillId));
        }

        [Theory, AutoMoqData]
        public void CalibrateCandidateSkills_ShouldAdd_WhenCurrentSkillIdsEmpty(
            Guid candidateId,
            CandidateSkill skill1,
            CandidateSkill skill2)
        {
            // Arrange
            var expected = new List<CandidateSkill>() {
                new CandidateSkill(skill1.SkillId, candidateId, skill1.Code, skill1.AliasTo?.Id, skill1.AliasTo?.Code),
                new CandidateSkill(skill2.SkillId, candidateId, skill2.Code, skill2.AliasTo?.Id, skill2.AliasTo?.Code)
            };

            var expectedCandidateSkillIds = expected.Select(x => x.SkillId).ToHashSet();

            var current = new List<CandidateSkill>();

            var request = new List<CandidateSkill>() {
                new CandidateSkill(skill1.SkillId, candidateId, skill1.Code, skill1.AliasTo?.Id, skill1.AliasTo?.Code),
                new CandidateSkill(skill2.SkillId, candidateId, skill2.Code, skill2.AliasTo?.Id, skill2.AliasTo?.Code)
            };

            // Act
            current.Calibrate(request, candidateId);

            // Assert
            Assert.Equal(expectedCandidateSkillIds, current.Select(x => x.SkillId));
        }
    }
}
