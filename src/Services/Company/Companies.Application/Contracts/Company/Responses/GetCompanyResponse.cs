using Contracts.Shared;
using Contracts.Shared.Responses;
using Common = Contracts.Company.Responses;

namespace Companies.Application.Contracts.Company.Responses
{
    public class GetCompanyResponse : Common.GetCompanyResponse
    {
        public static Common.GetCompanyResponse FromDomain(Domain.Aggregates.CompanyAggregate.Company company)
        {
            return new Common.GetCompanyResponse
            {
                Id = company.Id,
                Name = company.Name,
                RegistrationNumber = company.RegistrationNumber,
                Status = company.Status,
                WebsiteUrl = company.WebsiteUrl,
                LinkedInUrl = company.LinkedInUrl,
                GlassdoorUrl = company.GlassdoorUrl,
                Logo = ImageResponse.FromDomain(company.Logo),
                Address = AddressWithLocation.FromDomain(company.Address),
                ContactPersons =  company.ContactPersons.Select(GetContactPersonResponse.FromDomain).ToList(),
                Industries = company.Industries.Select(Industry.FromDomain).ToList(),
            };
        }
    }
}
