using EventBus.Constants;
using Jobs.Domain.Aggregates.JobAggregate;

namespace Jobs.Domain.Events.JobAggregate
{
    public class JobInitializedDomainEvent : JobChangedDomainEvent
    {
        public JobInitializedDomainEvent(Job job, DateTimeOffset emittedAt) :
                base(job, emittedAt, Topics.JobChanged.Filters.JobInitialized)
        {
        }
    }
}
