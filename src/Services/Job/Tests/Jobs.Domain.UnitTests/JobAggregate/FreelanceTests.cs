using Domain.Seedwork.Enums;
using Jobs.Domain.Aggregates.JobAggregate.ValueObjects;
using Tests.Shared;
using Xunit;

namespace Jobs.Domain.UnitTests.JobAggregate
{
    public class FreelanceTests
    {
        [Theory, AutoMoqData]
        public void Constructor_ShouldInitializeFreelance(
            int? hoursPerProject,
            decimal? salaryBudgetHourFrom,
            decimal? salaryBudgetPerHourTo,
            decimal? salaryBudgetPerMonthFrom,
            decimal? salaryBudgetPerMonthTo)
        {
            // Assert 
            var expectedWorkType = WorkType.Freelance;

            // Act 
            var freelance = new Freelance(
                hoursPerProject,
                salaryBudgetHourFrom,
                salaryBudgetPerHourTo,
                salaryBudgetPerMonthFrom,
                salaryBudgetPerMonthTo);

            Assert.Equal(expectedWorkType, freelance.WorkType);
            Assert.Equal(hoursPerProject, freelance.HoursPerProject);
            Assert.NotNull(freelance.HourlyBudget);
            Assert.NotNull(freelance.MonthlyBudget);
        }
    }
}
