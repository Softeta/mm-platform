using Candidates.Domain.Aggregates.CandidateJobsAggregate;
using EventBus.Constants;

namespace Candidates.Domain.Events.CandidateJobsAggregate;

public class CandidateJobsAddedDomainEvent : CandidateJobsChangedDomainEvent
{
    public CandidateJobsAddedDomainEvent(CandidateJobs candidateJobs, DateTimeOffset emittedAt) : 
        base(candidateJobs, emittedAt, Topics.CandidateJobsChanged.Filters.CandidateJobsAdded)
    {
    }
}
