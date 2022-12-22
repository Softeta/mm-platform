using Companies.Domain.Aggregates.CompanyAggregate;
using Companies.Infrastructure.Persistence;
using Domain.Seedwork.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Companies.Application.Queries
{
    public class GetCompanyByIdQueryHandler : IRequestHandler<GetCompanyByIdQuery, Company?>
    {
        private readonly ICompanyContext _companyContext;

        public GetCompanyByIdQueryHandler(
            ICompanyContext companyContext)
        {
            _companyContext = companyContext;
        }

        public async Task<Company?> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
        {
            return await _companyContext.Companies
                .Include(x => x.ContactPersons
                    .Where(x => x.Stage != ContactPersonStage.Rejected))
                .Include(c => c.Industries)
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        }
    }
}
