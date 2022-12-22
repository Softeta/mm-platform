using EventBus.Constants;
using Jobs.Domain.Aggregates.JobAggregate;

namespace Jobs.Domain.Events.JobAggregate;

public class JobSearchAndSelectionStartedDomainEvent : JobChangedDomainEvent
{
    public JobSearchAndSelectionStartedDomainEvent(Job job, DateTimeOffset emittedAt) :
            base(job, emittedAt, Topics.JobChanged.Filters.JobUpdated)
    {
    }
}

