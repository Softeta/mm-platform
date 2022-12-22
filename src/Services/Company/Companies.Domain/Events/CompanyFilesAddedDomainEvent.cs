using Domain.Seedwork;

namespace Companies.Domain.Events
{
    public class CompanyFilesAddedDomainEvent : Event
    {
        public CompanyFilesAddedDomainEvent(List<Guid> cacheIds, DateTimeOffset emittedAt)
        {
            CacheIds = cacheIds;
            EmittedAt = emittedAt;
        }

        public List<Guid> CacheIds { get; }
        public DateTimeOffset EmittedAt { get; }
    }
}
