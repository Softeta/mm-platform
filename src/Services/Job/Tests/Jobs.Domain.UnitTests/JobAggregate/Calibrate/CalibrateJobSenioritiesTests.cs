using Domain.Seedwork.Enums;
using Jobs.Domain.Aggregates.JobAggregate.Entities;
using Jobs.Domain.Aggregates.JobAggregate.Services.Calibrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Shared;
using Xunit;

namespace Jobs.Domain.UnitTests.JobAggregate.Calibrate
{
    public class CalibrateJobSenioritiesTests
    {
        [Theory, AutoMoqData]
        public void CalibrateJobSeniorities_ShouldNotChange_WhenRequestedSenioritiesSameAsCurrent(Guid jobId)
        {
            // Arrange
            var expected = new List<JobSeniority>() {
                new JobSeniority(jobId, SeniorityLevel.Junior),
                new JobSeniority(jobId, SeniorityLevel.Senior)
            };

            var expectedJobSeniorities = expected.Select(x => x.Seniority).ToHashSet();

            var current = new List<JobSeniority>() {
                new JobSeniority(jobId, SeniorityLevel.Junior),
                new JobSeniority(jobId, SeniorityLevel.Senior)
            };

            var request = new List<JobSeniority>() {
                new JobSeniority(jobId, SeniorityLevel.Junior),
                new JobSeniority(jobId, SeniorityLevel.Senior)
            };

            // Act
            current.Calibrate(request, jobId);

            // Assert
            Assert.Equal(expectedJobSeniorities, current.Select(x => x.Seniority));
        }

        [Theory, AutoMoqData]
        public void CalibrateJobSeniorities_ShouldChange_WhenPartOfJobSenioritiesDifferent(Guid jobId)
        {
            // Arrange
            var expected = new List<JobSeniority>() {
                new JobSeniority(jobId, SeniorityLevel.Senior),
                new JobSeniority(jobId, SeniorityLevel.Mid)
            };

            var expectedJobSeniorities = expected.Select(x => x.Seniority).ToHashSet();

            var current = new List<JobSeniority>() {
                new JobSeniority(jobId, SeniorityLevel.Junior),
                new JobSeniority(jobId, SeniorityLevel.Senior)
            };

            var request = new List<JobSeniority>() {
                new JobSeniority(jobId, SeniorityLevel.Mid),
                new JobSeniority(jobId, SeniorityLevel.Senior)
            };

            // Act
            current.Calibrate(request, jobId);

            // Assert
            Assert.Equal(expectedJobSeniorities, current.Select(x => x.Seniority));
        }

        [Theory, AutoMoqData]
        public void CalibrateJobSeniorities_ShouldChange_WhenCurrentSenioritiesEmpty(Guid jobId)
        {
            // Arrange
            var expected = new List<JobSeniority>() {
                new JobSeniority(jobId, SeniorityLevel.Mid),
                new JobSeniority(jobId, SeniorityLevel.Senior)
            };

            var expectedJobSeniorities = expected.Select(x => x.Seniority).ToHashSet();

            var current = new List<JobSeniority>();

            var request = new List<JobSeniority>() {
                new JobSeniority(jobId, SeniorityLevel.Mid),
                new JobSeniority(jobId, SeniorityLevel.Senior)
            };

            // Act
            current.Calibrate(request, jobId);

            // Assert
            Assert.Equal(expectedJobSeniorities, current.Select(x => x.Seniority));
        }
    }
}
