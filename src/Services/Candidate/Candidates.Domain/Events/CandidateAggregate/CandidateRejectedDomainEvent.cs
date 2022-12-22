using Candidates.Domain.Aggregates.CandidateAggregate;
using EventBus.Constants;

namespace Candidates.Domain.Events.CandidateAggregate
{
    public class CandidateRejectedDomainEvent : CandidateChangedDomainEvent
    {
        public CandidateRejectedDomainEvent(Candidate candidate, DateTimeOffset emittedAt) :
            base(candidate, emittedAt, Topics.CandidateChanged.Filters.CandidateRejected)
        {
        }
    }
}
