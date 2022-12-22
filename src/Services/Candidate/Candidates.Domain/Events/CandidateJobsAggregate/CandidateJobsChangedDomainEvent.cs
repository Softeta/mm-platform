using Candidates.Domain.Aggregates.CandidateJobsAggregate;
using Domain.Seedwork;

namespace Candidates.Domain.Events.CandidateJobsAggregate
{
    public abstract class CandidateJobsChangedDomainEvent : Event
    {
        protected CandidateJobsChangedDomainEvent(CandidateJobs candidateJobs, DateTimeOffset emittedAt, string eventName)
        {
            CandidateJobs = candidateJobs;
            EmittedAt = emittedAt;
            EventName = eventName;
        }

        public CandidateJobs CandidateJobs { get; }
        public DateTimeOffset EmittedAt { get; }
        public string EventName { get; }
    }
}
