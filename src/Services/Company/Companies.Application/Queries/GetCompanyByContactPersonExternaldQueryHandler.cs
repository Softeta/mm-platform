using Companies.Domain.Aggregates.CompanyAggregate;
using Companies.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Companies.Application.Queries
{
    public class GetCompanyByContactPersonExternaldQueryHandler : IRequestHandler<GetCompanyByContactPersonExternaldQuery, Company?>
    {
        private readonly CompanyContext _companyContext;

        public GetCompanyByContactPersonExternaldQueryHandler(CompanyContext companyContext)
        {
            _companyContext = companyContext;
        }

        public async Task<Company?> Handle(GetCompanyByContactPersonExternaldQuery request, CancellationToken cancellationToken)
        {
            return await _companyContext.Companies
                .Include(x => x.ContactPersons)
                .Include(c => c.Industries)
                .AsSplitQuery()
                .SingleOrDefaultAsync(x => x.ContactPersons.Any(c => c.ExternalId == request.ExternalId), cancellationToken);
        }
    }
}
