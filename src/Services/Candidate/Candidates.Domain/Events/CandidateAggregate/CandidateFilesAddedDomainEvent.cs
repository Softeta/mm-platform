using Domain.Seedwork;

namespace Candidates.Domain.Events.CandidateAggregate
{
    public class CandidateFilesAddedDomainEvent : Event
    {
        public CandidateFilesAddedDomainEvent(List<Guid> cacheIds, DateTimeOffset emittedAt)
        {
            CacheIds = cacheIds;
            EmittedAt = emittedAt;
        }

        public List<Guid> CacheIds { get; }
        public DateTimeOffset EmittedAt { get; }
    }
}
