using Candidates.Domain.Aggregates.CandidateAggregate.Entities;
using Candidates.Domain.Aggregates.CandidateAggregate.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Tests.Shared;
using Xunit;

namespace Candidates.Domain.UnitTests.CandidateAggregate.Calibrate
{
    public class CalibrateCandidateHobbiesTests
    {
        [Theory, AutoMoqData]
        public void CalibrateCandidateHobbies_ShouldNotChange_WhenRequestedHobbyIdsSameAsCurrent(
          Guid candidateId,
          CandidateHobby hobby1,
          CandidateHobby hobby2)
        {
            // Arrange
            var expected = new List<CandidateHobby>() {
                new CandidateHobby(hobby1.HobbyId, candidateId, hobby1.Code),
                new CandidateHobby(hobby2.HobbyId, candidateId, hobby2.Code)
            };

            var expectedCandidateHobbyIds = expected.Select(x => x.HobbyId).ToHashSet();

            var current = new List<CandidateHobby>() {
                new CandidateHobby(hobby1.HobbyId, candidateId, hobby1.Code),
                new CandidateHobby(hobby2.HobbyId, candidateId, hobby2.Code)
            };

            var request = new List<CandidateHobby>() {
                new CandidateHobby(hobby1.HobbyId, candidateId, hobby1.Code),
                new CandidateHobby(hobby2.HobbyId, candidateId, hobby2.Code)
            };

            // Act
            current.Calibrate(request, candidateId);

            // Assert
            Assert.Equal(expectedCandidateHobbyIds, current.Select(x => x.HobbyId));
        }

        [Theory, AutoMoqData]
        public void CalibrateCandidateHobbies_ShouldChange_WhenPartOfHobbyIdsDifferent(
            Guid candidateId,
            CandidateHobby hobby1,
            CandidateHobby hobby2,
            CandidateHobby hobby3)
        {
            // Arrange
            var expected = new List<CandidateHobby>() {
                new CandidateHobby(hobby2.HobbyId, candidateId, hobby2.Code),
                new CandidateHobby(hobby3.HobbyId, candidateId, hobby3.Code)
            };

            var expectedCandidateHobbyIds = expected.Select(x => x.HobbyId).ToHashSet();

            var current = new List<CandidateHobby>() {
                new CandidateHobby(hobby1.HobbyId, candidateId, hobby1.Code),
                new CandidateHobby(hobby2.HobbyId, candidateId, hobby2.Code)
            };

            var request = new List<CandidateHobby>() {
                new CandidateHobby(hobby2.HobbyId, candidateId, hobby2.Code),
                new CandidateHobby(hobby3.HobbyId, candidateId, hobby3.Code)
            };

            // Act
            current.Calibrate(request, candidateId);

            // Assert
            Assert.Equal(expectedCandidateHobbyIds, current.Select(x => x.HobbyId));
        }

        [Theory, AutoMoqData]
        public void CalibrateCandidateHobbies_ShouldAdd_WhenCurrentHobbyIdsEmpty(
            Guid candidateId,
            CandidateHobby hobby1,
            CandidateHobby hobby2)
        {
            // Arrange
            var expected = new List<CandidateHobby>() {
                new CandidateHobby(hobby1.HobbyId, candidateId, hobby1.Code),
                new CandidateHobby(hobby2.HobbyId, candidateId, hobby2.Code)
            };

            var expectedCandidateHobbyIds = expected.Select(x => x.HobbyId).ToHashSet();

            var current = new List<CandidateHobby>();

            var request = new List<CandidateHobby>() {
                new CandidateHobby(hobby1.HobbyId, candidateId, hobby1.Code),
                new CandidateHobby(hobby2.HobbyId, candidateId, hobby2.Code)
            };

            // Act
            current.Calibrate(request, candidateId);

            // Assert
            Assert.Equal(expectedCandidateHobbyIds, current.Select(x => x.HobbyId));
        }
    }
}
