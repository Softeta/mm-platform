using EventBus.Constants;
using Jobs.Domain.Aggregates.JobCandidatesAggregate;

namespace Jobs.Domain.Events.JobCandidatesAggregate
{
    public class SelectedCandidatesAddedDomainEvent : JobCandidatesChangedDomainEvent
    {
        public SelectedCandidatesAddedDomainEvent(JobCandidates jobCandidates, DateTimeOffset emittedAt) :
            base(jobCandidates, emittedAt, Topics.JobCandidatesChanged.Filters.JobSelectedCandidatesAdded)
        {
        }
    }
}
