using Candidates.Domain.Aggregates.CandidateJobsAggregate;
using EventBus.Constants;

namespace Candidates.Domain.Events.CandidateJobsAggregate;

public class CandidateJobsHiredDomainEvent : CandidateJobsChangedDomainEvent
{
    public CandidateJobsHiredDomainEvent(CandidateJobs candidateJobs, DateTimeOffset emittedAt) : 
        base(candidateJobs, emittedAt, Topics.CandidateJobsChanged.Filters.CandidateJobsHired)
    {
    }
}