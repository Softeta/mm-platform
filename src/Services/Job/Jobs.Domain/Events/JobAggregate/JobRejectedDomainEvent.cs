using EventBus.Constants;
using Jobs.Domain.Aggregates.JobAggregate;

namespace Jobs.Domain.Events.JobAggregate
{
    public class JobRejectedDomainEvent : JobChangedDomainEvent
    {
        public JobRejectedDomainEvent(Job job, DateTimeOffset emittedAt) :
            base(job, emittedAt, Topics.JobChanged.Filters.JobRejected)
        {
        }
    }
}
