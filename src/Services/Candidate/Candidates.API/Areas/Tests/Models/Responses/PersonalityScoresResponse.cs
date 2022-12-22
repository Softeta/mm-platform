using Candidates.Domain.Aggregates.CandidateTestsAggregate.ValueObjects;

namespace Candidates.API.Areas.Tests.Models.Responses
{
    public class PersonalityScoresResponse
    {
        public decimal A1 { get; set; }
        public decimal A2 { get; set; }
        public decimal W1 { get; set; }
        public decimal W2 { get; set; }
        public decimal R1 { get; set; }
        public decimal R2 { get; set; }
        public decimal S1 { get; set; }
        public decimal S2 { get; set; }
        public decimal Y1 { get; set; }
        public decimal Y2 { get; set; }
        public decimal SD { get; set; }
        public decimal AQ { get; set; }

        public static PersonalityScoresResponse? FromDomain(PersonalityScores? scores)
        {
            if (scores is null) return null;

            return new PersonalityScoresResponse
            {
                A1 = scores.A1,
                A2 = scores.A2,
                W1 = scores.W1,
                W2 = scores.W2,
                R1 = scores.R1,
                R2 = scores.R2,
                S1 = scores.S1,
                S2 = scores.S2,
                Y1 = scores.Y1,
                Y2 = scores.Y2,
                SD = scores.SD,
                AQ = scores.AQ,
            };
        }
    }
}
