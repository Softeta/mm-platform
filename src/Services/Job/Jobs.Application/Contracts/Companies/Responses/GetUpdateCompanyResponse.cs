using Contracts.Job.Companies.Responses;
using Contracts.Shared;
using Contracts.Shared.Responses;
using Common = Contracts.Job.Companies.Responses;
using ValueObjects = Jobs.Domain.Aggregates.JobAggregate.ValueObjects;

namespace Jobs.Application.Contracts.Jobs.Responses
{
    public class GetUpdateCompanyResponse : Common.GetUpdateCompanyResponse
    {
        public static Common.GetUpdateCompanyResponse FromDomain(Guid jobId, ValueObjects.Company company)
        {
            return new Common.GetUpdateCompanyResponse
            {
                JobId = jobId,
                Company = new CompanyResponse
                {
                    Id = company.Id,
                    Name = company.Name,
                    Address = company.Address != null ? new Address
                    {
                        AddressLine = company.Address.AddressLine,
                        City = company.Address.City,
                        Country = company.Address.Country,
                        PostalCode = company.Address.PostalCode,
                        Longitude = company.Address.Coordinates?.Longitude,
                        Latitude = company.Address.Coordinates?.Latitude
                    } : null,
                    Description = company.Description,
                    Logo = !string.IsNullOrEmpty(company.LogoUri)
                    ? new ImageResponse
                    {
                        Uri = company.LogoUri
                    }
                    : null,
                    ContactPersons = company.ContactPersons.Select(p => new JobContactPersonResponse()
                    {
                        Id = p.PersonId,
                        IsMainContact = p.IsMainContact,
                        FirstName = p.FirstName,
                        LastName = p.LastName,
                        Position = Position.FromDomain(p.Position),
                        Email = p.Email,
                        PhoneNumber = p.PhoneNumber,
                        Picture = !string.IsNullOrEmpty(p.PictureUri)
                        ? new ImageResponse
                        {
                            Uri = p.PictureUri
                        }
                        : null,
                    }),
                },
            };
        }
    }
}
