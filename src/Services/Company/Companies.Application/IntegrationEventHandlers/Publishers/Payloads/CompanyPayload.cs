using Companies.Application.IntegrationEventHandlers.Publishers.Payloads.Models.Company;
using Companies.Application.IntegrationEventHandlers.Publishers.Payloads.Models.Shared;
using Companies.Domain.Aggregates.CompanyAggregate;
using Domain.Seedwork.Enums;

namespace Companies.Application.IntegrationEventHandlers.Publishers.Payloads
{
    internal class CompanyPayload
    {
        public Guid Id { get; set; }
        public CompanyStatus Status { get; set; }
        public string? Name { get; set; }
        public string? RegistrationNumber { get; set; }
        public string? LogoUri { get; set; }
        public Address? Address { get; set; }
        public string? WebsiteUrl { get; set; }
        public string? LinkedInUrl { get; set; }
        public string? GlassdoorUrl { get; set; }
        public Image? Logo { get; set; }
        public IEnumerable<ContactPersonPayload> ContactPersons { get; set; } = new List<ContactPersonPayload>();

        public static CompanyPayload FromDomain(Company company)
        {
            return new CompanyPayload
            {
                Id = company.Id,
                Status = company.Status,
                Name = company.Name,
                RegistrationNumber = company.RegistrationNumber,
                LogoUri = company.Logo?.ThumbnailUri,
                Address = Address.FromDomain(company.Address),
                WebsiteUrl = company.WebsiteUrl,
                LinkedInUrl = company.LinkedInUrl,
                GlassdoorUrl = company.GlassdoorUrl,
                Logo = Image.FromDomain(company.Logo),
                ContactPersons = company.ContactPersons.Select(x => ContactPersonPayload.FromDomain(company, x))
            };
        }
    }
}
