using Domain.Seedwork;

namespace Candidates.Domain.Aggregates.CandidateJobsAggregate.ValueObjects
{
    public class Company : ValueObject<Company>
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string? LogoUri { get; init; }

        public Company(Guid id, string name, string? logoUri)
        {
            Id = id;
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
