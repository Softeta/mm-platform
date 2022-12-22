using API.Customization.Authorization.Constants;
using API.Customization.Controllers;
using API.Customization.Controllers.Attributes;
using Candidates.Application.Commands.Courses;
using Contracts.Candidate.Candidates.Responses;
using Contracts.Candidate.Courses.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence.Customization.FileStorage.Clients.Private;
using Local = Candidates.Application.Contracts.Candidates.Responses;

namespace Candidates.API.Areas.Candidates.Controllers
{
    public class CoursesController : AuthorizedApiController
    {
        private readonly IMediator _mediator;
        private readonly IPrivateBlobClient _privateBlobClient;

        public CoursesController(
            IMediator mediator,
            IPrivateBlobClient privateBlobClient)
        {
            _mediator = mediator;
            _privateBlobClient = privateBlobClient;
        }

        [HttpPost("api/v1/candidates/{candidateId}/courses")]
        [Authorize(CustomPolicies.IsAllowedModifyCandidate)]
        [ProducesResponseOk]
        public async Task<ActionResult<GetCandidateResponse>> AddCandidateCourse(
            [FromRoute] Guid candidateId,
            [FromBody] AddCandidateCourseRequest request)
        {
            var command = new AddCandidateCourseCommand(
                candidateId, 
                request.Name,
                request.IssuingOrganization,
                request.Description, 
                request.Certificate,
                UserId,
                Scope);

            var result = await _mediator.Send(command);

            return Ok(Local.GetCandidateResponse.FromDomain(result, _privateBlobClient));
        }

        [HttpPut("api/v1/candidates/{candidateId}/courses/{courseId}")]
        [Authorize(CustomPolicies.IsAllowedModifyCandidate)]
        [ProducesResponseOk]
        public async Task<ActionResult<GetCandidateResponse>> UpdateCandidateCourse(
            [FromRoute] Guid candidateId,
            [FromRoute] Guid courseId,
            [FromBody] UpdateCandidateCourseRequest request)
        {
            var command = new UpdateCandidateCourseCommand(
                candidateId,
                courseId,
                request.Name,
                request.IssuingOrganization,
                request.Description,
                request.Certificate,
                UserId,
                Scope);

            var result = await _mediator.Send(command);

            return Ok(Local.GetCandidateResponse.FromDomain(result, _privateBlobClient));
        }

        [HttpDelete("api/v1/candidates/{candidateId}/courses/{courseId}")]
        [Authorize(CustomPolicies.IsAllowedModifyCandidate)]
        [ProducesResponseOk]
        public async Task<ActionResult<GetCandidateResponse>> DeleteCandidateCourse(
            [FromRoute] Guid candidateId,
            [FromRoute] Guid courseId)
        {
            var command = new DeleteCandidateCourseCommand(
                candidateId,
                courseId,
                UserId,
                Scope);

            var result = await _mediator.Send(command);

            return Ok(Local.GetCandidateResponse.FromDomain(result, _privateBlobClient));
        }
    }
}
