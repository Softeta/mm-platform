using Domain.Seedwork;
using Domain.Seedwork.Enums;

namespace Jobs.Domain.Aggregates.JobAggregate.ValueObjects
{
    public class ProjectWorkingHours : ValueObject<ProjectWorkingHours>
    {
        public WorkingHoursType Type { get; init; }

        public ProjectWorkingHours()
        {
            Type = WorkingHoursType.ProjectEmployment;
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Type;
        }
    }
}
