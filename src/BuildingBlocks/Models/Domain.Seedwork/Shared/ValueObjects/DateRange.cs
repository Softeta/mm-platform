using Domain.Seedwork.Exceptions;

namespace Domain.Seedwork.Shared.ValueObjects
{
    public class DateRange : ValueObject<DateRange>
    {
        public static readonly DateRange MinValue = new(DateTimeOffset.MinValue, null);
        public static DateRange? FromDates(DateTimeOffset? from, DateTimeOffset? to)
        {
            if (from == null)
            {
                return null;
            }

            return new DateRange(from.Value, to);
        }

        public DateTimeOffset From { get; init; }

        public DateTimeOffset? To { get; init; }

        private DateRange() { }

        public DateRange(DateTimeOffset from, DateTimeOffset? to)
        {
            From = from;
            To = to;
            Validate();
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return From;
            yield return To;
        }

        private void Validate()
        {
            if (From == DateTimeOffset.MinValue || From == DateTimeOffset.MaxValue)
            {
                throw new DomainException($"From must be filled.", ErrorCodes.Shared.DateRange.FromRequired);
            }
        }
    }
}
