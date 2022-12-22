using Companies.Domain.Aggregates.CompanyAggregate;
using EventBus.Constants;

namespace Companies.Domain.Events;

public class CompanyUpdatedDomainEvent : CompanyChangedDomainEvent
{
    public CompanyUpdatedDomainEvent(Company company, DateTimeOffset emittedAt) : 
        base(company, emittedAt, Topics.CompanyChanged.Filters.CompanyUpdated)
    {
    }
}
