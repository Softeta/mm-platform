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
    public class ForceRetrieveLogicalTestResultsCommandHandler :
        BaseSaveTestRaportsCommandHandler,
        IRequestHandler<ForceRetrieveLogicalTestResultsCommand, CandidateTests?>
    {
        private readonly TalogySettings _talogySettings;
        private readonly ICandidateTestsRepository _candidateTestsRepository;
        private readonly ILogger<ForceRetrieveLogicalTestResultsCommandHandler> _logger;

        public ForceRetrieveLogicalTestResultsCommandHandler(
            ICandidateTestsRepository candidateTestsRepository,
            IOptions<TalogySettings> talogySettings,
            ITalogyApiClient talogyApiClient,
            IPrivateFileUploadClient privateFileUploadClient,
            IOptions<BlobContainerSettings> blobContainerSettings,
            ILogger<ForceRetrieveLogicalTestResultsCommandHandler> logger) 
            : base(talogyApiClient, privateFileUploadClient, blobContainerSettings, logger)
        {
            _candidateTestsRepository = candidateTestsRepository;
            _talogySettings = talogySettings.Value;
            _logger = logger;
        }

        public async Task<CandidateTests?> Handle(ForceRetrieveLogicalTestResultsCommand request, CancellationToken cancellationToken)
        {
            var candidateTests = await _candidateTestsRepository.GetAsync(request.CandidateId);
            ValidateTests(candidateTests, request);

            var response = await GetPackageInstanceAsync(request.PackageInstanceId);
            ValidatePackageInstance(response);

            var assessmentInstance = response!.AssessmentInstances.First();
            var logicalTest = candidateTests!.LogicalAssessment!;

            var scores = CollectLogicalScores(response);

            ValidateScores(scores);

            candidateTests!.CompleteLogicalAssessment(
                request.PackageInstanceId,
                logicalTest.PackageTypeId,
                assessmentInstance.AssessmentInstanceStartTime!.Value,
                assessmentInstance.AssessmentInstanceCompleteTime!.Value,
                CollectLogicalScores(response));

            await AddLgiGeneralFeedbackRaportAsync(candidateTests, response);

            _candidateTestsRepository.Update(candidateTests);

            await _candidateTestsRepository.UnitOfWork.SaveEntitiesAsync<CandidateTests>(cancellationToken);

            return candidateTests;
        }

        private async Task AddLgiGeneralFeedbackRaportAsync(CandidateTests candidateTests, GetPackageInstance packageInstance)
        {
            try
            {
                if (candidateTests.LgiGeneralFeedback is not null)
                {
                    return;
                }

                var raportType = _talogySettings.ReportTypes.LgiGeneralFeedback;
                var raport = packageInstance.ReportInstances.FirstOrDefault(x => x.ReportTypeId == raportType.Id);

                if (raport is null) return;

                var path = await SaveRaportAsync(raport, raportType);

                if (string.IsNullOrWhiteSpace(path)) return;

                candidateTests.AddLgiGeneralFeedbackRaport(raport.ReportInstanceId, path);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve: {PackageInstance}", packageInstance);
            }
        }

        private static void ValidateTests(CandidateTests? candidateTests, ForceRetrieveLogicalTestResultsCommand request)
        {
            var testExists =
                candidateTests?.LogicalAssessment?.PackageInstanceId == request.PackageInstanceId!;

            if (!testExists)
            {
                throw new NotFoundException("Test not found", ErrorCodes.NotFound.TestsPackageNotFound);
            }

            if (candidateTests?.LogicalAssessment?.Scores is not null)
            {
                throw new ConflictException("Logical test is already completed and synced", ErrorCodes.Conflict.TestAlreadyCompleted);
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

        private static void ValidateScores(LogicScores? scores)
        {
            if (scores is null)
            {
                throw new ConflictException("Test Results are not generated yet", ErrorCodes.Conflict.TestPackageNotCompleted);
            }
        }
    }
}
