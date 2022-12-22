using Candidates.Domain.Aggregates.CandidateTestsAggregate.ValueObjects;
using Candidates.Infrastructure.Clients.Talogy;
using Candidates.Infrastructure.Clients.Talogy.Constants;
using Candidates.Infrastructure.Clients.Talogy.Models.Responses;
using Candidates.Infrastructure.Clients.Talogy.Models.Responses.PackageInstance;

namespace Candidates.Application.NotificationHandlers
{
    public class BaseHandler
    {
        protected ITalogyApiClient TalogyApiClient { get; }

        public BaseHandler(ITalogyApiClient talogyApiClient)
        {
            TalogyApiClient = talogyApiClient;
        }

        protected async Task<GetPackageInstance?> GetPackageInstanceAsync(string packageInstanceId)
        {
            return await TalogyApiClient.GetAsync<GetPackageInstance>($"{Endpoints.PackageInstances}/{packageInstanceId}");
        }

        protected async Task<Stream?> GetReportAsync(string reportInstanceId, string accept)
        {
            return await TalogyApiClient.GetFileAsync($"{Endpoints.ReportInstances}/{reportInstanceId}", accept);
        }

        protected static LogicScores? CollectLogicalScores(GetPackageInstance packageInstance)
        {
            var assessmentInstanceScore = packageInstance.AssessmentInstances
                    .FirstOrDefault()?
                    .AssessmentInstanceScores?
                    .FirstOrDefault();

            return CollectLogicalScores(assessmentInstanceScore);
        }

        protected static LogicScores? CollectLogicalScores(AssessmentInstanceScore? assessmentInstanceScore)
        {
            var scores = assessmentInstanceScore?
                    .DetailedScores;

            var total = assessmentInstanceScore?
                .Percentile;

            if (!total.HasValue || 
                scores is null || 
                scores.Count < 6)
            {
                return null;
            }

            return new LogicScores(
                total.Value,
                scores.TryGetScore(LgiKeys.Speed),
                scores.TryGetScore(LgiKeys.Accuracy),
                scores.TryGetScore(LgiKeys.Verbal),
                scores.TryGetScore(LgiKeys.Numerical),
                scores.TryGetScore(LgiKeys.Abstract));
        }

        protected static PersonalityScores? CollectPersonalityScores(GetPackageInstance packageInstance)
        {
            var assessmentInstanceScores = packageInstance.AssessmentInstances
                    .FirstOrDefault()?
                    .AssessmentInstanceScores?
                    .FirstOrDefault();

            return CollectPersonalityScores(assessmentInstanceScores);
        }

        protected static PersonalityScores? CollectPersonalityScores(AssessmentInstanceScore? assessmentInstanceScore)
        {
            var scores = assessmentInstanceScore?
                    .DetailedScores;

            if (scores is null || scores.Count < 12)
            {
                return null;
            }

            return new PersonalityScores(
                scores.TryGetScore(PapiKeys.A1),
                scores.TryGetScore(PapiKeys.A2),
                scores.TryGetScore(PapiKeys.W1),
                scores.TryGetScore(PapiKeys.W2),
                scores.TryGetScore(PapiKeys.R1),
                scores.TryGetScore(PapiKeys.R2),
                scores.TryGetScore(PapiKeys.S1),
                scores.TryGetScore(PapiKeys.S2),
                scores.TryGetScore(PapiKeys.Y1),
                scores.TryGetScore(PapiKeys.Y2),
                scores.TryGetScore(PapiKeys.SD),
                scores.TryGetScore(PapiKeys.AQ));
        }
    }
}
