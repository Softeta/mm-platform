using EventBus.Constants;
using Jobs.Domain.Aggregates.JobAggregate;

namespace Jobs.Domain.Events.JobAggregate;

public class JobCreatedDomainEvent : JobChangedDomainEvent
{
    public JobCreatedDomainEvent(Job job, DateTimeOffset emittedAt) :
            base(job, emittedAt, Topics.JobChanged.Filters.JobCreated)
    {
    }
}

