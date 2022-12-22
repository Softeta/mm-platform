using EventBus.Constants;
using Jobs.Domain.Aggregates.JobAggregate;

namespace Jobs.Domain.Events.JobAggregate
{
    public class JobUpdatedDomainEvent : JobChangedDomainEvent
    {
        public JobUpdatedDomainEvent(Job job, DateTimeOffset emittedAt) :
                base(job, emittedAt, Topics.JobChanged.Filters.JobUpdated)
        {
        }
    }
}
