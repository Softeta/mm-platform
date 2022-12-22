using Candidates.Domain.Aggregates.CandidateJobsAggregate;
using Domain.Seedwork;

namespace Candidates.Domain.Events.CandidateJobsAggregate
{
    public class CandidateInJobJobArchivedDomainEvent : Event
    {
        public CandidateInJobJobArchivedDomainEvent(CandidateJobs candidateJobs, Guid jobId, DateTimeOffset emittedAt)
        {
            CandidateJobs = candidateJobs;
            JobId = jobId;
            EmittedAt = emittedAt;
        }

        public CandidateJobs CandidateJobs { get; }
        public Guid JobId { get; }
        public DateTimeOffset EmittedAt { get; }
    }
}
