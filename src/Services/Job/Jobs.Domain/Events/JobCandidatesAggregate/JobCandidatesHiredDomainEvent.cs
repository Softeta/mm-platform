using EventBus.Constants;
using Jobs.Domain.Aggregates.JobCandidatesAggregate;

namespace Jobs.Domain.Events.JobCandidatesAggregate
{
    public class JobCandidatesHiredDomainEvent : JobCandidatesChangedDomainEvent
    {
        public JobCandidatesHiredDomainEvent(JobCandidates jobCandidate, DateTimeOffset emittedAt) 
            : base(jobCandidate, emittedAt, Topics.JobCandidatesChanged.Filters.JobCandidatesHired)
        {
        }
    }
}
