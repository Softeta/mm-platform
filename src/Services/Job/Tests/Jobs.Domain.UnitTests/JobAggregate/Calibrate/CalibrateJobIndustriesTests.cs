using Jobs.Domain.Aggregates.JobAggregate.Entities;
using Jobs.Domain.Aggregates.JobAggregate.Services.Calibrate;
using System;
using System.Collections.Generic;
using System.Linq;
using Tests.Shared;
using Xunit;

namespace Jobs.Domain.UnitTests.JobAggregate.Calibrate
{
    public class CalibrateJobIndustriesTests
    {
        [Theory, AutoMoqData]
        public void CalibrateJobIndustries_ShouldNotChange_WhenRequestedIndustryIdsSameAsCurrent(
            Guid jobId,
            JobIndustry industry1,
            JobIndustry industry2)
        {
            // Arrange
            var expected = new List<JobIndustry>() {
                new JobIndustry(industry1.IndustryId, jobId, industry1.Code),
                new JobIndustry(industry2.IndustryId, jobId, industry2.Code)
            };

            var expectedJobIndustryIds = expected.Select(x => x.IndustryId).ToHashSet();

            var current = new List<JobIndustry>() {
                new JobIndustry(industry1.IndustryId, jobId, industry1.Code),
                new JobIndustry(industry2.IndustryId, jobId, industry2.Code)
            };

            var request = new List<JobIndustry>() {
                new JobIndustry(industry1.IndustryId, jobId, industry1.Code),
                new JobIndustry(industry2.IndustryId, jobId, industry2.Code)
            };

            // Act
            current.Calibrate(request, jobId);

            // Assert
            Assert.Equal(expectedJobIndustryIds, current.Select(x => x.IndustryId));
        }

        [Theory, AutoMoqData]
        public void CalibrateJobIndustries_ShouldChange_WhenPartOfJobIndustryIdsDifferent(
            Guid jobId,
            JobIndustry industry1,
            JobIndustry industry2,
            JobIndustry industry3)
        {
            // Arrange
            var expected = new List<JobIndustry>() {
                new JobIndustry(industry2.IndustryId, jobId, industry2.Code),
                new JobIndustry(industry3.IndustryId, jobId, industry3.Code)
            };

            var expectedJobIndustryIds = expected.Select(x => x.IndustryId).ToHashSet();

            var current = new List<JobIndustry>() {
                new JobIndustry(industry1.IndustryId, jobId, industry1.Code),
                new JobIndustry(industry2.IndustryId, jobId, industry2.Code)
            };

            var request = new List<JobIndustry>() {
                new JobIndustry(industry2.IndustryId, jobId, industry2.Code),
                new JobIndustry(industry3.IndustryId, jobId, industry3.Code)
            };

            // Act
            current.Calibrate(request, jobId);

            // Assert
            Assert.Equal(expectedJobIndustryIds, current.Select(x => x.IndustryId));
        }

        [Theory, AutoMoqData]
        public void CalibrateJobIndustries_ShouldAdd_WhenCurrentIndustryIdsEmpty(
            Guid jobId,
            JobIndustry industry1,
            JobIndustry industry2)
        {
            // Arrange
            var expected = new List<JobIndustry>() {
                new JobIndustry(industry1.IndustryId, jobId, industry1.Code),
                new JobIndustry(industry2.IndustryId, jobId, industry2.Code)
            };

            var expectedJobIndustryIds = expected.Select(x => x.IndustryId).ToHashSet();

            var current = new List<JobIndustry>();

            var request = new List<JobIndustry>() {
                new JobIndustry(industry1.IndustryId, jobId, industry1.Code),
                new JobIndustry(industry2.IndustryId, jobId, industry2.Code)
            };

            // Act
            current.Calibrate(request, jobId);

            // Assert
            Assert.Equal(expectedJobIndustryIds, current.Select(x => x.IndustryId));
        }
    }
}
