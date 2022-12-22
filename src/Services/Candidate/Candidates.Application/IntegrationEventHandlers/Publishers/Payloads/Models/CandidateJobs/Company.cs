using DomainValueObjects = Candidates.Domain.Aggregates.CandidateJobsAggregate.ValueObjects;

namespace Candidates.Application.IntegrationEventHandlers.Publishers.Payloads.Models.CandidateJobs
{
    internal class Company
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;

        public static Company FromDomain(DomainValueObjects.Company company)
        {
            return new Company
            {
                Id = company.Id,
                Name = company.Name
            };
        }
    }
}
