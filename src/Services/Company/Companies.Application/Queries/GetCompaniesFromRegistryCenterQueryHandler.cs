using Companies.Infrastructure.Persistence;
using Contracts.Company.Responses;
using Contracts.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Companies.Application.Queries
{
    public class GetCompaniesFromRegistryCenterQueryHandler
        : IRequestHandler<GetCompaniesFromRegistryCenterQuery, GetCompaniesSearchResponse>
    {
        private readonly ICompanyContext _companyContext;

        public GetCompaniesFromRegistryCenterQueryHandler(ICompanyContext companyContext)
        {
            _companyContext = companyContext;
        }

        public async Task<GetCompaniesSearchResponse> Handle(GetCompaniesFromRegistryCenterQuery request, CancellationToken cancellationToken)
        {
            var companiesQuery = _companyContext.RegistryCenterCompanies
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                companiesQuery = companiesQuery.Where(x =>
                    x.RegistrationNumber == request.Search ||
                    x.SearchIndexes.Any(x => x.Index == request.Search) ||
                    x.Name.StartsWith(request.Search));
            }

            var count = await companiesQuery.CountAsync(cancellationToken);

            var companies = await companiesQuery
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new GetCompanySearchResponse
                {
                    RegistrationNumber = x.RegistrationNumber,
                    Name = x.Name,
                    Address = new AddressWithLocation
                    {
                        AddressLine = x.AddressLine,
                        City = x.City,
                        Country = x.Country,
                        PostalCode = x.ZipCode ?? ""
                    }
                }).ToListAsync(cancellationToken);

            return new GetCompaniesSearchResponse(count, companies, false);
        }
    }
}
