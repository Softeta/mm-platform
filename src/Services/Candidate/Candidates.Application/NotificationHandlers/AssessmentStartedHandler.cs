using Candidates.Application.NotificationHandlers.Events;
using Candidates.Domain.Aggregates.CandidateTestsAggregate;
using Candidates.Infrastructure.Persistence.Repositories;
using Candidates.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Candidates.Application.NotificationHandlers
{
    public class AssessmentStartedHandler : INotificationHandler
    {
        public const string AssessmentInstanceStarted = "AssessmentInstanceStarted";
        private readonly ICandidateTestsRepository _candidateTestsRepository;
        private readonly TalogySettings _talogySettings;

        public AssessmentStartedHandler(
            ICandidateTestsRepository candidateTestsRepository,
            IOptions<TalogySettings> talogySettings)
        {
            _candidateTestsRepository = candidateTestsRepository;
            _talogySettings = talogySettings.Value;
        }

        public string NotificationType => AssessmentInstanceStarted;

        public async Task ExecuteAsync(Guid candidateId, Guid externalId, string notification)
        {
            var payload = JsonConvert.DeserializeObject<AssessmentInstanceStarted>(notification);

            if (payload is null)
            {
                throw new ArgumentException($"Failed to deserialize notification: {notification} ExternalId: {externalId}");
            }

            if (_talogySettings.PackageTypes.Logic == payload.PackageTypeId)
            {
                await StartLogicalTestAsync(candidateId, payload);
                return;
            }

            if (_talogySettings.PackageTypes.Personality == payload.PackageTypeId)
            {
                await StartPersonalityTestAsync(candidateId, payload);
                return;
            }
        }

        private async Task StartLogicalTestAsync(Guid candidateId, AssessmentInstanceStarted payload)
        {
            var candidateTests = await _candidateTestsRepository.GetAsync(candidateId);

            if (candidateTests is null) return;

            candidateTests.StartLogicalAssessment(
                payload.PackageInstanceId,
                payload.AssessmentTypeId,
                payload.AssessmentInstanceStartTime);

            _candidateTestsRepository.Update(candidateTests);
            await _candidateTestsRepository.UnitOfWork.SaveEntitiesAsync<CandidateTests>();
        }

        private async Task StartPersonalityTestAsync(Guid candidateId, AssessmentInstanceStarted payload)
        {
            var candidateTests = await _candidateTestsRepository.GetAsync(candidateId);

            if (candidateTests is null) return;

            candidateTests.StartPersonalityAssessment(
                payload.PackageInstanceId,
                payload.AssessmentTypeId,
                payload.AssessmentInstanceStartTime);

            _candidateTestsRepository.Update(candidateTests);
            await _candidateTestsRepository.UnitOfWork.SaveEntitiesAsync<CandidateTests>();
        }
    }
}
