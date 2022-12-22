using Domain.Seedwork.Enums;
using DomainValueObject = Jobs.Domain.Aggregates.JobAggregate.ValueObjects;

namespace Jobs.Application.IntegrationEventHandlers.Publishers.Payloads.Models.Job
{
    public class Freelance
    {
        public WorkType WorkType { get; set; }
        public int? HoursPerProject { get; set; }
        public decimal? HourlySalaryFrom { get; set; }
        public decimal? HourlySalaryTo { get; set; }
        public decimal? MonthlySalaryFrom { get; set; }
        public decimal? MonthlySalaryTo { get; set; }

        public static Freelance? FromDomain(DomainValueObject.Freelance? freelance)
        {
            if (freelance is null) return null;

            return new Freelance
            {
                WorkType = freelance.WorkType,
                HoursPerProject = freelance.HoursPerProject,
                HourlySalaryFrom = freelance.HourlyBudget?.From,
                HourlySalaryTo = freelance.HourlyBudget?.To,
                MonthlySalaryFrom = freelance.MonthlyBudget?.From,
                MonthlySalaryTo = freelance.MonthlyBudget?.To
            };
        }
    }
}
