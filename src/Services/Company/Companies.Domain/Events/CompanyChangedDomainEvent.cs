using Companies.Domain.Aggregates.CompanyAggregate;
using Domain.Seedwork;

namespace Companies.Domain.Events
{
    public abstract class CompanyChangedDomainEvent : Event
    {
        protected CompanyChangedDomainEvent(Company company, DateTimeOffset emittedAt, string eventName)
        {
            Company = company;
            EmittedAt = emittedAt;
            EventName = eventName;
        }

        public Company Company { get; }
        public DateTimeOffset EmittedAt { get; }
        public string EventName { get; }
    }
}
