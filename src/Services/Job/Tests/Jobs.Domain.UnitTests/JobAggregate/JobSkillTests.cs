using Jobs.Domain.Aggregates.JobAggregate.Entities;
using System;
using Tests.Shared;
using Xunit;

namespace Jobs.Domain.UnitTests.JobAggregate
{
    public class JobSkillTests
    {
        [Theory, AutoMoqData]
        public void Constructor_ShouldInitJobSkill(
           Guid jobId,
           Guid skillId,
           string skillCode,
           Guid skillAliasToId,
           string skillAliasToCode)
        {
            // Act
            var jobSkill = new JobSkill(skillId, jobId, skillCode, skillAliasToId, skillAliasToCode);

            // Assert
            Assert.NotEqual(Guid.Empty, jobSkill.Id);
            Assert.Equal(jobId, jobSkill.JobId);
            Assert.Equal(skillId, jobSkill.SkillId);
            Assert.Equal(skillCode, jobSkill.Code);
            Assert.Equal(skillAliasToId, jobSkill.AliasTo?.Id);
            Assert.Equal(skillAliasToCode, jobSkill.AliasTo?.Code);
        }

        [Theory, AutoMoqData]
        public void Sync_ShouldSyncAliasTo(
            Guid jobId,
            Guid skillId,
            string skillCode,
            Guid skillAliasToId,
            string skillAliasToCode)
        {
            // Assert
            var jobSkill = new JobSkill(skillId, jobId, skillCode, null, null);

            // Act
            jobSkill.Sync(skillAliasToId, skillAliasToCode);

            // Assert
            Assert.NotEqual(Guid.Empty, jobSkill.Id);
            Assert.Equal(jobId, jobSkill.JobId);
            Assert.Equal(skillId, jobSkill.SkillId);
            Assert.Equal(skillCode, jobSkill.Code);
            Assert.Equal(skillAliasToId, jobSkill.AliasTo?.Id);
            Assert.Equal(skillAliasToCode, jobSkill.AliasTo?.Code);
        }
    }
}
