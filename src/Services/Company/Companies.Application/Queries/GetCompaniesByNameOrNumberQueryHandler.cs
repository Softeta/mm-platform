using API.Customization.Authorization.Constants;
using Companies.Infrastructure.Persistence;
using Contracts.Company.Responses;
using Contracts.Company.Responses.ContactPersons;
using Contracts.Shared;
using Contracts.Shared.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Companies.Application.Queries
{
    public class GetCompaniesByNameOrNumberQueryHandler 
        : IRequestHandler<GetCompaniesByNameOrNumberQuery, GetCompaniesSearchResponse>
    {
        private readonly ICompanyContext _companyContext;

        public GetCompaniesByNameOrNumberQueryHandler(ICompanyContext companyContext)
        {
            _companyContext = companyContext;
        }

        public async Task<GetCompaniesSearchResponse> Handle(GetCompaniesByNameOrNumberQuery request, CancellationToken cancellationToken)
        {
            var companiesQuery = _companyContext.Companies
                .AsNoTracking()
                .Where(x => !string.IsNullOrWhiteSpace(x.Name))
                .AsQueryable();

            if (request.CompanyStatus.HasValue)
            {
                companiesQuery = _companyContext.Companies
                .Where(x => x.Status == request.CompanyStatus.Value);
            }

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                companiesQuery = companiesQuery.Where(x => 
                    (!string.IsNullOrEmpty(x.Name) && x.Name.Contains(request.Search.ToLower())) || 
                    (!string.IsNullOrEmpty(x.RegistrationNumber) && x.RegistrationNumber.Contains(request.Search.ToLower())));
            }

            var count = await companiesQuery.CountAsync(cancellationToken);

            var companies = await companiesQuery
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new GetCompanySearchResponse
                {
                    Id = x.Id,
                    Name = x.Name!,
                    RegistrationNumber = x.RegistrationNumber!,
                    Logo = x.Logo != null
                    ? new ImageResponse
                    {
                        Uri = x.Logo.ThumbnailUri
                    }
                    : null,
                    Address = x.Address != null 
                    ? new AddressWithLocation
                    {
                        AddressLine = x.Address.AddressLine,
                        City = x.Address.City,
                        Country = x.Address.Country,
                        Location = x.Address.Location,
                        PostalCode = x.Address.PostalCode,
                        Longitude = x.Address.Coordinates != null ? x.Address.Coordinates.Longitude : null,
                        Latitude = x.Address.Coordinates != null ? x.Address.Coordinates.Latitude : null
                    }
                    : null,
                    ContactPersons = request.Scope == CustomScopes.BackOffice.User 
                    ? x.ContactPersons.Select(c => new GetContactPersonResponse
                    {
                        Id = c.Id,
                        CompanyId = c.CompanyId,
                        FirstName = c.FirstName,
                        LastName = c.LastName,
                        Email = c.Email.Address,
                        Position = c.Position != null
                        ? new Position
                        {
                            Id = c.Position.Id,
                            Code = c.Position.Code,
                        }
                        : null,
                        Phone = c.Phone != null
                        ? new PhoneFullResponse
                        {
                            CountryCode = c.Phone.CountryCode,
                            Number = c.Phone.Number,
                            PhoneNumber = c.Phone.PhoneNumber,
                        }
                        : null,
                        Picture = c.Picture != null
                        ? new ImageResponse
                        {
                            Uri = c.Picture.ThumbnailUri
                        }
                        : null,
                    }).ToList()
                    : new()
                }).ToListAsync(cancellationToken);

            return new GetCompaniesSearchResponse(count, companies, count > 0);
        }
    }
}
