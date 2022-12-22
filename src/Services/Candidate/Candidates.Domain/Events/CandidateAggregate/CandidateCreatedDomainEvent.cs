using Candidates.Domain.Aggregates.CandidateAggregate;
using EventBus.Constants;

namespace Candidates.Domain.Events.CandidateAggregate;

public class CandidateCreatedDomainEvent : CandidateChangedDomainEvent
{
    public CandidateCreatedDomainEvent(Candidate candidate, DateTimeOffset emittedAt) : 
        base(candidate, emittedAt, Topics.CandidateChanged.Filters.CandidateCreated)
    {
    }
}
