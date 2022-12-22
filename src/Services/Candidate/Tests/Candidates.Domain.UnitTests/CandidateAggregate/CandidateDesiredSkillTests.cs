using Candidates.Domain.Aggregates.CandidateAggregate.Entities;
using System;
using Tests.Shared;
using Xunit;

namespace Candidates.Domain.UnitTests.CandidateAggregate
{
    public class CandidateDesiredSkillTests
    {
        [Theory, AutoMoqData]
        public void Constructor_ShouldInitCandidateDesiredSkill(
            Guid candidateId,
            Guid desiredSkillId,
            string desiredSkillCode,
            Guid desiredSkillAliasToId,
            string desiredSkillAliasToCode)
        {
            // Act
            var candidateDesiredSkill = new CandidateDesiredSkill(
                desiredSkillId,
                candidateId, 
                desiredSkillCode,
                desiredSkillAliasToId, 
                desiredSkillAliasToCode);

            // Assert
            Assert.NotEqual(Guid.Empty, candidateDesiredSkill.Id);
            Assert.Equal(candidateId, candidateDesiredSkill.CandidateId);
            Assert.Equal(desiredSkillId, candidateDesiredSkill.SkillId);
            Assert.Equal(desiredSkillCode, candidateDesiredSkill.Code);
            Assert.Equal(desiredSkillAliasToId, candidateDesiredSkill.AliasTo?.Id);
            Assert.Equal(desiredSkillAliasToCode, candidateDesiredSkill.AliasTo?.Code);
        }

        [Theory, AutoMoqData]
        public void Sync_ShouldSyncAliasTo(
            Guid candidateId,
            Guid desiredSkillId,
            string desiredSkillCode,
            Guid desiredSkillAliasToId,
            string desiredSkillAliasToCode)
        {
            // Assert
            var candidateDesiredSkill = new CandidateSkill(desiredSkillId, candidateId, desiredSkillCode, null, null);

            // Act
            candidateDesiredSkill.Sync(desiredSkillAliasToId, desiredSkillAliasToCode);

            // Assert
            Assert.NotEqual(Guid.Empty, candidateDesiredSkill.Id);
            Assert.Equal(candidateId, candidateDesiredSkill.CandidateId);
            Assert.Equal(desiredSkillId, candidateDesiredSkill.SkillId);
            Assert.Equal(desiredSkillCode, candidateDesiredSkill.Code);
            Assert.Equal(desiredSkillAliasToId, candidateDesiredSkill.AliasTo?.Id);
            Assert.Equal(desiredSkillAliasToCode, candidateDesiredSkill.AliasTo?.Code);
        }
    }
}
