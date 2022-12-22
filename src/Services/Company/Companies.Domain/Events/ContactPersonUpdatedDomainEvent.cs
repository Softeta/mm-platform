using Companies.Domain.Aggregates.CompanyAggregate;
using Companies.Domain.Aggregates.CompanyAggregate.Entities;
using EventBus.Constants;

namespace Companies.Domain.Events;

public class ContactPersonUpdatedDomainEvent : ContactPersonChangedDomainEvent
{
    public ContactPersonUpdatedDomainEvent(Company company, ContactPerson contactPerson, DateTimeOffset emittedAt) : 
        base(company, contactPerson, emittedAt, Topics.ContactPersonChanged.Filters.ContactPersonUpdated)
    {
    }
}
