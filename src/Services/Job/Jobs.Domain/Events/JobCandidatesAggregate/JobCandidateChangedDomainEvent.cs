using Jobs.Domain.Aggregates.JobCandidatesAggregate;
using Jobs.Domain.Aggregates.JobCandidatesAggregate.Entities;

namespace Jobs.Domain.Events.JobCandidatesAggregate
{
    public abstract class JobCandidateChangedDomainEvent : JobCandidatesChangedDomainEvent
    {
        protected JobCandidateChangedDomainEvent(
            JobCandidates jobCandidates,
            JobCandidateBase jobCandidate,
            DateTimeOffset emittedAt,
            string eventName) : base(jobCandidates, emittedAt, eventName)
        {
            JobCandidate = jobCandidate;
        }

        public JobCandidateBase JobCandidate { get; }
    }
}
