using EventBus.Constants;
using Jobs.Domain.Aggregates.JobAggregate;

namespace Jobs.Domain.Events.JobAggregate;

public class JobArchivedDomainEvent : JobChangedDomainEvent
{
    public JobArchivedDomainEvent(Job job, DateTimeOffset emittedAt) : 
        base(job, emittedAt, Topics.JobChanged.Filters.JobArchived)
    {
    }
}