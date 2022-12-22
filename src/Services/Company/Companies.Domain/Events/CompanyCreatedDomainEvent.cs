using Companies.Domain.Aggregates.CompanyAggregate;
using EventBus.Constants;

namespace Companies.Domain.Events;

public class CompanyCreatedDomainEvent : CompanyChangedDomainEvent
{
    public CompanyCreatedDomainEvent(Company company, DateTimeOffset emittedAt) : 
        base(company, emittedAt, Topics.CompanyChanged.Filters.CompanyCreated)
    {
    }
}
