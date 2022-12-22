using DomainPermanent = Jobs.Domain.Aggregates.JobAggregate.ValueObjects.Permanent;
using Common = Contracts.Job;

namespace Jobs.Application.Contracts
{
    internal class Permanent : Common.Permanent
    {
        public static Common.Permanent FromDomain(DomainPermanent permanent)
        {
            return new Common.Permanent
            {
                MonthlyBudget = permanent.MonthlyBudget != null ?
                    SalaryBudget.FromDomain(permanent.MonthlyBudget) :
                    null
            };
        }
    }
}
