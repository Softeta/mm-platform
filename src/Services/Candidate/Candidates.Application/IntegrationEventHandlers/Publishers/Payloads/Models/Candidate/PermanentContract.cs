using Candidates.Domain.Aggregates.CandidateAggregate.ValueObjects;
using Domain.Seedwork.Enums;

namespace Candidates.Application.IntegrationEventHandlers.Publishers.Payloads.Models.Candidate
{
    internal class PermanentContract
    {
        public WorkType WorkType { get; set; }
        public decimal? MonthlySalary { get; set; }

        public static PermanentContract? FromDomain(Permanent? permanent)
        {
            if (permanent is null) return null;

            return new PermanentContract
            {
                MonthlySalary = permanent.MonthlySalary,
                WorkType = permanent.WorkType
            };
        }
    }
}
