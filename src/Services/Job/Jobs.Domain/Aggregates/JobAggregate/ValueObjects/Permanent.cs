using Domain.Seedwork;
using Domain.Seedwork.Enums;

namespace Jobs.Domain.Aggregates.JobAggregate.ValueObjects
{
    public class Permanent : ValueObject<Permanent>
    {
        public WorkType WorkType { get; init; }
        public SalaryBudget? MonthlyBudget { get; init; }

        private Permanent() { }

        public Permanent(
            decimal? monthlySalaryFrom,
            decimal? monthlySalaryTo)
        {
            WorkType = WorkType.Permanent;
            MonthlyBudget = new SalaryBudget(
                monthlySalaryFrom,
                monthlySalaryTo);
        }

        public Permanent(Permanent? permanent)
        {
            if (permanent == null)
            {
                return;
            }

            WorkType = permanent.WorkType;
            MonthlyBudget = permanent.MonthlyBudget;
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return WorkType;
            yield return MonthlyBudget;
        }
    }
}
