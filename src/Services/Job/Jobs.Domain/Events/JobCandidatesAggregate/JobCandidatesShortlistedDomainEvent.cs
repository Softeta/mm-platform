using EventBus.Constants;
using Jobs.Domain.Aggregates.JobCandidatesAggregate;

namespace Jobs.Domain.Events.JobCandidatesAggregate
{
    public class JobCandidatesShortlistedDomainEvent : JobCandidatesChangedDomainEvent
    {
        public JobCandidatesShortlistedDomainEvent(
            JobCandidates jobCandidates, 
            DateTimeOffset emittedAt) :
            base(jobCandidates, emittedAt, Topics.JobCandidatesChanged.Filters.JobCandidatesShortlisted)
        {
        }
    }
}
