using Jobs.Domain.Aggregates.JobAggregate.Entities;
using Jobs.Domain.Aggregates.JobAggregate.Services.Calibrate;
using System;
using System.Collections.Generic;
using System.Linq;
using Tests.Shared;
using Xunit;

namespace Jobs.Domain.UnitTests.JobAggregate.Calibrate
{
    public class CalibrateJobSkillsTests
    {
        [Theory, AutoMoqData]
        public void CalibrateJobSkills_ShouldNotChange_WhenRequestedSkillIdsSameAsCurrent(
            Guid jobId,
            JobSkill skill1,
            JobSkill skill2)
        {
            // Arrange
            var expected = new List<JobSkill>() {
                new JobSkill(skill1.SkillId, jobId, skill1.Code, skill1.AliasTo?.Id, skill1.AliasTo?.Code),
                new JobSkill(skill2.SkillId, jobId, skill2.Code, skill2.AliasTo?.Id, skill2.AliasTo?.Code)
            };

            var expectedJobSkillIds = expected.Select(x => x.SkillId).ToHashSet();

            var current = new List<JobSkill>() {
                new JobSkill(skill1.SkillId, jobId, skill1.Code, skill1.AliasTo?.Id, skill1.AliasTo?.Code),
                new JobSkill(skill2.SkillId, jobId, skill2.Code, skill2.AliasTo?.Id, skill2.AliasTo?.Code)
            };

            var request = new List<JobSkill>() {
                new JobSkill(skill1.SkillId, jobId, skill1.Code, skill1.AliasTo?.Id, skill1.AliasTo?.Code),
                new JobSkill(skill2.SkillId, jobId, skill2.Code, skill2.AliasTo?.Id, skill2.AliasTo?.Code)
            };

            // Act
            current.Calibrate(request, jobId);

            // Assert
            Assert.Equal(expectedJobSkillIds, current.Select(x => x.SkillId));
        }

        [Theory, AutoMoqData]
        public void CalibrateJobSkills_ShouldChange_WhenPartOfJobSkillIdsDifferent(
            Guid jobId,
            JobSkill skill1,
            JobSkill skill2,
            JobSkill skill3)
        {
            // Arrange
            var expected = new List<JobSkill>() {
                new JobSkill(skill2.SkillId, jobId, skill2.Code, skill2.AliasTo?.Id, skill2.AliasTo?.Code),
                new JobSkill(skill3.SkillId, jobId, skill3.Code, skill3.AliasTo?.Id, skill3.AliasTo?.Code)
            };

            var expectedJobSkillIds = expected.Select(x => x.SkillId).ToHashSet();

            var current = new List<JobSkill>() {
                new JobSkill(skill1.SkillId, jobId, skill1.Code, skill1.AliasTo?.Id, skill1.AliasTo?.Code),
                new JobSkill(skill2.SkillId, jobId, skill2.Code, skill2.AliasTo?.Id, skill2.AliasTo?.Code)
            };

            var request = new List<JobSkill>() {
                new JobSkill(skill2.SkillId, jobId, skill2.Code, skill2.AliasTo?.Id, skill2.AliasTo?.Code),
                new JobSkill(skill3.SkillId, jobId, skill3.Code, skill3.AliasTo?.Id, skill3.AliasTo?.Code)
            };

            // Act
            current.Calibrate(request, jobId);

            // Assert
            Assert.Equal(expectedJobSkillIds, current.Select(x => x.SkillId));
        }

        [Theory, AutoMoqData]
        public void CalibrateJobSkills_ShouldAdd_WhenCurrentSkillIdsEmpty(
            Guid jobId,
            JobSkill skill1,
            JobSkill skill2)
        {
            // Arrange
            var expected = new List<JobSkill>() {
                new JobSkill(skill1.SkillId, jobId, skill1.Code, skill1.AliasTo?.Id, skill1.AliasTo?.Code),
                new JobSkill(skill2.SkillId, jobId, skill2.Code, skill2.AliasTo?.Id, skill2.AliasTo?.Code)
            };

            var expectedJobSkillIds = expected.Select(x => x.SkillId).ToHashSet();

            var current = new List<JobSkill>();

            var request = new List<JobSkill>() {
                new JobSkill(skill1.SkillId, jobId, skill1.Code, skill1.AliasTo?.Id, skill1.AliasTo?.Code),
                new JobSkill(skill2.SkillId, jobId, skill2.Code, skill2.AliasTo?.Id, skill2.AliasTo?.Code)
            };

            // Act
            current.Calibrate(request, jobId);

            // Assert
            Assert.Equal(expectedJobSkillIds, current.Select(x => x.SkillId));
        }
    }
}
