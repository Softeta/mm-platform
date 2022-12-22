using Candidates.Domain.Aggregates.CandidateAggregate.Entities;
using Candidates.Domain.Aggregates.CandidateAggregate.Services;
using Domain.Seedwork.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using Tests.Shared;
using Xunit;

namespace Candidates.Domain.UnitTests.CandidateAggregate.Calibrate
{
    public class CalibrateCandidateActivityStatusesTests
    {
        [Theory, AutoMoqData]
        public void CalibrateCandidateActivityStatuses_ShouldNotChange_WhenRequestedActivityStatusSameAsCurrent(Guid candidateId)
        {
            // Arrange
            var expected = new List<CandidateActivityStatus>() {
                new CandidateActivityStatus(candidateId, ActivityStatus.Freelancer),
                new CandidateActivityStatus(candidateId, ActivityStatus.Student)
            };

            var expectedCandidateActivityStatuses = expected.Select(x => x.ActivityStatus).ToHashSet();

            var current = new List<CandidateActivityStatus>() {
                new CandidateActivityStatus(candidateId, ActivityStatus.Freelancer),
                new CandidateActivityStatus(candidateId, ActivityStatus.Student)
            };

            var request = new List<ActivityStatus>() {
                ActivityStatus.Freelancer,
                ActivityStatus.Student
            };

            // Act
            current.Calibrate(request, candidateId);

            // Assert
            Assert.Equal(expectedCandidateActivityStatuses, current.Select(x => x.ActivityStatus));
        }

        [Theory, AutoMoqData]
        public void CalibrateCandidateActivityStatuses_ShouldChange_WhenDifferent(Guid candidateId)
        {
            // Arrange
            var expected = new List<CandidateActivityStatus>() {
                new CandidateActivityStatus(candidateId, ActivityStatus.Freelancer),
                new CandidateActivityStatus(candidateId, ActivityStatus.Student)
            };

            var expectedCandidateActivityStatuses = expected.Select(x => x.ActivityStatus).ToHashSet();

            var current = new List<CandidateActivityStatus>() {
                new CandidateActivityStatus(candidateId, ActivityStatus.Student),
                new CandidateActivityStatus(candidateId, ActivityStatus.Permanent)
            };

            var request = new List<ActivityStatus>() {
                ActivityStatus.Freelancer,
                ActivityStatus.Student
            };

            // Act
            current.Calibrate(request, candidateId);

            // Assert
            Assert.Equal(expectedCandidateActivityStatuses, current.Select(x => x.ActivityStatus).ToHashSet());
        }
    }
}
