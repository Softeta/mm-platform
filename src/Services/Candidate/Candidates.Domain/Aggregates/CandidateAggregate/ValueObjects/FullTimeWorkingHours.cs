using Domain.Seedwork;
using Domain.Seedwork.Enums;

namespace Candidates.Domain.Aggregates.CandidateAggregate.ValueObjects
{
    public class FullTimeWorkingHours : ValueObject<FullTimeWorkingHours>
    {
        public WorkingHoursType Type { get; init; }

        public FullTimeWorkingHours()
        {
            Type = WorkingHoursType.FullTime;
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Type;
        }
    }
}
