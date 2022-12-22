using Domain.Seedwork.Enums;
using Jobs.Domain.Aggregates.JobAggregate.ValueObjects;
using Jobs.Domain.UnitTests.JobAggregate.DataSeed;
using System.Collections.Generic;
using Tests.Shared;
using Xunit;

namespace Jobs.Domain.UnitTests.JobAggregate
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
                false,
                null,
                null,
                null,
                workTypes,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                workingHourTypes,
                jobFormats);

            // Assert
            Assert.NotNull(terms.FullTimeWorkingHours);
            Assert.Null(terms.PartTimeWorkingHours);
        }

        [Theory, AutoMoqData]
        public void Constructor_ShouldInitializePartTime(
            ICollection<WorkType> workTypes,
            ICollection<FormatType> jobFormats,
            int? weeklyHours)
        {
            // Arrange
            var workingHourTypes = new List<WorkingHoursType>
            {
                WorkingHoursType.PartTime
            };

            // Act
            var terms = new Terms(
                false,
                null,
                null,
                null,
                workTypes,
                null,
                weeklyHours,
                null,
                null,
                null,
                null,
                null,
                null,
                workingHourTypes,
                jobFormats);

            // Assert
            Assert.NotNull(terms.PartTimeWorkingHours);
            Assert.Equal(weeklyHours, terms.PartTimeWorkingHours!.Weekly);
            Assert.Null(terms.FullTimeWorkingHours);
        }


        [Theory, AutoMoqData]
        public void Constructor_ShouldInitializeWithValues(
            bool isUrgent,
            string? currency,
            int? hoursPerProject,
            int? weeklyHours,
            decimal? freelanceHourlySalaryFrom,
            decimal? freelanceHourlySalaryTo,
            decimal? freelanceMonthlySalaryFrom,
            decimal? freelanceMonthlySalaryTo,
            decimal? permanentMonthlySalaryFrom,
            decimal? permanentMonthlySalaryTo)
        {
            // Arrange
            var termsDataSeed = new JobDataSeed();
            var workTypes = new List<WorkType>
            {
                WorkType.Freelance,
                WorkType.Permanent
            };

            // Act
            var terms = new Terms(
                isUrgent,
                termsDataSeed.StartDate,
                termsDataSeed.EndDate,
                currency,
                workTypes,
                hoursPerProject,
                weeklyHours,
                freelanceHourlySalaryFrom,
                freelanceHourlySalaryTo,
                freelanceMonthlySalaryFrom,
                freelanceMonthlySalaryTo,
                permanentMonthlySalaryFrom,
                permanentMonthlySalaryTo,
                new List<WorkingHoursType>(),
                new List<FormatType>());

            // Assert
            Assert.NotNull(terms.Freelance);
            Assert.Equal(isUrgent, terms.IsUrgent);
            Assert.Equal(freelanceHourlySalaryFrom, terms.Freelance!.HourlyBudget!.From);
            Assert.Equal(freelanceHourlySalaryTo, terms.Freelance!.HourlyBudget!.To);
            Assert.Equal(freelanceMonthlySalaryFrom, terms.Freelance!.MonthlyBudget!.From);
            Assert.Equal(freelanceMonthlySalaryTo, terms.Freelance!.MonthlyBudget!.To);

            Assert.NotNull(terms.Permanent);
            Assert.Equal(permanentMonthlySalaryFrom, terms.Permanent!.MonthlyBudget!.From);
            Assert.Equal(permanentMonthlySalaryTo, terms.Permanent!.MonthlyBudget!.To);

            Assert.Equal(termsDataSeed.StartDate, terms.Availability?.From);
            Assert.Equal(termsDataSeed.EndDate, terms.Availability?.To);
        }
    }
}
