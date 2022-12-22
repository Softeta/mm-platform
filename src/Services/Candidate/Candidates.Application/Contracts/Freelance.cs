using DomainValueObjects = Candidates.Domain.Aggregates.CandidateAggregate.ValueObjects;
using Common = Contracts.Candidate;

namespace Candidates.Application.Contracts
{
    internal class Freelance : Common.CandidateFreelance
    {
        public static Common.CandidateFreelance? FromDomain(DomainValueObjects.Freelance? freelance)
        {
            if (freelance is null) return null;

            return new Common.CandidateFreelance
            {
                HourlySalary = freelance.HourlySalary,
                MonthlySalary = freelance.MonthlySalary,
            };
        }
    }
}
