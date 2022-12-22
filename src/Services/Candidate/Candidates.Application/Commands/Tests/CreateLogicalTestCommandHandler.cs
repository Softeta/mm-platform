using Candidates.Application.Contracts.Assessments;
using Candidates.Domain.Aggregates.CandidateAggregate;
using Candidates.Domain.Aggregates.CandidateTestsAggregate;
using Candidates.Domain.Aggregates.CandidateTestsAggregate.Constants;
using Candidates.Domain.Aggregates.CandidateTestsAggregate.Services;
using Candidates.Infrastructure.Clients.Talogy;
using Candidates.Infrastructure.Clients.Talogy.Constants;
using Candidates.Infrastructure.Clients.Talogy.Models.Responses;
using Candidates.Infrastructure.Persistence.Repositories;
using Candidates.Infrastructure.Settings;
using Domain.Seedwork.Exceptions;
using MediatR;
using Microsoft.Extensions.Options;

namespace Candidates.Application.Commands.Tests
{
    public class CreateLogicalTestCommandHandler : ModifyCandidateBaseCommandHandler<CreateLogicalTestCommand, CandidateTests?>
    {
        private readonly ICandidateTestsRepository _candidateTestsRepository;
        private readonly ITalogyApiClient _talogyApiClient;
        private readonly TalogySettings _talogySettings;
        private readonly IMediator _mediator;

        public CreateLogicalTestCommandHandler(
            ICandidateRepository candidateRepository,
            ICandidateTestsRepository candidateTestsRepository,
            ITalogyApiClient talogyApiClient,
            IOptions<TalogySettings> talogySettings,
            IMediator mediator) : base(candidateRepository)
        {
            _candidateTestsRepository = candidateTestsRepository;
            _talogyApiClient = talogyApiClient;
            _talogySettings = talogySettings.Value;
            _mediator = mediator;
        }

        protected override async Task<CandidateTests?> Handle(CreateLogicalTestCommand request, Candidate candidate, CancellationToken cancellationToken)
        {
            var candidateTests = await _candidateTestsRepository.GetAsync(request.CandidateId);

            if (candidateTests?.LogicalAssessment is not null)
            {
                throw new ConflictException("Logical test for candidate already exists", ErrorCodes.Conflict.LogicalTestAlreadyExists);
            }

            if (!candidate.ExternalId.HasValue)
            {
                throw new BadRequestException("Candidate must register before doing a test", ErrorCodes.BadRequest.CandidateMustRegisterBeforeDoingTest);
            }

            var packageInstanceId = candidate.Id.BuildPackageInstanceId(_talogySettings.PackageTypes.Logic, AssessmentTypes.LGI.ToString());
            var packageInstance = await GetOrCreatePackageInstanceAsync(candidate, packageInstanceId, _talogySettings.PackageTypes.Logic);

            if (candidateTests is null)
            {
                candidateTests = new CandidateTests(
                    request.CandidateId,
                    candidate.ExternalId.Value,
                    candidate.CandidateOldPlatformId);

                candidateTests.CreateLogicalAssessment(
                    packageInstanceId,
                    _talogySettings.PackageTypes.Logic, 
                    packageInstance.LogonUrl);

                _candidateTestsRepository.Add(candidateTests);
            }
            else
            {
                candidateTests.CreateLogicalAssessment(
                    packageInstanceId,
                    _talogySettings.PackageTypes.Logic,
                    packageInstance.LogonUrl);

                _candidateTestsRepository.Update(candidateTests);
            }

            await _candidateTestsRepository.UnitOfWork.SaveEntitiesAsync<CandidateTests>(cancellationToken);

            return candidateTests;
        }

        private async Task<PackageInstance> GetOrCreatePackageInstanceAsync(Candidate candidate, string packageInstanceId, string packageTypeId)
        {
            try
            {
                var createdInstance = await _mediator.Send(new CreatePackageInstanceCommand(candidate, packageInstanceId, packageTypeId));

                return new PackageInstance
                {
                    LogonUrl = createdInstance.LogonUrl
                };
            }
            catch
            {
                var instance = await _talogyApiClient.GetAsync<GetPackageInstance>($"{Endpoints.PackageInstances}/{packageInstanceId}");

                if (instance is null)
                {
                    throw new ConflictException("Logic test can't be created for candidate", ErrorCodes.Conflict.LogicalTestCanNotCreate);
                }

                return new PackageInstance
                {
                    LogonUrl = instance.LogonUrl
                };
            }
        }
    }
}
