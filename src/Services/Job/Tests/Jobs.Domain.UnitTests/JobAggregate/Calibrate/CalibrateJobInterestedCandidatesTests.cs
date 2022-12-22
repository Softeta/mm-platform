using Jobs.Domain.Aggregates.JobAggregate.Entities;
using Jobs.Domain.Aggregates.JobAggregate.Services.Calibrate;
using System;
using System.Collections.Generic;
using System.Linq;
using Tests.Shared;
using Xunit;

namespace Jobs.Domain.UnitTests.JobAggregate.Calibrate
{
    public class CalibrateJobInterestedCandidatesTests
    {
        [Theory, AutoMoqData]
        public void CalibrateJobInterestedCandidates_ShouldNotChange_WhenRequestedCandidatesIdsSameAsCurrent(
           Guid jobId,
           JobInterestedCandidate interestedCandidate1,
           JobInterestedCandidate interestedCandidate2)
        {
            // Arrange
            var expected = new List<JobInterestedCandidate>() {
                new JobInterestedCandidate(
                    jobId, 
                    interestedCandidate1.CandidateId,
                    interestedCandidate1.FirstName,
                    interestedCandidate1.LastName, 
                    interestedCandidate1.Position,
                    interestedCandidate1.PictureUri),
                new JobInterestedCandidate(
                    jobId,
                    interestedCandidate2.CandidateId,
                    interestedCandidate2.FirstName,
                    interestedCandidate2.LastName,
                    interestedCandidate2.Position,
                    interestedCandidate1.PictureUri)
            };

            var expectedJobInterestedCandidatesIds = expected.Select(x => x.CandidateId).ToHashSet();

            var current = new List<JobInterestedCandidate>() {
                new JobInterestedCandidate(
                    jobId,
                    interestedCandidate1.CandidateId,
                    interestedCandidate1.FirstName,
                    interestedCandidate1.LastName,
                    interestedCandidate1.Position,
                    interestedCandidate1.PictureUri),
                new JobInterestedCandidate(
                    jobId,
                    interestedCandidate2.CandidateId,
                    interestedCandidate2.FirstName,
                    interestedCandidate2.LastName,
                    interestedCandidate2.Position,
                    interestedCandidate1.PictureUri)
            };

            var request = new List<JobInterestedCandidate>() {
                new JobInterestedCandidate(
                    jobId,
                    interestedCandidate1.CandidateId,
                    interestedCandidate1.FirstName,
                    interestedCandidate1.LastName,
                    interestedCandidate1.Position,
                    interestedCandidate1.PictureUri),
                new JobInterestedCandidate(
                    jobId,
                    interestedCandidate2.CandidateId,
                    interestedCandidate2.FirstName,
                    interestedCandidate2.LastName,
                    interestedCandidate2.Position,
                    interestedCandidate1.PictureUri)
            };

            // Act
            current.Calibrate(request, jobId);

            // Assert
            Assert.Equal(expectedJobInterestedCandidatesIds, current.Select(x => x.CandidateId));
        }

        [Theory, AutoMoqData]
        public void CalibrateJobInterestedCandidates_ShouldChange_WhenPartOfJobCandidatesIdsDifferent(
            Guid jobId,
           JobInterestedCandidate interestedCandidate1,
           JobInterestedCandidate interestedCandidate2,
           JobInterestedCandidate interestedCandidate3)
        {
            // Arrange
            var expected = new List<JobInterestedCandidate>() {
                new JobInterestedCandidate(
                    jobId,
                    interestedCandidate2.CandidateId,
                    interestedCandidate2.FirstName,
                    interestedCandidate2.LastName,
                    interestedCandidate2.Position,
                    interestedCandidate1.PictureUri),
                new JobInterestedCandidate(
                    jobId,
                    interestedCandidate3.CandidateId,
                    interestedCandidate3.FirstName,
                    interestedCandidate3.LastName,
                    interestedCandidate3.Position,
                    interestedCandidate1.PictureUri)
            };

            var expectedJobInterestedCandidatesIds = expected.Select(x => x.CandidateId).ToHashSet();

            var current = new List<JobInterestedCandidate>() {
                new JobInterestedCandidate(
                    jobId,
                    interestedCandidate1.CandidateId,
                    interestedCandidate1.FirstName,
                    interestedCandidate1.LastName,
                    interestedCandidate1.Position,
                    interestedCandidate1.PictureUri),
                new JobInterestedCandidate(
                    jobId,
                    interestedCandidate2.CandidateId,
                    interestedCandidate2.FirstName,
                    interestedCandidate2.LastName,
                    interestedCandidate2.Position,
                    interestedCandidate1.PictureUri)
            };

            var request = new List<JobInterestedCandidate>() {
                new JobInterestedCandidate(
                    jobId,
                    interestedCandidate2.CandidateId,
                    interestedCandidate2.FirstName,
                    interestedCandidate2.LastName,
                    interestedCandidate2.Position,
                    interestedCandidate1.PictureUri),
                new JobInterestedCandidate(
                    jobId,
                    interestedCandidate3.CandidateId,
                    interestedCandidate3.FirstName,
                    interestedCandidate3.LastName,
                    interestedCandidate3.Position,
                    interestedCandidate1.PictureUri)
            };

            // Act
            current.Calibrate(request, jobId);

            // Assert
            Assert.Equal(expectedJobInterestedCandidatesIds, current.Select(x => x.CandidateId));
        }

        [Theory, AutoMoqData]
        public void CalibrateJobInterestedCandidates_ShouldAdd_WhenCurrentCandidatesIdsEmpty(
            Guid jobId,
           JobInterestedCandidate interestedCandidate1,
           JobInterestedCandidate interestedCandidate2)
        {
            // Arrange
            var expected = new List<JobInterestedCandidate>() {
                new JobInterestedCandidate(
                    jobId,
                    interestedCandidate1.CandidateId,
                    interestedCandidate1.FirstName,
                    interestedCandidate1.LastName,
                    interestedCandidate1.Position,
                    interestedCandidate1.PictureUri),
                new JobInterestedCandidate(
                    jobId,
                    interestedCandidate2.CandidateId,
                    interestedCandidate2.FirstName,
                    interestedCandidate2.LastName,
                    interestedCandidate2.Position,
                    interestedCandidate1.PictureUri)
            };

            var expectedJobInterestedCandidatesIds = expected.Select(x => x.CandidateId).ToHashSet();

            var current = new List<JobInterestedCandidate>();

            var request = new List<JobInterestedCandidate>() {
                new JobInterestedCandidate(
                    jobId,
                    interestedCandidate1.CandidateId,
                    interestedCandidate1.FirstName,
                    interestedCandidate1.LastName,
                    interestedCandidate1.Position,
                    interestedCandidate1.PictureUri),
                new JobInterestedCandidate(
                    jobId,
                    interestedCandidate2.CandidateId,
                    interestedCandidate2.FirstName,
                    interestedCandidate2.LastName,
                    interestedCandidate2.Position,
                    interestedCandidate1.PictureUri)
            };

            // Act
            current.Calibrate(request, jobId);

            // Assert
            Assert.Equal(expectedJobInterestedCandidatesIds, current.Select(x => x.CandidateId));
        }
    }
}
