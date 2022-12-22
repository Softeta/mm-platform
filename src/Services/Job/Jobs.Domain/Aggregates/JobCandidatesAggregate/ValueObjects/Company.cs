using Domain.Seedwork;

namespace Jobs.Domain.Aggregates.JobCandidatesAggregate.ValueObjects
{
    public class Company : ValueObject<Company>
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = null!;
        public string? LogoUri { get; init; }

        private Company() { }

        public Company(
            Guid companyId,
            string name,
            string? logoUri)
        {
            Id = companyId;
            Name = name;
            LogoUri = logoUri;
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Id;
            yield return Name;
            yield return LogoUri;
        }
    }
}
