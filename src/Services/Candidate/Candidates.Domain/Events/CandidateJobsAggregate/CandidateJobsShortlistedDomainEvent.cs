using Candidates.Domain.Aggregates.CandidateJobsAggregate;
using EventBus.Constants;

namespace Candidates.Domain.Events.CandidateJobsAggregate
{
    public class CandidateJobsShortlistedDomainEvent : CandidateJobsChangedDomainEvent
    {
        public CandidateJobsShortlistedDomainEvent(CandidateJobs candidateJobs, DateTimeOffset emittedAt) :
            base(candidateJobs, emittedAt, Topics.CandidateJobsChanged.Filters.CandidateJobsShortlisted)
        {
        }
    }
}
