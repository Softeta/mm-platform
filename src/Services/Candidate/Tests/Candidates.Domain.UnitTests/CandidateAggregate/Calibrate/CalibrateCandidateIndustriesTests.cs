using Candidates.Domain.Aggregates.CandidateAggregate.Entities;
using Candidates.Domain.Aggregates.CandidateAggregate.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Tests.Shared;
using Xunit;

namespace Candidates.Domain.UnitTests.CandidateAggregate.Calibrate
{
    public class CalibrateCandidateIndustriesTests
    {
        [Theory, AutoMoqData]
        public void CalibrateCandidateIndustries_ShouldNotChange_WhenRequestedIndustryIdsSameAsCurrent(
           Guid candidateId,
           CandidateIndustry industry1,
           CandidateIndustry industry2)
        {
            // Arrange
            var expected = new List<CandidateIndustry>() {
                new CandidateIndustry(industry1.IndustryId, candidateId, industry1.Code),
                new CandidateIndustry(industry2.IndustryId, candidateId, industry2.Code)
            };

            var expectedCandidateIndustryIds = expected.Select(x => x.IndustryId).ToHashSet();

            var current = new List<CandidateIndustry>() {
                new CandidateIndustry(industry1.IndustryId, candidateId, industry1.Code),
                new CandidateIndustry(industry2.IndustryId, candidateId, industry2.Code)
            };

            var request = new List<CandidateIndustry>() {
                new CandidateIndustry(industry1.IndustryId, candidateId, industry1.Code),
                new CandidateIndustry(industry2.IndustryId, candidateId, industry2.Code)
            };

            // Act
            current.Calibrate(request, candidateId);

            // Assert
            Assert.Equal(expectedCandidateIndustryIds, current.Select(x => x.IndustryId));
        }

        [Theory, AutoMoqData]
        public void CalibrateCandidateIndustries_ShouldChange_WhenPartOfIndustryIdsDifferent(
            Guid candidateId,
            CandidateIndustry industry1,
            CandidateIndustry industry2,
            CandidateIndustry industry3)
        {
            // Arrange
            var expected = new List<CandidateIndustry>() {
                new CandidateIndustry(industry2.IndustryId, candidateId, industry2.Code),
                new CandidateIndustry(industry3.IndustryId, candidateId, industry3.Code)
            };

            var expectedCandidateIndustryIds = expected.Select(x => x.IndustryId).ToHashSet();

            var current = new List<CandidateIndustry>() {
                new CandidateIndustry(industry1.IndustryId, candidateId, industry1.Code),
                new CandidateIndustry(industry2.IndustryId, candidateId, industry2.Code)
            };

            var request = new List<CandidateIndustry>() {
                new CandidateIndustry(industry2.IndustryId, candidateId, industry2.Code),
                new CandidateIndustry(industry3.IndustryId, candidateId, industry3.Code)
            };

            // Act
            current.Calibrate(request, candidateId);

            // Assert
            Assert.Equal(expectedCandidateIndustryIds, current.Select(x => x.IndustryId));
        }

        [Theory, AutoMoqData]
        public void CalibrateCandidateIndustries_ShouldAdd_WhenCurrentIndustryIdsEmpty(
            Guid candidateId,
            CandidateIndustry industry1,
            CandidateIndustry industry2)
        {
            // Arrange
            var expected = new List<CandidateIndustry>() {
                new CandidateIndustry(industry1.IndustryId, candidateId, industry1.Code),
                new CandidateIndustry(industry2.IndustryId, candidateId, industry2.Code)
            };

            var expectedCandidateIndustryIds = expected.Select(x => x.IndustryId).ToHashSet();

            var current = new List<CandidateIndustry>();

            var request = new List<CandidateIndustry>() {
                new CandidateIndustry(industry1.IndustryId, candidateId, industry1.Code),
                new CandidateIndustry(industry2.IndustryId, candidateId, industry2.Code)
            };

            // Act
            current.Calibrate(request, candidateId);

            // Assert
            Assert.Equal(expectedCandidateIndustryIds, current.Select(x => x.IndustryId));
        }
    }
}
