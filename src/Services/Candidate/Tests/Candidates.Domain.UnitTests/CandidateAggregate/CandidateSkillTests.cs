using Candidates.Domain.Aggregates.CandidateAggregate.Entities;
using System;
using Tests.Shared;
using Xunit;

namespace Candidates.Domain.UnitTests.CandidateAggregate
{
    public class CandidateSkillTests
    {
        [Theory, AutoMoqData]
        public void Constructor_ShouldInitCandidateSkill(
            Guid candidateId,
            Guid skillId,
            string skillCode,
            Guid skillAliasToId,
            string skillAliasToCode)
        {
            // Act
            var candidateSkill = new CandidateSkill(skillId, candidateId, skillCode, skillAliasToId, skillAliasToCode);

            // Assert
            Assert.NotEqual(Guid.Empty, candidateSkill.Id);
            Assert.Equal(candidateId, candidateSkill.CandidateId);
            Assert.Equal(skillId, candidateSkill.SkillId);
            Assert.Equal(skillCode, candidateSkill.Code);
            Assert.Equal(skillAliasToId, candidateSkill.AliasTo?.Id);
            Assert.Equal(skillAliasToCode, candidateSkill.AliasTo?.Code);
        }

        [Theory, AutoMoqData]
        public void Sync_ShouldSyncAliasTo(
            Guid candidateId,
            Guid skillId,
            string skillCode,
            Guid skillAliasToId,
            string skillAliasToCode)
        {
            // Assert
            var candidateSkill = new CandidateSkill(skillId, candidateId, skillCode, null, null);

            // Act
            candidateSkill.Sync(skillAliasToId, skillAliasToCode);

            // Assert
            Assert.NotEqual(Guid.Empty, candidateSkill.Id);
            Assert.Equal(candidateId, candidateSkill.CandidateId);
            Assert.Equal(skillId, candidateSkill.SkillId);
            Assert.Equal(skillCode, candidateSkill.Code);
            Assert.Equal(skillAliasToId, candidateSkill.AliasTo?.Id);
            Assert.Equal(skillAliasToCode, candidateSkill.AliasTo?.Code);
        }
    }
}