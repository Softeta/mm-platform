using Jobs.Domain.Aggregates.JobAggregate.Entities;
using Jobs.Domain.Aggregates.JobAggregate.Services.Calibrate;
using System;
using System.Collections.Generic;
using System.Linq;
using Tests.Shared;
using Xunit;

namespace Jobs.Domain.UnitTests.JobAggregate.Calibrate
{
    public class CalibrateJobInterestedLinkedInsTests
    {
        [Theory, AutoMoqData]
        public void CalibrateJobInterestedLinkedIns_ShouldNotChange_WhenRequestedLinkedInsSameAsCurrent(
           Guid jobId,
           JobInterestedLinkedIn interestedLinkedIn1,
           JobInterestedLinkedIn interestedLinkedIn2)
        {
            // Arrange
            var expected = new List<JobInterestedLinkedIn>() {
                new JobInterestedLinkedIn(jobId, interestedLinkedIn1.Url),
                new JobInterestedLinkedIn(jobId, interestedLinkedIn2.Url)
            };

            var expectedLinkedIns = expected.Select(x => x.Url).ToHashSet();

            var current = new List<JobInterestedLinkedIn>() {
                new JobInterestedLinkedIn(jobId, interestedLinkedIn1.Url),
                new JobInterestedLinkedIn(jobId, interestedLinkedIn2.Url)
            };

            var request = new List<string>() {
                interestedLinkedIn1.Url, interestedLinkedIn2.Url
            };

            // Act
            current.Calibrate(request, jobId);

            // Assert
            Assert.Equal(expectedLinkedIns, current.Select(x => x.Url));
        }

        [Theory, AutoMoqData]
        public void CalibrateJobInterestedLinkedIns_ShouldChange_WhenPartOfJobLinkedInsDifferent(
            Guid jobId,
           JobInterestedLinkedIn interestedLinkedIn1,
           JobInterestedLinkedIn interestedLinkedIn2,
           JobInterestedLinkedIn interestedLinkedIn3)
        {
            // Arrange
            var expected = new List<JobInterestedLinkedIn>() {
                new JobInterestedLinkedIn(jobId, interestedLinkedIn2.Url),
                new JobInterestedLinkedIn(jobId, interestedLinkedIn3.Url)
            };

            var expectedLinkedIns = expected.Select(x => x.Url).ToHashSet();

            var current = new List<JobInterestedLinkedIn>() {
                new JobInterestedLinkedIn(jobId, interestedLinkedIn1.Url),
                new JobInterestedLinkedIn(jobId, interestedLinkedIn2.Url)
            };

            var request = new List<string>() {
                interestedLinkedIn2.Url, interestedLinkedIn3.Url
            };

            // Act
            current.Calibrate(request, jobId);

            // Assert
            Assert.Equal(expectedLinkedIns, current.Select(x => x.Url));
        }

        [Theory, AutoMoqData]
        public void CalibrateJobInterestedLinkedIns_ShouldAdd_WhenCurrentLinkedInsEmpty(
            Guid jobId,
           JobInterestedLinkedIn interestedLinkedIn1,
           JobInterestedLinkedIn interestedLinkedIn2)
        {
            // Arrange
            var expected = new List<JobInterestedLinkedIn>() {
                new JobInterestedLinkedIn(jobId, interestedLinkedIn1.Url),
                new JobInterestedLinkedIn(jobId, interestedLinkedIn2.Url)
            };

            var expectedLinkedIns = expected.Select(x => x.Url).ToHashSet();

            var current = new List<JobInterestedLinkedIn>();

            var request = new List<string>() {
                interestedLinkedIn1.Url, interestedLinkedIn2.Url
            };

            // Act
            current.Calibrate(request, jobId);

            // Assert
            Assert.Equal(expectedLinkedIns, current.Select(x => x.Url));
        }
    }
}
