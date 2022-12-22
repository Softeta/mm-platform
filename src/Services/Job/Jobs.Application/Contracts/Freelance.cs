using DomainFreelance = Jobs.Domain.Aggregates.JobAggregate.ValueObjects.Freelance;
using Common = Contracts.Job;

namespace Jobs.Application.Contracts
{
    internal class Freelance : Common.Freelance
    {
        public static Common.Freelance FromDomain(DomainFreelance freelance)
        {
            return new Common.Freelance
            {
                HoursPerProject = freelance.HoursPerProject,
                HourlyBudget = freelance.HourlyBudget != null ?
                    SalaryBudget.FromDomain(freelance.HourlyBudget) :
                    null,
                MonthlyBudget = freelance.MonthlyBudget != null ?
                SalaryBudget.FromDomain(freelance.MonthlyBudget) :
                null
            };
        }
    }
}
