using Domain.Seedwork.Enums;
using DomainValueObject = Jobs.Domain.Aggregates.JobAggregate.ValueObjects;

namespace Jobs.Application.IntegrationEventHandlers.Publishers.Payloads.Models.Job
{
    public class Permanent
    {
        public WorkType WorkType { get; set; }
        public decimal? MonthlySalaryFrom { get; set; }
        public decimal? MonthlySalaryTo { get; set; }

        public static Permanent? FromDomain(DomainValueObject.Permanent? permanent)
        {
            if (permanent is null) return null;

            return new Permanent
            {
                WorkType = permanent.WorkType,
                MonthlySalaryFrom = permanent.MonthlyBudget?.From,
                MonthlySalaryTo = permanent.MonthlyBudget?.To
            };
        }
    }
}
