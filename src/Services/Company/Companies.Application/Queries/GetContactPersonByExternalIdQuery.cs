using Companies.Domain.Aggregates.CompanyAggregate.Entities;
using MediatR;

namespace Companies.Application.Queries
{
    public record GetContactPersonByExternalIdQuery(Guid ExternalId) : IRequest<ContactPerson?>;
}
