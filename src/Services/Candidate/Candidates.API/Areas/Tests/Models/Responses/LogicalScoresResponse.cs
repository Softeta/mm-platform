using Candidates.Domain.Aggregates.CandidateTestsAggregate.ValueObjects;

namespace Candidates.API.Areas.Tests.Models.Responses
{
    public class LogicalScoresResponse
    {
        public decimal Total { get; set; }
        public decimal Speed { get; set; }
        public decimal Accuracy { get; set; }
        public decimal Verbal { get; set; }
        public decimal Numerical { get; set; }
        public decimal Abstract { get; set; }

        public static LogicalScoresResponse? FromDomain(LogicScores? scores)
        {
            if (scores is null) return null;

            return new LogicalScoresResponse
            {
                Total = scores.Total,
                Speed = scores.Speed,
                Accuracy = scores.Accuracy,
                Verbal = scores.Verbal,
                Numerical = scores.Numerical,
                Abstract = scores.Abstract
            };
        }
    }
}
