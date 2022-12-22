using Candidates.Domain.Aggregates.CandidateAggregate;
using EventBus.Constants;

namespace Candidates.Domain.Events.CandidateAggregate;

public class CandidateUpdatedDomainEvent : CandidateChangedDomainEvent
{
    public CandidateUpdatedDomainEvent(Candidate candidate, DateTimeOffset emittedAt) : 
        base(candidate, emittedAt, Topics.CandidateChanged.Filters.CandidateUpdated)
    {
    }
}
