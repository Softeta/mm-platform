using Contracts.Shared;
using Domain.Seedwork.Enums;

namespace Contracts.Company.Responses.ContactPersons
{
    public class GetContactPersonResponse : GetContactPersonBase
    {
        public Guid CompanyId { get; set; }

        public bool IsEmailVerified { get; set; }

        public ContactPersonStage Stage { get; set; }

        public ContactPersonRole Role { get; set; }

        public LegalInformationAgreement? TermsAndConditions { get; set; }

        public LegalInformationAgreement? MarketingAcknowledgement { get; set; }
    }
}
