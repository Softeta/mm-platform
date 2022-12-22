using Companies.Domain.Aggregates.CompanyAggregate;
using MediatR;

namespace Companies.Application.Queries
{
    public record GetCompanyByContactPersonEmailQuery(string Email) : IRequest<Company?>;
}
