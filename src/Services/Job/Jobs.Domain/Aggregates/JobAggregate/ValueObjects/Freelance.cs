using Domain.Seedwork;
using Domain.Seedwork.Enums;

namespace Jobs.Domain.Aggregates.JobAggregate.ValueObjects
{
    public class Freelance : ValueObject<Freelance>
    {
        public WorkType WorkType { get; init; }
        public int? HoursPerProject { get; init; }
        public SalaryBudget? HourlyBudget { get; init; }
        public SalaryBudget? MonthlyBudget { get; init; }

        private Freelance() { }

        public Freelance(
            int? hoursPerProject,
            decimal? hourlySalaryFrom,
            decimal? hourlySalaryTo,
            decimal? monthlySalaryFrom,
            decimal? monthlySalaryTo)
        {
            WorkType = WorkType.Freelance;
            HoursPerProject = hoursPerProject;
            HourlyBudget = new SalaryBudget(
                hourlySalaryFrom,
                hourlySalaryTo);
            MonthlyBudget = new SalaryBudget(
                monthlySalaryFrom,
                monthlySalaryTo);
        }

        public Freelance(Freelance? freelance)
        {
            if (freelance == null)
            {
                return;
            }

            WorkType = freelance.WorkType;
            HoursPerProject = freelance.HoursPerProject;
            HourlyBudget = freelance.HourlyBudget;
            MonthlyBudget = freelance.MonthlyBudget;
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return WorkType;
            yield return HoursPerProject;
            yield return HourlyBudget;
            yield return MonthlyBudget;
        }
    }
}
