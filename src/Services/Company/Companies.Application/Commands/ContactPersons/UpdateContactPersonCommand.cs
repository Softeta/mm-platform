using Companies.Domain.Aggregates.CompanyAggregate.Entities;
using Contracts.Shared.Services.Company.Requests;
using MediatR;

namespace Companies.Application.Commands.ContactPersons
{
    public record UpdateContactPersonCommand(
        Guid CompanyId,
        Guid ContactId,
        UpdatePersonRequest ContactPerson) : IRequest<ContactPerson>;
}
