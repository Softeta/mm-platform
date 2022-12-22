using Domain.Seedwork;

namespace Candidates.Domain.Events.CandidateJobsAggregate
{
    public class CandidateJobsFilesAddedDomainEvent : Event
    {
        public CandidateJobsFilesAddedDomainEvent(List<Guid> cacheIds, DateTimeOffset emittedAt)
        {
            CacheIds = cacheIds;
            EmittedAt = emittedAt;
        }

        public List<Guid> CacheIds { get; }
        public DateTimeOffset EmittedAt { get; }
    }
}
