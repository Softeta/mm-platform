using EventBus.Constants;
using Jobs.Domain.Aggregates.JobCandidatesAggregate;

namespace Jobs.Domain.Events.JobCandidatesAggregate
{
    public class JobCandidatesJobStageUpdatedDomainEvent : JobCandidatesChangedDomainEvent
    {
        public JobCandidatesJobStageUpdatedDomainEvent(JobCandidates jobCandidate, DateTimeOffset emittedAt)
            : base(jobCandidate, emittedAt, Topics.JobCandidatesChanged.Filters.JobCandidatesJobStageUpdated)
        {
        }
    }
}
