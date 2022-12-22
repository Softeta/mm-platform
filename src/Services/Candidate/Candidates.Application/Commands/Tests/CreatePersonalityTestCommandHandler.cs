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
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Candidates.Application.Commands.Tests
{
    public class CreatePersonalityTestCommandHandler : ModifyCandidateBaseCommandHandler<CreatePersonalityTestCommand, CandidateTests?>
    {
        private readonly ICandidateTestsRepository _candidateTestsRepository;
        private readonly ITalogyApiClient _talogyApiClient;
        private readonly TalogySettings _talogySettings;
        private readonly IMediator _mediator;
        private readonly ILogger<CreatePersonalityTestCommandHandler> _logger;

        public CreatePersonalityTestCommandHandler(
            ICandidateRepository candidateRepository,
            ICandidateTestsRepository candidateTestsRepository,
            ITalogyApiClient talogyApiClient,
            IOptions<TalogySettings> talogySettings,
            IMediator mediator,
            ILogger<CreatePersonalityTestCommandHandler> logger) : base(candidateRepository)
        {
            _candidateTestsRepository = candidateTestsRepository;
            _talogyApiClient = talogyApiClient;
            _talogySettings = talogySettings.Value;
            _mediator = mediator;
            _logger = logger;
        }

        protected override async Task<CandidateTests?> Handle(CreatePersonalityTestCommand request, Candidate candidate, CancellationToken cancellationToken)
        {
            var candidateTests = await _candidateTestsRepository.GetAsync(request.CandidateId);

            if (candidateTests?.PersonalityAssessment is not null)
            {
                throw new ConflictException("Personality test for candidate already exists", ErrorCodes.Conflict.PersonalityTestAlreadyExists);
            }

            if (!candidate.ExternalId.HasValue)
            {
                throw new BadRequestException("Candidate must register before doing a test", ErrorCodes.BadRequest.CandidateMustRegisterBeforeDoingTest);
            }

            var packageInstanceId = candidate.Id.BuildPackageInstanceId(_talogySettings.PackageTypes.Personality, AssessmentTypes.PAPI.ToString());
            var packageInstance = await GetOrCreatePackageInstanceAsync(candidate, packageInstanceId, _talogySettings.PackageTypes.Personality);

            if (candidateTests is null)
            {
                candidateTests = new CandidateTests(
                    request.CandidateId, 
                    candidate.ExternalId.Value,
                    candidate.CandidateOldPlatformId);

                candidateTests.CreatePersonalityAssessment(
                    packageInstanceId,
                    _talogySettings.PackageTypes.Personality, 
                    packageInstance.LogonUrl);

                _candidateTestsRepository.Add(candidateTests);
            }
            else
            {
                candidateTests.CreatePersonalityAssessment(
                    packageInstanceId,
                    _talogySettings.PackageTypes.Personality,
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
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to create Package Instance {packageInstanceId}");

                var instance = await _talogyApiClient.GetAsync<GetPackageInstance>($"{Endpoints.PackageInstances}/{packageInstanceId}");

                if (instance is null)
                {
                    throw new ConflictException("Personality test can't be created for candidate", ErrorCodes.Conflict.PersonalityTestAlreadyExists);
                }

                return new PackageInstance
                {
                    LogonUrl = instance.LogonUrl
                };
            }
        }
    }
}
