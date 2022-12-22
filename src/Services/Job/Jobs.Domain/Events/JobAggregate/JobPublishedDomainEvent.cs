using EventBus.Constants;
using Jobs.Domain.Aggregates.JobAggregate;

namespace Jobs.Domain.Events.JobAggregate
{
    public class JobPublishedDomainEvent : JobChangedDomainEvent
    {
        public JobPublishedDomainEvent(Job job, DateTimeOffset emittedAt) :
                base(job, emittedAt, Topics.JobChanged.Filters.JobPublished)
        {
        }
    }
}
