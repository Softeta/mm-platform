using Candidates.Domain.Aggregates.CandidateAggregate;
using EventBus.Constants;

namespace Candidates.Domain.Events.CandidateAggregate
{
    public class CandidateEmailVerificationRequestedDomainEvent : CandidateChangedDomainEvent
    {
        public CandidateEmailVerificationRequestedDomainEvent(Candidate candidate, DateTimeOffset emittedAt) :
            base(candidate, emittedAt, Topics.CandidateChanged.Filters.CandidateEmailVerificationRequested)
        {
        }
    }
}