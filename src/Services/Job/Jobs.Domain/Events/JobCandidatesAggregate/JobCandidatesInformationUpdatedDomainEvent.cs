using EventBus.Constants;
using Jobs.Domain.Aggregates.JobCandidatesAggregate;

namespace Jobs.Domain.Events.JobCandidatesAggregate
{
    public class JobCandidatesInformationUpdatedDomainEvent : JobCandidatesChangedDomainEvent
    {
        public JobCandidatesInformationUpdatedDomainEvent(JobCandidates jobCandidates, DateTimeOffset emittedAt) :
            base(jobCandidates, emittedAt, Topics.JobCandidatesChanged.Filters.JobCandidatesInformationUpdated)
        {
        }
    }
}
