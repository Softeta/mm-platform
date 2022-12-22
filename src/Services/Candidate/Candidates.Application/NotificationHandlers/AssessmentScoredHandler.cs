using Candidates.Application.NotificationHandlers.Events;
using Candidates.Domain.Aggregates.CandidateTestsAggregate;
using Candidates.Infrastructure.Clients.Talogy;
using Candidates.Infrastructure.Persistence.Repositories;
using Candidates.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Candidates.Application.NotificationHandlers
{
    public class AssessmentScoredHandler : BaseHandler, INotificationHandler
    {
        public const string AssessmentInstanceScored = "AssessmentInstanceScored";
        private readonly ICandidateTestsRepository _candidateTestsRepository;
        private readonly TalogySettings _talogySettings;

        public AssessmentScoredHandler(
            ITalogyApiClient talogyApiClient,
            ICandidateTestsRepository candidateTestsRepository,
            IOptions<TalogySettings> talogySettings) : base(talogyApiClient)
        {
            _candidateTestsRepository = candidateTestsRepository;
            _talogySettings = talogySettings.Value;
        }

        public string NotificationType => AssessmentInstanceScored;

        public async Task ExecuteAsync(Guid candidateId, Guid externalId, string notification)
        {
            var payload = JsonConvert.DeserializeObject<AssessmentInstanceScored>(notification);

            if (payload is null)
            {
                throw new ArgumentException("Failed to deserialize notification {Notification}", notification);
            }

            if (_talogySettings.PackageTypes.Logic == payload.PackageTypeId)
            {
                await ScoreLogicalTestAsync(candidateId, externalId, payload);
                return;
            }

            if (_talogySettings.PackageTypes.Personality == payload.PackageTypeId)
            {
                await ScorePersonalityTestAsync(candidateId, externalId, payload);
                return;
            }
        }

        private async Task ScoreLogicalTestAsync(Guid candidateId, Guid externalId, AssessmentInstanceScored payload)
        {
            var candidateTests = await _candidateTestsRepository.GetAsync(candidateId);

            if (candidateTests is null)
            {
                var packageInstance = await GetPackageInstanceAsync(payload.PackageInstanceId);

                if (packageInstance is null) return;

                candidateTests = new CandidateTests(candidateId, externalId, null);

                candidateTests.CreateLogicalAssessment(
                    payload.PackageInstanceId,
                    payload.PackageTypeId,
                    packageInstance.LogonUrl);

                candidateTests.ScoreLogicalAssessment(
                    payload.PackageInstanceId,
                    payload.PackageTypeId,
                    payload.AssessmentInstanceStartTime,
                    payload.AssessmentInstanceCompleteTime,
                    CollectLogicalScores(packageInstance));

                _candidateTestsRepository.Add(candidateTests);
            }
            else
            {
                candidateTests.ScoreLogicalAssessment(
                    payload.PackageInstanceId,
                    payload.PackageTypeId,
                    payload.AssessmentInstanceStartTime,
                    payload.AssessmentInstanceCompleteTime,
                    CollectLogicalScores(payload.AssessmentInstanceScores.FirstOrDefault()));

                _candidateTestsRepository.Update(candidateTests);
            }

            await _candidateTestsRepository.UnitOfWork.SaveEntitiesAsync<CandidateTests>();
        }

        private async Task ScorePersonalityTestAsync(Guid candidateId, Guid externalId, AssessmentInstanceScored payload)
        {
            var candidateTests = await _candidateTestsRepository.GetAsync(candidateId);

            if (candidateTests is null)
            {
                var packageInstance = await GetPackageInstanceAsync(payload.PackageInstanceId);

                if (packageInstance is null) return;

                candidateTests = new CandidateTests(candidateId, externalId, null);

                candidateTests.CreatePersonalityAssessment(
                    payload.PackageInstanceId,
                    payload.PackageTypeId,
                    packageInstance.LogonUrl);

                candidateTests.ScorePersonalityAssessment(
                    payload.PackageInstanceId,
                    payload.PackageTypeId,
                    payload.AssessmentInstanceStartTime,
                    payload.AssessmentInstanceCompleteTime,
                    CollectPersonalityScores(packageInstance));

                _candidateTestsRepository.Add(candidateTests);
            }
            else
            {
                candidateTests.ScorePersonalityAssessment(
                    payload.PackageInstanceId,
                    payload.PackageTypeId,
                    payload.AssessmentInstanceStartTime,
                    payload.AssessmentInstanceCompleteTime,
                    CollectPersonalityScores(payload.AssessmentInstanceScores.FirstOrDefault()));

                _candidateTestsRepository.Update(candidateTests);
            }

            await _candidateTestsRepository.UnitOfWork.SaveEntitiesAsync<CandidateTests>();
        }
    }
}
