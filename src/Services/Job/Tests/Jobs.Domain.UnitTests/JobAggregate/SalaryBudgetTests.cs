using Jobs.Domain.Aggregates.JobAggregate.ValueObjects;
using Tests.Shared;
using Xunit;

namespace Jobs.Domain.UnitTests.JobAggregate
{
    public class SalaryBudgetTests
    {
        [Theory, AutoMoqData]
        public void Constructor_ShouldInitializeSalaryBudget(
            decimal from,
            decimal to)
        {
            // Act
            var salaryBudget = new SalaryBudget(from, to);

            // Assert
            Assert.Equal(from, salaryBudget.From);
            Assert.Equal(to, salaryBudget.To);
        }
    }
}
