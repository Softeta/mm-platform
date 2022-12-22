using API.Customization.Authorization.Constants;
using API.Customization.Controllers;
using API.Customization.Controllers.Attributes;
using API.Customization.Pagination;
using Candidates.API.Areas.CandidateJobs.Models.Filters;
using Candidates.Application.Commands.CandidateInJobs;
using Candidates.Application.Contracts.CandidateJobs.Responses;
using Candidates.Application.Queries;
using Contracts.Candidate.CandidateJobs.Requests;
using Contracts.Candidate.CandidateJobs.Responses;
using Domain.Seedwork.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence.Customization.FileStorage.Clients.Private;

namespace Candidates.API.Areas.CandidateJobs.Controllers
{
    public class SelectedInJobsController : AuthorizedApiController
    {
        private readonly IMediator _mediator;
        private readonly IPrivateBlobClient _privateBlobClient;

        public SelectedInJobsController(
            IMediator mediator,
            IPrivateBlobClient privateBlobClient)
        {
            _mediator = mediator;
            _privateBlobClient = privateBlobClient;
        }

        [HttpGet("api/v1/candidates/{candidateId}/selected-jobs", Name = nameof(GetCandidateSelectedInJobs))]
        [Authorize(CustomPolicies.IsAllowedReadCandidate)]
        [ProducesResponseOk]
        public async Task<ActionResult<PagedResponse<GetCandidateSelectedInJobBriefResponse>>> GetCandidateSelectedInJobs(
            [FromRoute] Guid candidateId,
            [FromQuery] CandidateSelectedJobsFilter filterParams)
        {
            // TODO #2878: Candidate shouldn't access other candidates.

            var result = await _mediator.Send(new GetCandidateSelectedInJobsQuery(
                candidateId,
                filterParams.SelectedCandidateStages,
                filterParams.IsInvited,
                filterParams.PageNumber,
                filterParams.PageSize));

            var pageResponse = new PagedResponse<GetCandidateSelectedInJobBriefResponse>(
                result.Count,
                result.SelectedInJobs,
                filterParams.PageNumber,
                filterParams.PageSize,
                Url.RouteUrl(nameof(GetCandidateSelectedInJobs))!,
                Request.QueryString.ToString());

            return Ok(pageResponse);
        }

        [HttpGet("api/v1/candidates/{candidateId}/selected-jobs/{jobId}")]
        [Authorize(CustomPolicies.IsAllowedReadCandidate)]
        [ProducesResponseOk]
        public async Task<ActionResult<CandidateSelectedInJobResponse>> GetCandidateSelectedInJob(
            [FromRoute] Guid jobId,
            [FromRoute] Guid candidateId)
        {
            // TODO #2878: Candidate shouldn't access other candidates.

            var result = await _mediator.Send(new GetCandidateSelectedInJobQuery(jobId, candidateId));

            if (result is null)
            {
                throw new NotFoundException(
                    $"Selected candidate in job not found. CandidateId: {candidateId}, JobId: {jobId}",
                    ErrorCodes.NotFound.CandidateSelectedInJobNotFound);
            }

            return Ok(CandidateSelectedInJobResponse.FromDomain(result, _privateBlobClient));
        }

        [HttpPut("api/v1/candidates/{candidateId}/selected-jobs/{jobId}")]
        [Authorize(CustomPolicies.IsAllowedModifyCandidate)]
        [ProducesResponseOk]
        public async Task<ActionResult<CandidateSelectedInJobResponse>> UpdateCandidateSelectedInJob(
            [FromRoute] Guid candidateId,
            [FromRoute] Guid jobId,
            [FromBody] UpdateCandidateSelectedInJobRequest request)
        {
            // TODO #2878: Candidate shouldn't access other candidates.
            var command = new UpdateCandidateSelectedInJobCommand(candidateId, jobId, request.MotivationVideo, request.CoverLetter);
            var result = await _mediator.Send(command);

            return Ok(CandidateSelectedInJobResponse.FromDomain(result, _privateBlobClient));
        }

        [HttpPatch("api/v1/candidates/{candidateId}/selected-jobs/{jobId}/rejected")]
        [Authorize(CustomPolicies.IsAllowedModifyCandidate)]
        [ProducesResponseNoContent]
        public async Task<ActionResult> RejectCandidateSelectedInJob(
            [FromRoute] Guid candidateId,
            [FromRoute] Guid jobId)
        {
            // TODO #2878: Candidate shouldn't access other candidates.
            var command = new RejectCandidateSelectedInJobCommand(candidateId, jobId);
            await _mediator.Publish(command);

            return NoContent();
        }
    }
}
