using Candidates.Domain.Aggregates.CandidateAggregate.ValueObjects;
using Candidates.Domain.UnitTests.CandidateAggregate.DataSeed;
using Domain.Seedwork.Enums;
using Domain.Seedwork.Exceptions;
using System;
using System.Collections.Generic;
using Tests.Shared;
using Xunit;

namespace Candidates.Domain.UnitTests.CandidateAggregate
{
    public class TermsTests
    {
        [Theory, AutoMoqData]
        public void Constructor_ShouldInitializeFullTime(
            ICollection<WorkType> workTypes,
            ICollection<FormatType> jobFormats)
        {
            // Arrange
            var workingHourTypes = new List<WorkingHoursType>
            {
                WorkingHoursType.FullTime
            };

            // Act
            var terms = new Terms(
                workTypes,
                null,
                null,
                null,
                null,
                null,
                null,
                workingHourTypes,
                null,
                jobFormats);

            // Assert
            Assert.NotNull(terms.FulTimeWorkingHours);
            Assert.Null(terms.PartTimeWorkingHours);
        }

        [Theory, AutoMoqData]
        public void Constructor_ShouldInitializePartTime(
            ICollection<WorkType> workTypes,
            ICollection<FormatType> jobFormats,
            int? workHours)
        {
            // Arrange
            var workingHourTypes = new List<WorkingHoursType>
            {
                WorkingHoursType.PartTime
            };

            // Act
            var terms = new Terms(
                workTypes,
                null,
                null,
                null,
                null,
                null,
                null,
                workingHourTypes,
                workHours,
                jobFormats);

            // Assert
            Assert.NotNull(terms.PartTimeWorkingHours);
            Assert.Equal(workHours, terms.PartTimeWorkingHours!.Weekly);
            Assert.Null(terms.FulTimeWorkingHours);
        }
        [Theory, AutoMoqData]
        public void Constructor_ShouldInitializeProjectEmployment(
            ICollection<WorkType> workTypes,
            ICollection<FormatType> jobFormats)
        {
            // Arrange
            var workingHourTypes = new List<WorkingHoursType>
            {
                WorkingHoursType.ProjectEmployment
            };

            // Act
            var terms = new Terms(
                workTypes,
                null,
                null,
                null,
                null,
                null,
                null,
                workingHourTypes,
                null,
                jobFormats);

            // Assert
            Assert.NotNull(terms.ProjectWorkingHours);
        }

        [Theory, AutoMoqData]
        public void Constructor_ShouldInitializeWithValues(
            decimal? freelanceHourlySalary,
            decimal? freelanceMonthlySalary,
            decimal? permanentMonthlySalary,
            string? currency,
            ICollection<WorkingHoursType>? workingHourTypes)
        {
            // Arrange
            var termTestData = new CandidateDataSeed();
            var workTypes = new List<WorkType>
            {
                WorkType.Freelance,
                WorkType.Permanent
            };

            // Act
            var terms = new Terms(
                workTypes,
                freelanceHourlySalary,
                freelanceMonthlySalary,
                permanentMonthlySalary,
                termTestData.StartDate,
                termTestData.EndDate,
                currency,
                workingHourTypes,
                null,
                new List<FormatType>());

            // Assert
            Assert.NotNull(terms.Freelance);
            Assert.Equal(freelanceHourlySalary, terms.Freelance!.HourlySalary);
            Assert.Equal(freelanceMonthlySalary, terms.Freelance!.MonthlySalary);

            Assert.NotNull(terms.Permanent);
            Assert.Equal(permanentMonthlySalary, terms.Permanent!.MonthlySalary);
        }
    }
}
