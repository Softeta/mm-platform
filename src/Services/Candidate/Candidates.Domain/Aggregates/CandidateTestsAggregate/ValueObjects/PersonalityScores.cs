using Domain.Seedwork;

namespace Candidates.Domain.Aggregates.CandidateTestsAggregate.ValueObjects
{
    public class PersonalityScores : ValueObject<PersonalityScores>
    {
        public decimal A1 { get; init; }
        public decimal A2 { get; init; }
        public decimal W1 { get; init; }
        public decimal W2 { get; init; }
        public decimal R1 { get; init; }
        public decimal R2 { get; init; }
        public decimal S1 { get; init; }
        public decimal S2 { get; init; }
        public decimal Y1 { get; init; }
        public decimal Y2 { get; init; }
        public decimal SD { get; init; }
        public decimal AQ { get; init; }

        private PersonalityScores() { }

        public PersonalityScores(
            decimal a1,
            decimal a2,
            decimal w1,
            decimal w2,
            decimal r1,
            decimal r2,
            decimal s1,
            decimal s2,
            decimal y1,
            decimal y2,
            decimal sD,
            decimal aQ)
        {
            A1 = a1;
            A2 = a2;
            W1 = w1;
            W2 = w2;
            R1 = r1;
            R2 = r2;
            S1 = s1;
            S2 = s2;
            Y1 = y1;
            Y2 = y2;
            SD = sD;
            AQ = aQ;
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return A1;
            yield return A2;
            yield return W1;
            yield return W2;
            yield return R1; 
            yield return R2; 
            yield return S1;
            yield return S2;
            yield return Y1;
            yield return Y2;
            yield return SD;
            yield return AQ;
        }
    }
}
