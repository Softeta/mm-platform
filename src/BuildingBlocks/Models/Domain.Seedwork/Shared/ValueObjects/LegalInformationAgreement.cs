namespace Domain.Seedwork.Shared.ValueObjects
{
    public class LegalInformationAgreement : ValueObject<LegalInformationAgreement>
    {
        public bool Agreed { get; init; }
        public DateTimeOffset ModifiedAt { get; init; }

        public LegalInformationAgreement(bool agreed, DateTimeOffset modifiedAt)
        {
            Agreed = agreed;
            ModifiedAt = modifiedAt;
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Agreed;
            yield return ModifiedAt;
        }
    }
}
