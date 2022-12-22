using Domain.Seedwork;

namespace Jobs.Domain.Aggregates.JobAggregate.Entities
{
    public class JobInterestedLinkedIn : Entity
    {
        public Guid JobId { get; private set; }
        public string Url { get; private set; } = null!;

        public JobInterestedLinkedIn(Guid jobId, string url)
        {
            Id = Guid.NewGuid();
            JobId = jobId;
            Url = url;
            CreatedAt = DateTimeOffset.UtcNow;
        }
    }
}
