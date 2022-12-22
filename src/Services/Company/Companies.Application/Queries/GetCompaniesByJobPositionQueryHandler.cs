using Companies.Domain.Aggregates.CompanyAggregate.Entities;
using Companies.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Companies.Application.Queries
{
    public class GetCompaniesByJobPositionQueryHandler : IRequestHandler<GetCompaniesByJobPositionQuery, List<Guid>>
    {
        private readonly ICompanyContext _context;

        public GetCompaniesByJobPositionQueryHandler(ICompanyContext context)
        {
            _context = context;
        }

        public async Task<List<Guid>> Handle(GetCompaniesByJobPositionQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<ContactPerson, bool>> findByJobPosition = contactPerson =>
                contactPerson.Position != null && contactPerson.Position.Id == request.Id;

            return await _context.ContactPersons
                .Where(findByJobPosition)
                .Select(x => x.CompanyId)
                .ToListAsync();
        }
    }
}
