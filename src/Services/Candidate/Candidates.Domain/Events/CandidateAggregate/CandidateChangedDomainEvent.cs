using Candidates.Domain.Aggregates.CandidateAggregate;
using Domain.Seedwork;

namespace Candidates.Domain.Events.CandidateAggregate
{
    public abstract class CandidateChangedDomainEvent : Event
    {
        protected CandidateChangedDomainEvent(Candidate candidate, DateTimeOffset emittedAt, string eventName)
        {
            Candidate = candidate;
            EmittedAt = emittedAt;
            EventName = eventName;
        }

        public Candidate Candidate { get; }
        public DateTimeOffset EmittedAt { get; }
        public string EventName { get; }
    }
}
