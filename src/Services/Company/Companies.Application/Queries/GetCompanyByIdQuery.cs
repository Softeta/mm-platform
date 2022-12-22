using Companies.Domain.Aggregates.CompanyAggregate;
using MediatR;

namespace Companies.Application.Queries
{
    public record GetCompanyByIdQuery(Guid Id) : IRequest<Company?>;
}
