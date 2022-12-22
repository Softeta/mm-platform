using Companies.Domain.Aggregates.CompanyAggregate.Entities;
using MediatR;

namespace Companies.Application.Commands.ContactPersons
{
    public record UpdateContactPersonLegalTermsCommand(
        Guid CompanyId,
        Guid ContactId,
        bool TermsAgreement,
        bool MarketingAgreement) : IRequest<ContactPerson>;
}
