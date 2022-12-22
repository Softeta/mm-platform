using Candidates.Domain.Aggregates.CandidateAggregate;
using EventBus.Constants;

namespace Candidates.Domain.Events.CandidateAggregate
{
    public class CandidateApprovedDomainEvent : CandidateChangedDomainEvent
    {
        public CandidateApprovedDomainEvent(Candidate candidate, DateTimeOffset emittedAt) :
            base(candidate, emittedAt, Topics.CandidateChanged.Filters.CandidateApproved)
        {
        }
    }
}
