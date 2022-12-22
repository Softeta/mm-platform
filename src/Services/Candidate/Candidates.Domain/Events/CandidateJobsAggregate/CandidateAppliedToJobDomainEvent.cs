using Candidates.Domain.Aggregates.CandidateJobsAggregate;
using EventBus.Constants;

namespace Candidates.Domain.Events.CandidateJobsAggregate
{
    public class CandidateAppliedToJobDomainEvent : CandidateJobChangedDomainEvent
    {
        public CandidateAppliedToJobDomainEvent(CandidateJobs candidateJobs, Guid jobId, DateTimeOffset emittedAt) :
            base(candidateJobs, jobId, emittedAt, Topics.CandidateJobsChanged.Filters.CandidateAppliedToJob)
        {
        }
    }
}
