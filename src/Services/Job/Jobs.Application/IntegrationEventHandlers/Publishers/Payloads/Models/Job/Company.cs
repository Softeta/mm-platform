using Domain.Seedwork.Enums;
using DomainValueObject = Jobs.Domain.Aggregates.JobAggregate.ValueObjects;

namespace Jobs.Application.IntegrationEventHandlers.Publishers.Payloads.Models.Job
{
    public class Company
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public string? LogoUri { get; set; }

        public Address? Address { get; set; }

        public CompanyStatus Status { get; set; }

        public IEnumerable<ContactPerson> ContactPersons { get; set; } = new List<ContactPerson>();

        public static Company FromDomain(DomainValueObject.Company company)
        {
            return new Company
            {
                Id = company.Id,
                Name = company.Name,
                Description = company.Description,
                LogoUri = company.LogoUri,
                Address = Address.FromAddress(company.Address),
                Status = company.Status,
                ContactPersons = company.ContactPersons.Select(ContactPerson.FromDomain).ToList()
            };
        }
    }
}
