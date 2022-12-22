using DomainValueObjects = Jobs.Domain.Aggregates.JobAggregate.ValueObjects;
using Common = Contracts.Job;

namespace Jobs.Application.Contracts
{
    internal class YearExperience : Common.YearExperience
    {
        public static Common.YearExperience? FromDomain(DomainValueObjects.YearExperience? yearExperience)
        {
            if (yearExperience is null) return null;

            return new Common.YearExperience
            {
                From = yearExperience.From,
                To = yearExperience.To
            };
        }
    }
}
