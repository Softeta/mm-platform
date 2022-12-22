using Domain.Seedwork;
using Domain.Seedwork.Enums;

namespace Jobs.Domain.Aggregates.JobAggregate.ValueObjects
{
    public class PartTimeWorkingHours : ValueObject<PartTimeWorkingHours>
    {
        public WorkingHoursType Type { get; init; }
        public int? Weekly { get; init; }

        private PartTimeWorkingHours() { }

        public PartTimeWorkingHours(int? workingHours)
        {
            Type = WorkingHoursType.PartTime;
            Weekly = workingHours;
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Type;
            yield return Weekly;
        }
    }
}
