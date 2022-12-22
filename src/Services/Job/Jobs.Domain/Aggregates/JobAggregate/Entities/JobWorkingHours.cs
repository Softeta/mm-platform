using Domain.Seedwork;
using Domain.Seedwork.Enums;

namespace Jobs.Domain.Aggregates.JobAggregate.Entities
{
    public class JobWorkingHours : Entity
    {
        public Guid JobId { get; private set; }
        public WorkingHoursType WorkingHoursType { get; private set; }

        public JobWorkingHours(Guid jobId, WorkingHoursType workingHoursType)
        {
            Id = Guid.NewGuid();
            JobId = jobId;
            WorkingHoursType = workingHoursType;
            CreatedAt = DateTimeOffset.UtcNow;
        }
    }
}
