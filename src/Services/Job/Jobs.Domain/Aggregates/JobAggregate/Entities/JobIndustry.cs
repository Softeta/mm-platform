using Domain.Seedwork.Shared.Entities;

namespace Jobs.Domain.Aggregates.JobAggregate.Entities
{
    public class JobIndustry : IndustryBase
    {
        public Guid JobId { get; private set; }

        public JobIndustry(Guid industryId, Guid jobId, string code)
        {
            Id = Guid.NewGuid();
            IndustryId = industryId;
            JobId = jobId;
            Code = code;
            CreatedAt = DateTimeOffset.UtcNow;
        }
    }
}
