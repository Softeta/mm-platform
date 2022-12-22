using Candidates.Domain.Aggregates.CandidateTestsAggregate;
using Candidates.Domain.Aggregates.CandidateTestsAggregate.ValueObjects;
using Candidates.Infrastructure.Clients.Talogy;
using Candidates.Infrastructure.Clients.Talogy.Models.Responses;
using Candidates.Infrastructure.Clients.Talogy.Models.Responses.PackageInstance;
using Candidates.Infrastructure.Persistence.Repositories;
using Candidates.Infrastructure.Settings;
using Domain.Seedwork.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Persistence.Customization.FileStorage;
using Persistence.Customization.FileStorage.Clients.Private;

namespace Candidates.Application.Commands.Tests
{
    public class ForceRetrievePersonalityTestResultsCommandHandler : 
        BaseSaveTestRaportsCommandHandler, 
        IRequestHandler<ForceRetrievePersonalityTestResultsCommand, CandidateTests?>
    {
        private readonly TalogySettings _talogySettings;
        private readonly ICandidateTestsRepository _candidateTestsRepository;
        private readonly ILogger<ForceRetrievePersonalityTestResultsCommandHandler> _logger;

        public ForceRetrievePersonalityTestResultsCommandHandler(
            ICandidateTestsRepository candidateTestsRepository,
            IOptions<TalogySettings> talogySettings,
            ITalogyApiClient talogyApiClient,
            IPrivateFileUploadClient privateFileUploadClient,
            IOptions<BlobContainerSettings> blobContainerSettings,
            ILogger<ForceRetrievePersonalityTestResultsCommandHandler> logger)
            : base(talogyApiClient, privateFileUploadClient, blobContainerSettings, logger)
        {
            _candidateTestsRepository = candidateTestsRepository;
            _talogySettings = talogySettings.Value;
            _logger = logger;
        }

        public async Task<CandidateTests?> Handle(ForceRetrievePersonalityTestResultsCommand request, CancellationToken cancellationToken)
        {
            var candidateTests = await _candidateTestsRepository.GetAsync(request.CandidateId);
            ValidateTests(candidateTests, request);

            var response = await GetPackageInstanceAsync(request.PackageInstanceId);
            ValidatePackageInstance(response);

            var assessmentInstance = response!.AssessmentInstances.First();
            var logicalTest = candidateTests!.PersonalityAssessment!;
            var scores = CollectPersonalityScores(response);

            ValidateScores(scores);

            candidateTests!.CompletePersonalityAssessment(
                request.PackageInstanceId,
                logicalTest.PackageTypeId,
                assessmentInstance.AssessmentInstanceStartTime!.Value,
                assessmentInstance.AssessmentInstanceCompleteTime!.Value,
                scores);

            await AddPapiGeneralFeedbackRaportAsync(candidateTests, response);
            await AddPapiDynamicWheelRaportAsync(candidateTests, response);

            _candidateTestsRepository.Update(candidateTests);

            await _candidateTestsRepository.UnitOfWork.SaveEntitiesAsync<CandidateTests>(cancellationToken);

            return candidateTests;
        }

        private async Task AddPapiGeneralFeedbackRaportAsync(CandidateTests candidateTests, GetPackageInstance packageInstance)
        {
            try
            {
                if (candidateTests.PapiGeneralFeedback is not null)
                {
                    return;
                }

                var raportType = _talogySettings.ReportTypes.PapiGeneralFeedback;
                var raport = packageInstance.ReportInstances.FirstOrDefault(x => x.ReportTypeId == raportType.Id);

                if (raport is null) return;

                var path = await SaveRaportAsync(raport, raportType);

                if (string.IsNullOrWhiteSpace(path)) return;

                candidateTests.AddPapiGeneralFeedbackRaport(raport.ReportInstanceId, path);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve: {PackageInstance}", packageInstance);
            }
        }

        private async Task AddPapiDynamicWheelRaportAsync(CandidateTests candidateTests, GetPackageInstance packageInstance)
        {
            try
            {
                if (candidateTests.PapiDynamicWheel is not null)
                {
                    return;
                }

                var raportType = _talogySettings.ReportTypes.PapiDynamicWheel;
                var raport = packageInstance.ReportInstances.FirstOrDefault(x => x.ReportTypeId == raportType.Id);

                if (raport is null) return;

                var path = await SaveRaportAsync(raport, raportType);

                if (string.IsNullOrWhiteSpace(path)) return;

                candidateTests.AddPapiDynamicWheelRaport(raport.ReportInstanceId, path);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve: {PackageInstance}", packageInstance);
            }
        }

        private static void ValidateTests(CandidateTests? candidateTests, ForceRetrievePersonalityTestResultsCommand request)
        {
            var testExists =
                candidateTests?.PersonalityAssessment?.PackageInstanceId == request.PackageInstanceId!;

            if (!testExists)
            {
                throw new NotFoundException("Test not found", ErrorCodes.NotFound.TestsPackageNotFound);
            }

            if (candidateTests?.PersonalityAssessment?.Scores is not null)
            {
                throw new ConflictException("Personality test is already completed and synced", ErrorCodes.Conflict.TestAlreadyCompleted);
            }
        }

        private static void ValidateScores(PersonalityScores? scores)
        {
            if (scores is null)
            {
                throw new ConflictException("Test Results are not generated yet", ErrorCodes.Conflict.TestPackageNotCompleted);
            }
        }

        private static void ValidatePackageInstance(GetPackageInstance? packageInstance)
        {
            if (packageInstance is null)
            {
                throw new NotFoundException("Tests not found in Talogy API", ErrorCodes.NotFound.TestsPackageNotFound);
            }

            var assessmentInstance = packageInstance.AssessmentInstances.SingleOrDefault();

            if (assessmentInstance is null)
            {
                throw new NotFoundException("Test not found in Talogy API", ErrorCodes.NotFound.TestsPackageNotFound);
            }

            if (assessmentInstance.Status != TalogyAssessmentStatus.Completed)
            {
                throw new ConflictException("Test Results are not generated yet", ErrorCodes.Conflict.TestPackageNotCompleted);
            }

            if (!assessmentInstance.AssessmentInstanceStartTime.HasValue)
            {
                throw new ConflictException("Test Results are not generated yet", ErrorCodes.Conflict.TestPackageNotCompleted);
            }

            if (!assessmentInstance.AssessmentInstanceCompleteTime.HasValue)
            {
                throw new ConflictException("Test Results are not generated yet", ErrorCodes.Conflict.TestPackageNotCompleted);
            }
        }
    }
}
