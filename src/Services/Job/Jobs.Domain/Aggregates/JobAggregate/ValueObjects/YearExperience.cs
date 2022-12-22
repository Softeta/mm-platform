using Domain.Seedwork;

namespace Jobs.Domain.Aggregates.JobAggregate.ValueObjects
{
    public class YearExperience : ValueObject<YearExperience>
    {
        public int? From { get; init; }
        public int? To { get; init; }

        public YearExperience(int? from, int? to)
        {
            From = from;
            To = to;
        }

        public static YearExperience? Create(int? from, int? to)
        {
            if (from is null && to is null) return null;

            return new YearExperience(from, to);
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return From;
            yield return To;
        }
    }
}
