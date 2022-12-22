using Candidates.Domain.Aggregates.CandidateJobsAggregate;
using EventBus.Constants;

namespace Candidates.Domain.Events.CandidateJobsAggregate;

public class CandidateJobsUpdatedDomainEvent : CandidateJobsChangedDomainEvent
{
    public CandidateJobsUpdatedDomainEvent(CandidateJobs candidateJobs, DateTimeOffset emittedAt) : 
        base(candidateJobs, emittedAt, Topics.CandidateJobsChanged.Filters.CandidateJobsUpdated)
    {
    }
}
