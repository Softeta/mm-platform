using Companies.Domain.Aggregates.CompanyAggregate.Entities;
using Domain.Seedwork.Enums;
using MediatR;

namespace Companies.Application.Commands.ContactPersons
{
    public record RegisterMyselfCommand(
        string Email,
        Guid ExternalId,
        SystemLanguage? SystemLanguage,
        bool AcceptTermsAndConditions,
        bool AcceptMarketingAcknowledgement
    ) : IRequest<ContactPerson>;
}
