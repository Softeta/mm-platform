using Domain.Seedwork;
using Domain.Seedwork.Enums;

namespace Jobs.Domain.Aggregates.JobAggregate.Entities
{
    public class JobSeniority : Entity
    {
        public Guid JobId { get; private set; }
        public SeniorityLevel Seniority { get; private set; }

        public JobSeniority(Guid jobId, SeniorityLevel seniority)
        {
            Id = Guid.NewGuid();
            JobId = jobId;
            Seniority = seniority;
            CreatedAt = DateTimeOffset.UtcNow;
        }
    }
}
