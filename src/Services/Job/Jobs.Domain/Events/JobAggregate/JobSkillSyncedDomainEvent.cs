using EventBus.Constants;
using Jobs.Domain.Aggregates.JobAggregate;

namespace Jobs.Domain.Events.JobAggregate
{
    public class JobSkillSyncedDomainEvent : JobChangedDomainEvent
    {
        public JobSkillSyncedDomainEvent(Job job, DateTimeOffset emittedAt) :
                base(job, emittedAt, Topics.JobChanged.Filters.JobSkillSynced)
        {
        }
    }
}
