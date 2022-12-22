using EventBus.Constants;
using Jobs.Domain.Aggregates.JobCandidatesAggregate;
using Jobs.Domain.Aggregates.JobCandidatesAggregate.Entities;

namespace Jobs.Domain.Events.JobCandidatesAggregate
{
    public class JobSelectedCandidateInvitedDomainEvent : JobCandidateChangedDomainEvent
    {
        public JobSelectedCandidateInvitedDomainEvent(
            JobCandidates jobCandidates, 
            JobCandidateBase jobCandidate,
            DateTimeOffset emittedAt) : base(jobCandidates, jobCandidate, emittedAt, Topics.JobCandidatesChanged.Filters.JobSelectedCandidateInvited)
        {
        }
    }
}
