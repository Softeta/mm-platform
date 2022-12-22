using Candidates.Domain.Aggregates.CandidateJobsAggregate;
using Domain.Seedwork;

namespace Candidates.Domain.Events.CandidateJobsAggregate
{
    public abstract class CandidateJobChangedDomainEvent : Event
    {
        protected CandidateJobChangedDomainEvent(CandidateJobs candidateJobs, Guid jobId, DateTimeOffset emittedAt, string eventName)
        {
            CandidateJobs = candidateJobs;
            JobId = jobId;
            EmittedAt = emittedAt;
            EventName = eventName;
        }

        public CandidateJobs CandidateJobs { get; }
        public Guid JobId { get; }
        public DateTimeOffset EmittedAt { get; }
        public string EventName { get; }
    }
}
