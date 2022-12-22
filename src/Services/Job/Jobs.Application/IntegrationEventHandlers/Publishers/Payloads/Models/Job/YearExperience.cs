using DomainValueObject = Jobs.Domain.Aggregates.JobAggregate.ValueObjects;

namespace Jobs.Application.IntegrationEventHandlers.Publishers.Payloads.Models.Job
{
    public class YearExperience
    {
        public int? From { get; set; }
        public int? To { get; set; }

        public static YearExperience? FromDomain(DomainValueObject.YearExperience? yearExperience)
        {
            if (yearExperience is null) return null;

            return new YearExperience
            {
                From = yearExperience.From,
                To = yearExperience.To
            };
        }
    }
}
