using Contracts.Shared;
using Contracts.Shared.Responses;
using Common = Contracts.Company.Responses.ContactPersons;
using DomainEntity = Companies.Domain.Aggregates.CompanyAggregate.Entities;

namespace Companies.Application.Contracts.Company.Responses
{
    public class GetContactPersonResponse : Common.GetContactPersonResponse
    {
        public static Common.GetContactPersonResponse FromDomain(DomainEntity.ContactPerson person)
        {
            return new Common.GetContactPersonResponse
            {
                Id = person.Id,
                CompanyId = person.CompanyId,
                Email = person.Email.Address,
                IsEmailVerified = person.Email.IsVerified,
                Stage = person.Stage,
                Role = person.Role,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Position = Position.FromDomain(person.Position),
                Phone = PhoneFullResponse.FromDomain(person.Phone),
                Picture = ImageResponse.FromDomain(person.Picture),
                SystemLanguage = person.SystemLanguage,
                TermsAndConditions = LegalInformationAgreement.FromDomain(person.TermsAndConditions),
                MarketingAcknowledgement = LegalInformationAgreement.FromDomain(person.MarketingAcknowledgement),
                ExternalId = person.ExternalId
            };
        }
    }
}
