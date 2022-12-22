using DomainValueObject = Jobs.Domain.Aggregates.JobCandidatesAggregate.ValueObjects;

namespace Jobs.Application.IntegrationEventHandlers.Publishers.Payloads.Models.JobCandidates
{
    public class Company
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string? LogoUri { get; set; }

        public static Company FromDomain(DomainValueObject.Company company)
        {
            return new Company
            {
                Id = company.Id,
                Name = company.Name,
                LogoUri = company.LogoUri
            };
        }
    }
}
