using Jobs.Domain.Aggregates.JobAggregate.Entities;
using Jobs.Domain.Aggregates.JobAggregate.Services.Calibrate;
using System;
using System.Collections.Generic;
using System.Linq;
using Tests.Shared;
using Xunit;

namespace Jobs.Domain.UnitTests.JobAggregate.Calibrate
{
    public class CalibrateJobLanguagesTests
    {
        [Theory, AutoMoqData]
        public void CalibrateJobLanguages_ShouldNotChange_WhenRequestedLanguageIdsSameAsCurrent(
            Guid jobId,
            JobLanguage jobLanguage1,
            JobLanguage jobLanguage2)
        {
            // Arrange
            var expected = new List<JobLanguage>() {
                new JobLanguage(jobId, jobLanguage1.Language.Id, jobLanguage1.Language.Code, jobLanguage1.Language.Name),
                new JobLanguage(jobId, jobLanguage2.Language.Id, jobLanguage2.Language.Code, jobLanguage2.Language.Name)
            };

            var expectedJobLanguageIds = expected.Select(x => x.Language.Id).ToHashSet();

            var current = new List<JobLanguage>() {
                new JobLanguage(jobId, jobLanguage1.Language.Id, jobLanguage1.Language.Code, jobLanguage1.Language.Name),
                new JobLanguage(jobId, jobLanguage2.Language.Id, jobLanguage2.Language.Code, jobLanguage2.Language.Name)
            };

            var request = new List<JobLanguage>() {
                new JobLanguage(jobId, jobLanguage1.Language.Id, jobLanguage1.Language.Code, jobLanguage1.Language.Name),
                new JobLanguage(jobId, jobLanguage2.Language.Id, jobLanguage2.Language.Code, jobLanguage2.Language.Name)
            };

            // Act
            current.Calibrate(request, jobId);

            // Assert
            Assert.Equal(expectedJobLanguageIds, current.Select(x => x.Language.Id));
        }

        [Theory, AutoMoqData]
        public void CalibrateJobLanguages_ShouldChange_WhenPartOfJobLanguageIdsDifferent(
            Guid jobId,
            JobLanguage jobLanguage1,
            JobLanguage jobLanguage2,
            JobLanguage jobLanguage3)
        {
            // Arrange
            var expected = new List<JobLanguage>() {
                new JobLanguage(jobId, jobLanguage2.Language.Id, jobLanguage2.Language.Code, jobLanguage2.Language.Name),
                new JobLanguage(jobId, jobLanguage3.Language.Id, jobLanguage3.Language.Code, jobLanguage3.Language.Name)
            };

            var expectedJobLanguageIds = expected.Select(x => x.Language.Id).ToHashSet();

            var current = new List<JobLanguage>() {
                new JobLanguage(jobId, jobLanguage1.Language.Id, jobLanguage1.Language.Code, jobLanguage1.Language.Name),
                new JobLanguage(jobId, jobLanguage2.Language.Id, jobLanguage2.Language.Code, jobLanguage2.Language.Name),
            };

            var request = new List<JobLanguage>() {
                new JobLanguage(jobId, jobLanguage2.Language.Id, jobLanguage2.Language.Code, jobLanguage2.Language.Name),
                new JobLanguage(jobId, jobLanguage3.Language.Id, jobLanguage3.Language.Code, jobLanguage3.Language.Name)
            };

            // Act
            current.Calibrate(request, jobId);

            // Assert
            Assert.Equal(expectedJobLanguageIds, current.Select(x => x.Language.Id));
        }

        [Theory, AutoMoqData]
        public void CalibrateJobLanguages_ShouldAdd_WhenCurrentLanguageIdsEmpty(
            Guid jobId,
            JobLanguage jobLanguage1,
            JobLanguage jobLanguage2)
        {
            // Arrange
            var expected = new List<JobLanguage>() {
                new JobLanguage(jobId, jobLanguage1.Language.Id, jobLanguage1.Language.Code, jobLanguage1.Language.Name),
                new JobLanguage(jobId, jobLanguage2.Language.Id, jobLanguage2.Language.Code, jobLanguage2.Language.Name)
            };

            var expectedJobLanguageIds = expected.Select(x => x.Language.Id).ToHashSet();

            var current = new List<JobLanguage>();

            var request = new List<JobLanguage>() {
                new JobLanguage(jobId, jobLanguage1.Language.Id, jobLanguage1.Language.Code, jobLanguage1.Language.Name),
                new JobLanguage(jobId, jobLanguage2.Language.Id, jobLanguage2.Language.Code, jobLanguage2.Language.Name)
            };

            // Act
            current.Calibrate(request, jobId);

            // Assert
            Assert.Equal(expectedJobLanguageIds, current.Select(x => x.Language.Id));
        }
    }
}
