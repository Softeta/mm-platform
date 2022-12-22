using Candidates.Domain.Aggregates.CandidateTestsAggregate;
using Candidates.Infrastructure.Clients.Talogy;
using Candidates.Infrastructure.Clients.Talogy.Constants;
using Candidates.Infrastructure.Persistence.Repositories;
using Domain.Seedwork.Exceptions;
using MediatR;
using Persistence.Customization.FileStorage.Clients.Private;

namespace Candidates.Application.Commands.Tests
{
    public class DeletePersonalityTestCommandHandler : INotificationHandler<DeletePersonalityTestCommand>
    {
        private readonly ICandidateTestsRepository _candidateTestsRepository;
        private readonly IPrivateFileDeleteClient _privateFileDeleteClient;
        private readonly ITalogyApiClient _talogyApiClient;

        public DeletePersonalityTestCommandHandler(
            ICandidateTestsRepository candidateTestsRepository,
            IPrivateFileDeleteClient privateFileDeleteClient,
            ITalogyApiClient talogyApiClient) 
        {
            _candidateTestsRepository = candidateTestsRepository;
            _privateFileDeleteClient = privateFileDeleteClient;
            _talogyApiClient = talogyApiClient;
        }

        public async Task Handle(DeletePersonalityTestCommand request, CancellationToken cancellationToken)
        {
            var candidateTests = await _candidateTestsRepository.GetAsync(request.CandidateId);

            if (candidateTests == null ||
                candidateTests.PersonalityAssessment?.PackageInstanceId != request.PackageInstanceId)
            {
                throw new NotFoundException("Personality test not found", ErrorCodes.NotFound.TestsPackageNotFound);
            }

            await _talogyApiClient.DeleteAsync($"{Endpoints.PackageInstances}/{request.PackageInstanceId}");
            await DeleteBlobsAsync(candidateTests, cancellationToken);

            candidateTests.RemovePersonalityTest();

            _candidateTestsRepository.Update(candidateTests);
            await _candidateTestsRepository.UnitOfWork.SaveEntitiesAsync<CandidateTests>(cancellationToken);
        }

        private async Task DeleteBlobsAsync(CandidateTests candidateTests, CancellationToken cancellationToken)
        {
            var raports = new List<string?>
            {
                candidateTests.PapiDynamicWheel?.Url,
                candidateTests.PapiGeneralFeedback?.Url
            };

            var uris = raports
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => new Uri(x!))
            .ToArray();

            if (uris.Length == 0) return;

            await _privateFileDeleteClient.BatchDeleteAsync(uris, cancellationToken);
        }
    }
}
