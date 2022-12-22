using Companies.Domain.Aggregates.CompanyAggregate;
using EventBus.Constants;

namespace Companies.Domain.Events
{
    public class CompanyRejectedDomainEvent : CompanyChangedDomainEvent
    {
        public CompanyRejectedDomainEvent(Company company, DateTimeOffset emittedAt) :
            base(company, emittedAt, Topics.CompanyChanged.Filters.CompanyRejected)
        {
        }
    }
}
