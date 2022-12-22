using Domain.Seedwork.Enums;
using Jobs.Domain.Aggregates.JobAggregate.ValueObjects;
using Tests.Shared;
using Xunit;

namespace Jobs.Domain.UnitTests.JobAggregate
{
    public class PermanentTests
    {
        [Theory, AutoMoqData]
        public void Constructor_ShouldInitializeFreelance(
            decimal? salaryPerMonthFrom,
            decimal? salaryPerMonthTo)
        {
            // Assert 
            var expectedWorkType = WorkType.Permanent;

            // Act 
            var permanent = new Permanent(
                salaryPerMonthFrom,
                salaryPerMonthTo);

            Assert.Equal(expectedWorkType, permanent.WorkType);
            Assert.NotNull(permanent.MonthlyBudget);
        }
    }
}
