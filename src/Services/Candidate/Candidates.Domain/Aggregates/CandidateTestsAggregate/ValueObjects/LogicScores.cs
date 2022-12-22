using Domain.Seedwork;

namespace Candidates.Domain.Aggregates.CandidateTestsAggregate.ValueObjects
{
    public class LogicScores : ValueObject<PersonalityScores>
    {
        public decimal Total { get; init; }
        public decimal Speed { get; init; }
        public decimal Accuracy { get; init; }
        public decimal Verbal { get; init; }
        public decimal Numerical { get; init; }
        public decimal Abstract { get; init; }

        public LogicScores(
            decimal total,
            decimal speed,
            decimal accuracy,
            decimal verbal,
            decimal numerical,
            decimal @abstract)
        {

            Total = total;
            Speed = speed;
            Accuracy = accuracy;
            Verbal = verbal;
            Numerical = numerical;
            Abstract = @abstract;
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Total;
            yield return Speed;
            yield return Accuracy;
            yield return Verbal;
            yield return Numerical;
            yield return Abstract;
        }
    }
}
