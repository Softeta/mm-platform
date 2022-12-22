using Candidates.Domain.Aggregates.CandidateJobsAggregate;
using EventBus.Constants;

namespace Candidates.Domain.Events.CandidateJobsAggregate;

public class CandidateJobsArchivedDomainEvent : CandidateJobsChangedDomainEvent
{
    public CandidateJobsArchivedDomainEvent(CandidateJobs candidateJobs, DateTimeOffset emittedAt) : 
        base(candidateJobs, emittedAt, Topics.CandidateJobsChanged.Filters.CandidateJobsArchived)
    {
    }
}
