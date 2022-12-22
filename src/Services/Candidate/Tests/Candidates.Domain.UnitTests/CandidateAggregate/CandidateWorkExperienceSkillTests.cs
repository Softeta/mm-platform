using Candidates.Domain.Aggregates.CandidateAggregate.Entities;
using System;
using Tests.Shared;
using Xunit;

namespace Candidates.Domain.UnitTests.CandidateAggregate
{
    public class CandidateWorkExperienceSkillTests
    {
        [Theory, AutoMoqData]
        public void Constructor_ShouldInitCandidateWorkExperienceSkill(
           Guid candidateWorkExperienceId,
           Guid workExperienceSkillId,
           string workExperienceSkillCode,
           Guid workExperienceSkillAliasToId,
           string workExperienceSkillAliasToCode)
        {
            // Act
            var candidateWorkExperienceSkill = new CandidateWorkExperienceSkill(
                workExperienceSkillId, 
                candidateWorkExperienceId,
                workExperienceSkillCode,
                workExperienceSkillAliasToId,
                workExperienceSkillAliasToCode);

            // Assert
            Assert.NotEqual(Guid.Empty, candidateWorkExperienceSkill.Id);
            Assert.Equal(candidateWorkExperienceId, candidateWorkExperienceSkill.CandidateWorkExperienceId);
            Assert.Equal(workExperienceSkillId, candidateWorkExperienceSkill.SkillId);
            Assert.Equal(workExperienceSkillCode, candidateWorkExperienceSkill.Code);
            Assert.Equal(workExperienceSkillAliasToId, candidateWorkExperienceSkill.AliasTo?.Id);
            Assert.Equal(workExperienceSkillAliasToCode, candidateWorkExperienceSkill.AliasTo?.Code);
        }

        [Theory, AutoMoqData]
        public void Sync_ShouldSyncAliasTo(
           Guid candidateWorkExperienceId,
           Guid workExperienceSkillId,
           string workExperienceSkillCode,
           Guid workExperienceSkillAliasToId,
           string workExperienceSkillAliasToCode)
        {
            // Assert
            var candidateWorkExperienceSkill = new CandidateWorkExperienceSkill(
                workExperienceSkillId,
                candidateWorkExperienceId,
                workExperienceSkillCode,
                null,
                null);

            // Act
            candidateWorkExperienceSkill.Sync(workExperienceSkillAliasToId, workExperienceSkillAliasToCode);

            // Assert
            Assert.NotEqual(Guid.Empty, candidateWorkExperienceSkill.Id);
            Assert.Equal(candidateWorkExperienceId, candidateWorkExperienceSkill.CandidateWorkExperienceId);
            Assert.Equal(workExperienceSkillId, candidateWorkExperienceSkill.SkillId);
            Assert.Equal(workExperienceSkillCode, candidateWorkExperienceSkill.Code);
            Assert.Equal(workExperienceSkillAliasToId, candidateWorkExperienceSkill.AliasTo?.Id);
            Assert.Equal(workExperienceSkillAliasToCode, candidateWorkExperienceSkill.AliasTo?.Code);
        }
    }
}
