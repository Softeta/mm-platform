using Companies.Domain.Aggregates.CompanyAggregate.Entities;
using Companies.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Companies.Application.Queries
{
    public class GetContactPersonByExternalIdQueryHandler : IRequestHandler<GetContactPersonByExternalIdQuery, ContactPerson?>
    {
        private readonly ICompanyContext _companyContext;

        public GetContactPersonByExternalIdQueryHandler(ICompanyContext companyContext)
        {
            _companyContext = companyContext;
        }

        public async Task<ContactPerson?> Handle(GetContactPersonByExternalIdQuery request, CancellationToken cancellationToken)
        {
            return await _companyContext.ContactPersons
                .AsNoTracking()
                .SingleOrDefaultAsync(
                    x => x.ExternalId.HasValue &&
                    x.ExternalId.Value == request.ExternalId, cancellationToken);
        }
    }
}
