using Companies.Domain.Aggregates.CompanyAggregate;
using MediatR;

namespace Companies.Application.Queries
{
    public record GetCompanyByContactPersonExternaldQuery(Guid ExternalId) : IRequest<Company?>;
}
