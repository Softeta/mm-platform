using Companies.Domain.Aggregates.CompanyAggregate;
using Companies.Domain.Aggregates.CompanyAggregate.Entities;
using Domain.Seedwork;

namespace Companies.Domain.Events
{
    public abstract class ContactPersonChangedDomainEvent : Event
    {
        protected ContactPersonChangedDomainEvent(
            Company company,
            ContactPerson contactPerson,
            DateTimeOffset emittedAt, 
            string eventName)
        {
            Company = company;
            ContactPerson = contactPerson;
            EmittedAt = emittedAt;
            EventName = eventName;
        }

        public Company Company { get; }
        public ContactPerson ContactPerson { get; }
        public DateTimeOffset EmittedAt { get; }
        public string EventName { get; }
    }
}
