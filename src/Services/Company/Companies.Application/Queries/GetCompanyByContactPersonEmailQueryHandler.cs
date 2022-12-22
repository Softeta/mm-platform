using Companies.Domain.Aggregates.CompanyAggregate;
using Companies.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Companies.Application.Queries
{
    public class GetCompanyByContactPersonEmailQueryHandler : IRequestHandler<GetCompanyByContactPersonEmailQuery, Company?>
    {
        private readonly CompanyContext _companyContext;

        public GetCompanyByContactPersonEmailQueryHandler(CompanyContext companyContext)
        {
            _companyContext = companyContext;
        }

        public async Task<Company?> Handle(GetCompanyByContactPersonEmailQuery request, CancellationToken cancellationToken)
        {
            return await _companyContext.Companies
                .Include(x => x.ContactPersons)
                .Include(c => c.Industries)
                .AsSplitQuery()
                .SingleOrDefaultAsync(x => x.ContactPersons.Any(c => c.Email.Address == request.Email), cancellationToken);
        }
    }
}
