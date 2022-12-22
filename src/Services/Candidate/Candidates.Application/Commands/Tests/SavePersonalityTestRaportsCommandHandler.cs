using Candidates.Application.NotificationHandlers.Events;
using Candidates.Domain.Aggregates.CandidateTestsAggregate;
using Candidates.Infrastructure.Clients.Talogy;
using Candidates.Infrastructure.Persistence.Repositories;
using Candidates.Infrastructure.Settings;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Persistence.Customization.FileStorage;
using Persistence.Customization.FileStorage.Clients.Private;

namespace Candidates.Application.Commands.Tests
{
    public class SavePersonalityTestRaportsCommandHandler :
        BaseSaveTestRaportsCommandHandler, 
        INotificationHandler<SavePersonalityTestRaportsCommand>
    {
        private readonly TalogySettings _talogySettings;
        private readonly ICandidateTestsRepository _candidateTestsRepository;
        private readonly ILogger<SaveLogicalTestRaportsCommandHandler> _logger;

        public SavePersonalityTestRaportsCommandHandler(
            ITalogyApiClient talogyApiClient,
            IOptions<TalogySettings> talogySettings,
            ICandidateTestsRepository candidateTestsRepository,
            IPrivateFileUploadClient privateFileUploadClient,
            IOptions<BlobContainerSettings> blobContainerSettings,
            ILogger<SaveLogicalTestRaportsCommandHandler> logger) 
            : base(talogyApiClient, privateFileUploadClient, blobContainerSettings, logger)
        {
            _candidateTestsRepository = candidateTestsRepository;
            _talogySettings = talogySettings.Value;
            _logger = logger;
        }

        public async Task Handle(SavePersonalityTestRaportsCommand request, CancellationToken cancellationToken)
        {
            var candidateTests = await _candidateTestsRepository.GetAsync(request.CandidateId);

            if (candidateTests is null)
            {      
                candidateTests = new CandidateTests(request.CandidateId, request.ExternalId, null);
                _candidateTestsRepository.Add(candidateTests);
            }
            else
            {
                _candidateTestsRepository.Update(candidateTests);
            }

            await SavePapiDynamicWheelRaportAsync(candidateTests, request.Payload);
            await SavePapiGeneralFeedbackRaportAsync(candidateTests, request.Payload);

            await _candidateTestsRepository.UnitOfWork.SaveEntitiesAsync<CandidateTests>(cancellationToken);
        }

        private async Task SavePapiDynamicWheelRaportAsync(CandidateTests candidateTests, PackageInstanceScored payload)
        {
            try
            {
                if (candidateTests.PapiDynamicWheel is not null)
                {
                    return;
                }

                var raportType = _talogySettings.ReportTypes.PapiDynamicWheel;
                var raport = payload.ReportInstances.FirstOrDefault(x => x.ReportTypeId == raportType.Id);

                if (raport is null) return;

                var path = await SaveRaportAsync(raport, raportType);

                if (string.IsNullOrWhiteSpace(path)) return;

                candidateTests.AddPapiDynamicWheelRaport(raport.ReportInstanceId, path);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve: {Payload}", payload);
            }
        }

        private async Task SavePapiGeneralFeedbackRaportAsync(CandidateTests candidateTests, PackageInstanceScored payload)
        {
            try
            {
                if (candidateTests.PapiGeneralFeedback is not null)
                {
                    return;
                }
                
                var raportType = _talogySettings.ReportTypes.PapiGeneralFeedback;
                var raport = payload.ReportInstances.FirstOrDefault(x => x.ReportTypeId == raportType.Id);

                if (raport is null) return;

                var path = await SaveRaportAsync(raport, raportType);

                if (string.IsNullOrWhiteSpace(path)) return;

                candidateTests.AddPapiGeneralFeedbackRaport(raport.ReportInstanceId, path);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve: {Payload}", payload);
            }
        }
    }
}
