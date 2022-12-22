using ValueObjects = Domain.Seedwork.Shared.ValueObjects;

namespace Contracts.Shared
{
    public class LegalInformationAgreement
    {
        public bool Agreed { get; set; }
        public DateTimeOffset ModifiedAt { get; set; }

        public static LegalInformationAgreement? FromDomain(ValueObjects.LegalInformationAgreement? agreement)
        {
            if (agreement is null) return null;

            return new LegalInformationAgreement
            {
                Agreed = agreement.Agreed,
                ModifiedAt = agreement.ModifiedAt,
            };
        }
    }
}
