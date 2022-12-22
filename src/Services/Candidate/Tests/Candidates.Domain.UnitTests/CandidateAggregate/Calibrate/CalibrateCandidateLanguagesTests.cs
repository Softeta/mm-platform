using Candidates.Domain.Aggregates.CandidateAggregate.Entities;
using Candidates.Domain.Aggregates.CandidateAggregate.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Tests.Shared;
using Xunit;

namespace Candidates.Domain.UnitTests.CandidateAggregate.Calibrate
{
    public class CalibrateCandidateLanguagesTests
    {
        [Theory, AutoMoqData]
        public void CalibrateCandidateLanguages_ShouldNotChange_WhenRequestedLanguageIdsSameAsCurrent(
           Guid candidateId,
           CandidateLanguage candidateLanguage1,
           CandidateLanguage candidateLanguage2)
        {
            // Arrange
            var expected = new List<CandidateLanguage>() {
                new CandidateLanguage(candidateId, candidateLanguage1.Language.Id, candidateLanguage1.Language.Code, candidateLanguage1.Language.Name),
                new CandidateLanguage(candidateId, candidateLanguage2.Language.Id, candidateLanguage2.Language.Code, candidateLanguage2.Language.Name)
            };

            var expectedCandidateLanguageIds = expected.Select(x => x.Language.Id).ToHashSet();

            var current = new List<CandidateLanguage>() {
                new CandidateLanguage(candidateId, candidateLanguage1.Language.Id, candidateLanguage1.Language.Code, candidateLanguage1.Language.Name),
                new CandidateLanguage(candidateId, candidateLanguage2.Language.Id, candidateLanguage2.Language.Code, candidateLanguage2.Language.Name)
            };

            var request = new List<CandidateLanguage>() {
                new CandidateLanguage(candidateId, candidateLanguage1.Language.Id, candidateLanguage1.Language.Code, candidateLanguage1.Language.Name),
                new CandidateLanguage(candidateId, candidateLanguage2.Language.Id, candidateLanguage2.Language.Code, candidateLanguage2.Language.Name)
            };

            // Act
            current.Calibrate(request, candidateId);

            // Assert
            Assert.Equal(expectedCandidateLanguageIds, current.Select(x => x.Language.Id));
        }

        [Theory, AutoMoqData]
        public void CalibrateCandidateLanguages_ShouldChange_WhenPartOfLanguageIdsDifferent(
            Guid candidateId,
            CandidateLanguage candidateLanguage1,
            CandidateLanguage candidateLanguage2,
            CandidateLanguage candidateLanguage3)
        {
            // Arrange
            var expected = new List<CandidateLanguage>() {
                new CandidateLanguage(candidateId, candidateLanguage2.Language.Id, candidateLanguage2.Language.Code, candidateLanguage2.Language.Name),
                new CandidateLanguage(candidateId, candidateLanguage3.Language.Id, candidateLanguage3.Language.Code, candidateLanguage3.Language.Name)
            };

            var expectedCandidateLanguageIds = expected.Select(x => x.Language.Id).ToHashSet();

            var current = new List<CandidateLanguage>() {
                new CandidateLanguage(candidateId, candidateLanguage1.Language.Id, candidateLanguage1.Language.Code, candidateLanguage1.Language.Name),
                new CandidateLanguage(candidateId, candidateLanguage2.Language.Id, candidateLanguage2.Language.Code, candidateLanguage2.Language.Name),
            };

            var request = new List<CandidateLanguage>() {
                new CandidateLanguage(candidateId, candidateLanguage2.Language.Id, candidateLanguage2.Language.Code, candidateLanguage2.Language.Name),
                new CandidateLanguage(candidateId, candidateLanguage3.Language.Id, candidateLanguage3.Language.Code, candidateLanguage3.Language.Name)
            };

            // Act
            current.Calibrate(request, candidateId);

            // Assert
            Assert.Equal(expectedCandidateLanguageIds, current.Select(x => x.Language.Id));
        }

        [Theory, AutoMoqData]
        public void CalibrateCandidateLanguages_ShouldAdd_WhenCurrentLanguageIdsEmpty(
            Guid candidateId,
            CandidateLanguage candidateLanguage1,
            CandidateLanguage candidateLanguage2)
        {
            // Arrange
            var expected = new List<CandidateLanguage>() {
                new CandidateLanguage(candidateId, candidateLanguage1.Language.Id, candidateLanguage1.Language.Code, candidateLanguage1.Language.Name),
                new CandidateLanguage(candidateId, candidateLanguage2.Language.Id, candidateLanguage2.Language.Code, candidateLanguage2.Language.Name)
            };

            var expectedCandidateLanguageIds = expected.Select(x => x.Language.Id).ToHashSet();

            var current = new List<CandidateLanguage>();

            var request = new List<CandidateLanguage>() {
                new CandidateLanguage(candidateId, candidateLanguage1.Language.Id, candidateLanguage1.Language.Code, candidateLanguage1.Language.Name),
                new CandidateLanguage(candidateId, candidateLanguage2.Language.Id, candidateLanguage2.Language.Code, candidateLanguage2.Language.Name)
            };

            // Act
            current.Calibrate(request, candidateId);

            // Assert
            Assert.Equal(expectedCandidateLanguageIds, current.Select(x => x.Language.Id));
        }
    }
}
