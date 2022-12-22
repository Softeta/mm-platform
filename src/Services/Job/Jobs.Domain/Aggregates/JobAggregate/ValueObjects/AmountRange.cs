using Domain.Seedwork;

namespace Jobs.Domain.Aggregates.JobAggregate.ValueObjects
{
    public class AmountRange : ValueObject<AmountRange>
    {
        public decimal From { get; init; }
        public decimal To { get; init; }

        public AmountRange(decimal from, decimal to)
        {
            From = from;
            To = to;
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return From;
            yield return To;
        }
    }
}
