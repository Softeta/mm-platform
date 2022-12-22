using Candidates.Domain.Aggregates.CandidateAggregate;
using EventBus.Constants;

namespace Candidates.Domain.Events.CandidateAggregate
{
    public class CandidateSkillsSyncedDomainEvent : CandidateChangedDomainEvent
    {
        public CandidateSkillsSyncedDomainEvent(Candidate candidate, DateTimeOffset emittedAt) :
            base(candidate, emittedAt, Topics.CandidateChanged.Filters.CandidateSkillsSynced)
        {
        }
    }
}
