using Domain.Seedwork;

namespace Jobs.Domain.Aggregates.JobAggregate.ValueObjects
{
    public class SalaryBudget : ValueObject<SalaryBudget>
    {
        public decimal? From { get; init; }
        public decimal? To { get; init; }

        private SalaryBudget() { }

        public SalaryBudget(decimal? from, decimal? to)
        {
            From = from;
            To = to;
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return From;
            yield return To;
        }
    }
}
