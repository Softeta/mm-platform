namespace Candidates.Infrastructure.Clients.Talogy.Models.Responses.PackageInstance
{
    public class AssessmentInstanceScore
    {
        public string AssessmentTypeScoringId { get; set; } = null!;
        public decimal? TScore { get; set; }
        public decimal? Percentile { get; set; }
        public decimal? Sten { get; set; }
        public decimal? Raw { get; set; }
        public Dictionary<string, decimal>? DetailedScores { get; set; }
    }

    public static class ScoresExtension
    {
        public static decimal TryGetScore(this Dictionary<string, decimal> scores, string key)
        {
            if (scores.TryGetValue(key, out decimal value))
            {
                return value;
            }

            throw new ArgumentException($"Can't retrieve score {key} from {scores}");
        }
    }

    public static class LgiKeys
    {
        public const string Speed = "Speed-Percentile";
        public const string Accuracy = "Accuracy-Percentile";
        public const string Verbal = "Verbal-Percentile";
        public const string Numerical = "Numerical-Percentile";
        public const string Abstract = "Abstract-Percentile";
    }

    public static class PapiKeys
    {
        public const string A1 = "A1-Sten";
        public const string A2 = "A2-Sten";

        public const string W1 = "W1-Sten";
        public const string W2 = "W2-Sten";

        public const string R1 = "R1-Sten";
        public const string R2 = "R2-Sten";

        public const string S1 = "S1-Sten";
        public const string S2 = "S2-Sten";

        public const string Y1 = "Y1-Sten";
        public const string Y2 = "Y2-Sten";

        public const string SD = "SD-Sten";
        public const string AQ = "AQ-Sten";
    }
}
