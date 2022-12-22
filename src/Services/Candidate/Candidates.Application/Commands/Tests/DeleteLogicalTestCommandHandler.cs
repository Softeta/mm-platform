using Candidates.Domain.Aggregates.CandidateTestsAggregate;
using Candidates.Infrastructure.Clients.Talogy;
using Candidates.Infrastructure.Clients.Talogy.Constants;
using Candidates.Infrastructure.Persistence.Repositories;
using Domain.Seedwork.Exceptions;
using MediatR;
using Persistence.Customization.FileStorage.Clients.Private;

namespace Candidates.Application.Commands.Tests
{
    public class DeleteLogicalTestCommandHandler : INotificationHandler<DeleteLogicalTestCommand>
    {
        private readonly ICandidateTestsRepository _candidateTestsRepository;
        private readonly IPrivateFileDeleteClient _privateFileDeleteClient;
        private readonly ITalogyApiClient _talogyApiClient;

        public DeleteLogicalTestCommandHandler(
            ICandidateTestsRepository candidateTestsRepository,
            IPrivateFileDeleteClient privateFileDeleteClient,
            ITalogyApiClient talogyApiClient) 
        {
            _candidateTestsRepository = candidateTestsRepository;
            _privateFileDeleteClient = privateFileDeleteClient;
            _talogyApiClient = talogyApiClient;
        }

        public async Task Handle(DeleteLogicalTestCommand request, CancellationToken cancellationToken)
        {
            var candidateTests = await _candidateTestsRepository.GetAsync(request.CandidateId);

            if (candidateTests == null ||
                candidateTests.LogicalAssessment?.PackageInstanceId != request.PackageInstanceId)
            {
                throw new NotFoundException("Logical test not found", ErrorCodes.NotFound.TestsPackageNotFound);
            }

            await _talogyApiClient.DeleteAsync($"{Endpoints.PackageInstances}/{request.PackageInstanceId}");
            await DeleteBlobsAsync(candidateTests, cancellationToken);

            candidateTests.RemoveLogicalTest();

            _candidateTestsRepository.Update(candidateTests);
            await _candidateTestsRepository.UnitOfWork.SaveEntitiesAsync<CandidateTests>(cancellationToken);
        }

        private async Task DeleteBlobsAsync(CandidateTests candidateTests, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrWhiteSpace(candidateTests.LgiGeneralFeedback?.Url))
            {
                await _privateFileDeleteClient.DeleteAsync(candidateTests.LgiGeneralFeedback.Url, cancellationToken);
            }
        }
    }
}
