using EventBus.Constants;
using Jobs.Domain.Aggregates.JobCandidatesAggregate;

namespace Jobs.Domain.Events.JobCandidatesAggregate
{
    public class ArchivedCandidatesUpdatedDomainEvent : JobCandidatesChangedDomainEvent
    {
        public ArchivedCandidatesUpdatedDomainEvent(JobCandidates jobCandidates, DateTimeOffset emittedAt) :
            base(jobCandidates, emittedAt, Topics.JobCandidatesChanged.Filters.JobArchivedCandidatesChanged)
        {
        }
    }
}
