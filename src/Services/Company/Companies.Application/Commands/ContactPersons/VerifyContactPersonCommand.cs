using Companies.Domain.Aggregates.CompanyAggregate.Entities;
using MediatR;

namespace Companies.Application.Commands.ContactPersons
{
    public record VerifyContactPersonCommand(
        Guid CompanyId,
        Guid ContactId,
        Guid Key) : IRequest<ContactPerson>;
}
