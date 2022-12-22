using Domain.Seedwork;
using Jobs.Domain.Aggregates.JobCandidatesAggregate;

namespace Jobs.Domain.Events.JobCandidatesAggregate
{
    public abstract class JobCandidatesChangedDomainEvent : Event
    {
        protected JobCandidatesChangedDomainEvent(JobCandidates jobCandidates, DateTimeOffset emittedAt, string eventName)
        {
            JobCandidates = jobCandidates;
            EmittedAt = emittedAt;
            EventName = eventName;
        }

        public JobCandidates JobCandidates { get; }
        public DateTimeOffset EmittedAt { get; }
        public string EventName { get; }
    }
}
