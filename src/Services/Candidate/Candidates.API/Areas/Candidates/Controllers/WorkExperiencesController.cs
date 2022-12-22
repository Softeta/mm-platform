using API.Customization.Authorization.Constants;
using API.Customization.Controllers;
using API.Customization.Controllers.Attributes;
using Candidates.Application.Commands.WorkExperiences;
using Contracts.Candidate.Candidates.Responses;
using Contracts.Candidate.WorkExperiences.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence.Customization.FileStorage.Clients.Private;
using Local = Candidates.Application.Contracts.Candidates.Responses;

namespace Candidates.API.Areas.Candidates.Controllers
{
    public class WorkExperiencesController : AuthorizedApiController
    {
        private readonly IMediator _mediator;
        private readonly IPrivateBlobClient _privateBlobClient;

        public WorkExperiencesController(
            IMediator mediator,
            IPrivateBlobClient privateBlobClient)
        {
            _mediator = mediator;
            _privateBlobClient = privateBlobClient;
        }

        [HttpPost("api/v1/candidates/{candidateId}/work-experiences")]
        [Authorize(CustomPolicies.IsAllowedModifyCandidate)]
        [ProducesResponseOk]
        public async Task<ActionResult<GetCandidateResponse>> AddCandidateWorkExperience(
            [FromRoute] Guid candidateId,
            [FromBody] CandidateWorkExperienceRequest request)
        {
            var command = new AddCandidateWorkExperienceCommand(
                candidateId,
                request.Type,
                request.CompanyName,
                request.Position,
                request.From,
                request.To,
                request.JobDescription,
                request.IsCurrentJob,
                request.Skills,
                UserId,
                Scope);

            var result = await _mediator.Send(command);

            return Ok(Local.GetCandidateResponse.FromDomain(result, _privateBlobClient));
        }

        [HttpPut("api/v1/candidates/{candidateId}/work-experiences/{workExperienceId}")]
        [Authorize(CustomPolicies.IsAllowedModifyCandidate)]
        [ProducesResponseOk]
        public async Task<ActionResult<GetCandidateResponse>> UpdateCandidateWorkExperience(
            [FromRoute] Guid candidateId,
            [FromRoute] Guid workExperienceId,
            [FromBody] CandidateWorkExperienceRequest request)
        {
            var command = new UpdateCandidateWorkExperienceCommand(
                candidateId,
                workExperienceId,
                request.Type,
                request.CompanyName,
                request.Position,
                request.From,
                request.To,
                request.JobDescription,
                request.IsCurrentJob,
                request.Skills,
                UserId,
                Scope);

            var result = await _mediator.Send(command);

            return Ok(Local.GetCandidateResponse.FromDomain(result, _privateBlobClient));
        }

        [HttpDelete("api/v1/candidates/{candidateId}/work-experiences/{workExperienceId}")]
        [Authorize(CustomPolicies.IsAllowedModifyCandidate)]
        [ProducesResponseOk]
        public async Task<ActionResult<GetCandidateResponse>> DeleteCandidateWorkExperience(
            [FromRoute] Guid candidateId,
            [FromRoute] Guid workExperienceId)
        {
            var command = new DeleteCandidateWorkExperienceCommand(
                candidateId,
                workExperienceId,
                UserId,
                Scope);

            var result = await _mediator.Send(command);

            return Ok(Local.GetCandidateResponse.FromDomain(result, _privateBlobClient));
        }
    }
}
