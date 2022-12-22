using Companies.Domain.Aggregates.CompanyAggregate.Entities;
using Contracts.Shared.Services.Company.Requests;
using Domain.Seedwork.Enums;
using MediatR;

namespace Companies.Application.Commands.ContactPersons
{
    public record AddCompanyContactPersonCommand(
        Guid CompanyId,
        CreatePersonRequest ContactPerson,
        Guid CreatedById,
        Scope CreatedByScope) : IRequest<ContactPerson>;
}
