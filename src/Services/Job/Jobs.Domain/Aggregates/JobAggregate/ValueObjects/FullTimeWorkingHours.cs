using Domain.Seedwork;
using Domain.Seedwork.Enums;

namespace Jobs.Domain.Aggregates.JobAggregate.ValueObjects
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
