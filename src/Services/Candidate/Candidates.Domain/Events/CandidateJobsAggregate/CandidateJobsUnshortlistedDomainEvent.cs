using Candidates.Domain.Aggregates.CandidateJobsAggregate;
using EventBus.Constants;

namespace Candidates.Domain.Events.CandidateJobsAggregate
{
    public class CandidateJobsUnshortlistedDomainEvent : CandidateJobsChangedDomainEvent
    {
        public CandidateJobsUnshortlistedDomainEvent(CandidateJobs candidateJobs, DateTimeOffset emittedAt) :
            base(candidateJobs, emittedAt, Topics.CandidateJobsChanged.Filters.CandidateJobsUnshortlisted)
        {
        }
    }
}
