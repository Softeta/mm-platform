using DomainSalaryBudget = Jobs.Domain.Aggregates.JobAggregate.ValueObjects.SalaryBudget;
using Common = Contracts.Job;

namespace Jobs.Application.Contracts
{
    internal class SalaryBudget : Common.SalaryBudget
    {
        public static Common.SalaryBudget FromDomain(DomainSalaryBudget salaryBudget)
        {
            return new Common.SalaryBudget
            {
                From = salaryBudget.From,
                To = salaryBudget.To
            };
        }
    };

}
