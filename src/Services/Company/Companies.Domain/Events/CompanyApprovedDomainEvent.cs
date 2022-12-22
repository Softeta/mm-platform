using Companies.Domain.Aggregates.CompanyAggregate;
using EventBus.Constants;

namespace Companies.Domain.Events
{
    public class CompanyApprovedDomainEvent : CompanyChangedDomainEvent
    {
        public CompanyApprovedDomainEvent(Company company, DateTimeOffset emittedAt) :
            base(company, emittedAt, Topics.CompanyChanged.Filters.CompanyApproved)
        {
        }
    }
}
