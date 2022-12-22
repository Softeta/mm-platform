using Companies.Domain.Aggregates.CompanyAggregate;
using Companies.Domain.Aggregates.CompanyAggregate.Entities;
using EventBus.Constants;

namespace Companies.Domain.Events;

public class ContactPersonRegisteredDomainEvent : ContactPersonChangedDomainEvent
{
    public ContactPersonRegisteredDomainEvent(Company company, ContactPerson contactPerson, DateTimeOffset emittedAt) : 
        base(company, contactPerson, emittedAt, Topics.ContactPersonChanged.Filters.ContactPersonRegistered)
    {
    }
}
