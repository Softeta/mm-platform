using DomainValueObjects = Candidates.Domain.Aggregates.CandidateAggregate.ValueObjects;
using Common = Contracts.Candidate;

namespace Candidates.Application.Contracts
{
    internal class Permanent : Common.CandidatePermanent
    {
        public static Common.CandidatePermanent? FromDomain(DomainValueObjects.Permanent? permanent)
        {
            if (permanent is null) return null;

            return new Common.CandidatePermanent
            {
                MonthlySalary = permanent.MonthlySalary,
            };
        }
    }
}
