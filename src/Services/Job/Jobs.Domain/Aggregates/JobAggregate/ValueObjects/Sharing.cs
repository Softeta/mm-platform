using Domain.Seedwork;

namespace Jobs.Domain.Aggregates.JobAggregate.ValueObjects
{
    public class Sharing : ValueObject<Sharing>
    {
        public Guid Key { get; init; }
        public DateTimeOffset Date { get; init; }

        public Sharing()
        {
            Key = Guid.NewGuid();
            Date = DateTimeOffset.UtcNow;
        }

        public Sharing(Guid id)
        {
            Key = id;
            Date = DateTimeOffset.UtcNow;
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Key;
            yield return Date;
        }
    }
}
