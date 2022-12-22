using Companies.Domain.Aggregates.CompanyAggregate.Entities;
using Domain.Seedwork.Enums;
using MediatR;

namespace Companies.Application.Commands.ContactPersons
{
    public record UpdateContactPersonSettingsCommand(
        Guid CompanyId,
        Guid ContactId,
        SystemLanguage SystemLanguage,
        bool MarketingAcknowledgement) : IRequest<ContactPerson>;
}
