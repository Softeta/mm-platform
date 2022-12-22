using EventBus.Constants;
using Jobs.Domain.Aggregates.JobAggregate;

namespace Jobs.Domain.Events.JobAggregate
{
    public class JobPositionSyncedDomainEvent : JobChangedDomainEvent
    {
        public JobPositionSyncedDomainEvent(Job job, DateTimeOffset emittedAt) :
                base(job, emittedAt, Topics.JobChanged.Filters.JobPositionSynced)
        {
        }
    }
}
