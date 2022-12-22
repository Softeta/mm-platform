using Candidates.Domain.Aggregates.CandidateAggregate.ValueObjects;
using Domain.Seedwork.Enums;

namespace Candidates.Application.IntegrationEventHandlers.Publishers.Payloads.Models.Candidate
{
    internal class FreelanceContract
    {
        public WorkType WorkType { get; set; }
        public decimal? HourlySalary { get; set; }
        public decimal? MonthlySalary { get; set; }

        public static FreelanceContract? FromDomain(Freelance? freelance)
        {
            if (freelance is null) return null;

            return new FreelanceContract
            {
                HourlySalary = freelance.HourlySalary,
                MonthlySalary = freelance.MonthlySalary,
                WorkType = freelance.WorkType
            };
        }
    }
}
