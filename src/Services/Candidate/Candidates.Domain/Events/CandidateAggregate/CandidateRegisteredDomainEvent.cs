using Candidates.Domain.Aggregates.CandidateAggregate;
using EventBus.Constants;

namespace Candidates.Domain.Events.CandidateAggregate;

public class CandidateRegisteredDomainEvent : CandidateChangedDomainEvent
{
    public CandidateRegisteredDomainEvent(Candidate candidate, DateTimeOffset emittedAt) : 
        base(candidate, emittedAt, Topics.CandidateChanged.Filters.CandidateRegistered)
    {
    }
}
