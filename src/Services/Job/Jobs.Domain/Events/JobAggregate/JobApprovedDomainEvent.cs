using EventBus.Constants;
using Jobs.Domain.Aggregates.JobAggregate;

namespace Jobs.Domain.Events.JobAggregate
{
    public class JobApprovedDomainEvent : JobChangedDomainEvent
    {
        public JobApprovedDomainEvent(Job job, DateTimeOffset emittedAt) :
            base(job, emittedAt, Topics.JobChanged.Filters.JobApproved)
        {
        }
    }
}