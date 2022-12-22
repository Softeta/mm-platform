using API.Customization.Authorization.Constants;
using API.Customization.Controllers;
using API.Customization.Controllers.Attributes;
using Candidates.API.Areas.Tests.Models.Responses;
using Candidates.Application.Commands.Tests;
using Candidates.Application.Queries.CandidateTest;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence.Customization.FileStorage.Clients.Private;

namespace Candidates.API.Areas.Tests.Controllers
{
    public class CandidateTestsController : AuthorizedApiController
    {
        private readonly IMediator _mediator;
        private readonly IPrivateBlobClient _privateBlobClient;

        public CandidateTestsController(IMediator mediator, IPrivateBlobClient privateBlobClient)
        {
            _mediator = mediator;
            _privateBlobClient = privateBlobClient;
        }

        [HttpGet("api/v1/candidate-tests/{candidateId}")]
        [Authorize(CustomPolicies.IsAllowedReadSingleCandidate)]
        [ProducesResponseOk]
        public async Task<ActionResult<GetCandidateTestsResponse>> GetCandidatesTests([FromRoute] Guid candidateId)
        {
            var command = new GetCandidateTestsQuery(candidateId, UserId, Scope);
            var result = await _mediator.Send(command);

            return Ok(GetCandidateTestsResponse.FromDomain(result, _privateBlobClient));
        }


        [HttpPost("api/v1/candidate-tests/{candidateId}/logical")]
        [Authorize(CustomPolicies.IsAllowedCandidateModifyHimself)]
        [ProducesResponseOk]
        public async Task<ActionResult<GetCandidateTestsResponse>> InitializeLogicTest([FromRoute] Guid candidateId)
        {
            var command = new CreateLogicalTestCommand(candidateId, UserId, Scope);
            var result = await _mediator.Send(command);

            return Ok(GetCandidateTestsResponse.FromDomain(result, _privateBlobClient));
        }

        [HttpPost("api/v1/candidate-tests/{candidateId}/personality")]
        [Authorize(CustomPolicies.IsAllowedCandidateModifyHimself)]
        [ProducesResponseOk]
        public async Task<ActionResult<GetCandidateTestsResponse>> InitializePersonalityTest([FromRoute] Guid candidateId)
        {
            var command = new CreatePersonalityTestCommand(candidateId, UserId, Scope);
            var result = await _mediator.Send(command);

            return Ok(GetCandidateTestsResponse.FromDomain(result, _privateBlobClient));
        }

        [HttpDelete("api/v1/candidate-tests/{candidateId}/personality/{packageInstanceId}")]
        [Authorize(CustomPolicies.IsAllowedBackOfficeUserModifyCandidate)]
        [ProducesResponseNoContent]
        public async Task<ActionResult<GetCandidateTestsResponse>> DeletePersonalityTest(
            [FromRoute] Guid candidateId, 
            [FromRoute] string packageInstanceId)
        {
            var command = new DeletePersonalityTestCommand(candidateId, packageInstanceId);
            await _mediator.Publish(command);

            return NoContent();
        }

        [HttpDelete("api/v1/candidate-tests/{candidateId}/logical/{packageInstanceId}")]
        [Authorize(CustomPolicies.IsAllowedBackOfficeUserModifyCandidate)]
        [ProducesResponseNoContent]
        public async Task<ActionResult<GetCandidateTestsResponse>> DeleteLogicalTest(
            [FromRoute] Guid candidateId,
            [FromRoute] string packageInstanceId)
        {
            var command = new DeleteLogicalTestCommand(candidateId, packageInstanceId);
            await _mediator.Publish(command);

            return NoContent();
        }

        [HttpPost("api/v1/candidate-tests/{candidateId}/personality/{packageInstanceId}/forced")]
        [Authorize(CustomPolicies.IsAllowedBackOfficeUserModifyCandidate)]
        [ProducesResponseOk]
        public async Task<ActionResult<GetCandidateTestsResponse>> ForceSyncPersonalityTestResults(
            [FromRoute] Guid candidateId, 
            [FromRoute] string packageInstanceId)
        {
            var command = new ForceRetrievePersonalityTestResultsCommand(candidateId, packageInstanceId);
            var result = await _mediator.Send(command);

            return Ok(GetCandidateTestsResponse.FromDomain(result, _privateBlobClient));
        }

        [HttpPost("api/v1/candidate-tests/{candidateId}/logical/{packageInstanceId}/forced")]
        [Authorize(CustomPolicies.IsAllowedBackOfficeUserModifyCandidate)]
        [ProducesResponseOk]
        public async Task<ActionResult<GetCandidateTestsResponse>> ForceSyncLogicalTestResults(
            [FromRoute] Guid candidateId,
            [FromRoute] string packageInstanceId)
        {
            var command = new ForceRetrieveLogicalTestResultsCommand(candidateId, packageInstanceId);
            var result = await _mediator.Send(command);

            return Ok(GetCandidateTestsResponse.FromDomain(result, _privateBlobClient));
        }
    }
}
