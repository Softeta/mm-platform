using API.Customization.Authorization.Constants;
using API.Customization.Controllers;
using API.Customization.Controllers.Attributes;
using Candidates.Application.Commands.Educations;
using Contracts.Candidate.Candidates.Responses;
using Contracts.Candidate.Educations.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence.Customization.FileStorage.Clients.Private;
using Local = Candidates.Application.Contracts.Candidates.Responses;

namespace Candidates.API.Areas.Candidates.Controllers
{
    public class EducationsController : AuthorizedApiController
    {
        private readonly IMediator _mediator;
        private readonly IPrivateBlobClient _privateBlobClient;

        public EducationsController(
            IMediator mediator,
            IPrivateBlobClient privateBlobClient)
        {
            _mediator = mediator;
            _privateBlobClient = privateBlobClient;
        }

        [HttpPost("api/v1/candidates/{candidateId}/educations")]
        [Authorize(CustomPolicies.IsAllowedModifyCandidate)]
        [ProducesResponseOk]
        public async Task<ActionResult<GetCandidateResponse>> AddCandidateEducation(
            [FromRoute] Guid candidateId,
            [FromBody] AddCandidateEducationRequest request)
        {
            var command = new AddCandidateEducationCommand(
                candidateId,
                request.SchoolName,
                request.Degree,
                request.FieldOfStudy,
                request.From,
                request.To,
                request.StudiesDescription,
                request.IsStillStudying,
                request.Certificate,
                UserId,
                Scope);

            var result = await _mediator.Send(command);

            return Ok(Local.GetCandidateResponse.FromDomain(result, _privateBlobClient));
        }

        [HttpPut("api/v1/candidates/{candidateId}/educations/{educationId}")]
        [Authorize(CustomPolicies.IsAllowedModifyCandidate)]
        [ProducesResponseOk]
        public async Task<ActionResult<GetCandidateResponse>> UpdateCandidateEducation(
            [FromRoute] Guid candidateId,
            [FromRoute] Guid educationId,
            [FromBody] UpdateCandidateEducationRequest request)
        {
            var command = new UpdateCandidateEducationCommand(
                candidateId,
                educationId,
                request.SchoolName,
                request.Degree,
                request.FieldOfStudy,
                request.From,
                request.To,
                request.StudiesDescription,
                request.IsStillStudying,
                request.Certificate,
                UserId,
                Scope);

            var result = await _mediator.Send(command);

            return Ok(Local.GetCandidateResponse.FromDomain(result, _privateBlobClient));
        }

        [HttpDelete("api/v1/candidates/{candidateId}/educations/{educationId}")]
        [Authorize(CustomPolicies.IsAllowedModifyCandidate)]
        [ProducesResponseOk]
        public async Task<ActionResult<GetCandidateResponse>> DeleteCandidateEducation(
            [FromRoute] Guid candidateId,
            [FromRoute] Guid educationId)
        {
            var command = new DeleteCandidateEducationCommand(
                candidateId,
                educationId,
                UserId,
                Scope);

            var result = await _mediator.Send(command);

            return Ok(Local.GetCandidateResponse.FromDomain(result, _privateBlobClient));
        }
    }
}
