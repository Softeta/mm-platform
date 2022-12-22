using Domain.Seedwork;
using Domain.Seedwork.Enums;

namespace Candidates.Domain.Aggregates.CandidateAggregate.ValueObjects
{
    public class Freelance : ValueObject<Freelance>
    {
        public WorkType WorkType { get; init; }
        public decimal? HourlySalary { get; init; }
        public decimal? MonthlySalary { get; init; }

        private Freelance() { }

        public Freelance(decimal? perHour, decimal? perMonth)
        {
            WorkType = WorkType.Freelance;
            HourlySalary = perHour;
            MonthlySalary = perMonth;
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return WorkType;
            yield return HourlySalary;
            yield return MonthlySalary;
        }
    }
}
