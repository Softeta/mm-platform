using API.Customization.Extensions;
using Companies.Domain.Aggregates.CompanyAggregate;
using Companies.Infrastructure.Persistence;
using Contracts.Company.Responses;
using Contracts.Shared;
using Contracts.Shared.Responses;
using Domain.Seedwork.Consts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TopologyPoint = NetTopologySuite.Geometries.Point;

namespace Companies.Application.Queries
{
    public class GetCompaniesQueryHandler
        : IRequestHandler<GetCompaniesQuery, GetCompaniesResponse>
    {
        private readonly ICompanyContext _companyContext;

        public GetCompaniesQueryHandler(ICompanyContext companyContext)
        {
            _companyContext = companyContext;
        }

        public async Task<GetCompaniesResponse> Handle(GetCompaniesQuery request, CancellationToken cancellationToken)
        {
            var radiusInMeters = request.RadiusInKm.ToMeters();
            TopologyPoint? requestCoordinates = null;

            if (request.Latitude.HasValue && request.Longitude.HasValue && request.RadiusInKm.HasValue)
            {
                requestCoordinates = new TopologyPoint(
                    request.Longitude.Value,
                    request.Latitude.Value)
                { SRID = SpatialReferenceIds.Gps };
            }

            Expression<Func<Company, bool>> filterByStatuses = company =>
                request.CompanyStatuses == null || request.CompanyStatuses.Contains(company.Status);

            Expression<Func<Company, bool>> filterByLocation = company =>
                requestCoordinates == null || (
                company.Address != null &&
                company.Address.Coordinates != null &&
                company.Address.Coordinates.Point.Distance(requestCoordinates) <= radiusInMeters);

            Expression<Func<Company, bool>> filterByIndustries = company =>
                request.Industries == null || company.Industries.Any(i => request.Industries.Contains(i.IndustryId));

            Expression<Func<Company, bool>> filterByName = company =>
                string.IsNullOrWhiteSpace(request.Search) || 
                (!string.IsNullOrWhiteSpace(company.Name) && company.Name.ToLower().Contains(request.Search.ToLower()));

            Expression<Func<Company, bool>> filterByCompanies = company =>
                request.Companies == null || request.Companies.Contains(company.Id);

            var companiesQuery = _companyContext.Companies
                .AsNoTracking()
                .Where(x => !string.IsNullOrWhiteSpace(x.Name))
                .Where(filterByStatuses)
                .Where(filterByName)
                .Where(filterByLocation)
                .Where(filterByIndustries)
                .Where(filterByCompanies)
                .AsQueryable();

            var count = await companiesQuery.CountAsync(cancellationToken);

            var companies = await companiesQuery
                .OrderByDescending(x => x.CreatedAt)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new GetCompanyBriefResponse
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
                    Industries = x.Industries
                        .Select(x => new Industry 
                        { 
                            Id = x.Id,
                            Code = x.Code 
                        }),
                    CreatedAt = x.CreatedAt
                })
                .ToListAsync(cancellationToken);

            return new GetCompaniesResponse(count, companies);
        }
    }
}
