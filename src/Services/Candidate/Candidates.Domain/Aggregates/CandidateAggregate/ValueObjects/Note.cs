using Domain.Seedwork;

namespace Candidates.Domain.Aggregates.CandidateAggregate.ValueObjects
{
    public class Note : ValueObject<Note>
    {
        public string Value { get; init; } = null!;
        public DateTimeOffset? EndDate { get; init; }


        private Note(string value, DateTimeOffset? endDate)
        {
            Value = value;
            EndDate = endDate;
        }

        public static Note? Create(string? value, DateTimeOffset? endDate)
        {
            if (value is null) return null;

            return new Note(value, endDate);
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value;
            yield return EndDate;
        }
    }
}
