using Candidates.Domain.Aggregates.CandidateAggregate;
using EventBus.Constants;

namespace Candidates.Domain.Events.CandidateAggregate
{
    public class CandidateJobPositionSyncedDomainEvent : CandidateChangedDomainEvent
    {
        public CandidateJobPositionSyncedDomainEvent(Candidate candidate, DateTimeOffset emittedAt) :
            base(candidate, emittedAt, Topics.CandidateChanged.Filters.CandidateJobPositionSynced)
        {
        }
    }
}
