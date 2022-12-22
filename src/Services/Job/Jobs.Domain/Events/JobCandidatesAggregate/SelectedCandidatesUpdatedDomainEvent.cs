using EventBus.Constants;
using Jobs.Domain.Aggregates.JobCandidatesAggregate;

namespace Jobs.Domain.Events.JobCandidatesAggregate
{
    public class SelectedCandidatesUpdatedDomainEvent : JobCandidatesChangedDomainEvent
    {
        public SelectedCandidatesUpdatedDomainEvent(JobCandidates jobCandidates, DateTimeOffset emittedAt) :
            base(jobCandidates, emittedAt, Topics.JobCandidatesChanged.Filters.JobSelectedCandidatesUpdated)
        {
        }
    }
}
