using Domain.Seedwork;
using Domain.Seedwork.Enums;

namespace Candidates.Domain.Aggregates.CandidateAggregate.ValueObjects
{
    public class Permanent : ValueObject<Permanent>
    {
        public WorkType WorkType { get; init; }
        public decimal? MonthlySalary { get; init; }

        private Permanent() { }

        public Permanent(decimal? minimumSalary)
        {
            WorkType = WorkType.Permanent;
            MonthlySalary = minimumSalary;
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return WorkType;
            yield return MonthlySalary;
        }
    }
}
