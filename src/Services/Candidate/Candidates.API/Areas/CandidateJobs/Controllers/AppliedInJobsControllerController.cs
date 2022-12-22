using API.Customization.Authorization.Constants;
using API.Customization.Controllers;
using API.Customization.Controllers.Attributes;
using API.Customization.Pagination;
using Candidates.API.Areas.CandidateJobs.Models.Filters;
using Candidates.Application.Commands.CandidateInJobs;
using Candidates.Application.Queries;
using Contracts.Candidate.CandidateJobs.Requests;
using Contracts.Candidate.CandidateJobs.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Candidates.API.Areas.CandidateJobs.Controllers
{
    public class AppliedInJobsControllerController : AuthorizedApiController
    {
        private readonly IMediator _mediator;

        public AppliedInJobsControllerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("api/v1/candidates/{candidateId}/applied-jobs", Name = nameof(GetAppliedToJobs))]
        [Authorize(CustomPolicies.IsAllowedReadCandidate)]
        [ProducesResponseOk]
        public async Task<ActionResult<PagedResponse<GetCandidateAppliedToJobResponse>>> GetAppliedToJobs(
            [FromRoute] Guid candidateId,
            [FromQuery] GetAppliedJobsFilter filterParams)
        {
            // TODO #2878: Candidate shouldn't access other candidates.

            var query = new GetAppliedJobsQuery(
                candidateId,
                filterParams.OrderBy,
                filterParams.PageNumber,
                filterParams.PageSize);

            var result = await _mediator.Send(query);

            var pageResponse = new PagedResponse<GetCandidateAppliedToJobResponse>(
                result.Count,
                result.AppliedToJobs,
                filterParams.PageNumber,
                filterParams.PageSize,
                Url.RouteUrl(nameof(GetAppliedToJobs))!,
                Request.QueryString.ToString());

            return Ok(pageResponse);
        }

        [HttpPost("api/v1/candidates/{candidateId}/applied-jobs/{jobId}")]
        [Authorize(CustomPolicies.IsAllowedModifyCandidate)]
        [ProducesResponseNoContent]
        public async Task<ActionResult> ApplyToJob(
            [FromRoute] Guid candidateId,
            [FromRoute] Guid jobId,
            [FromBody] CandidateApplyToJobRequest request)
        {
            // TODO #2878: Candidate shouldn't access other candidates.
            var command = new CandidateApplyToJobCommand(
                candidateId,
                jobId,
                request.JobStage,
                request.PositionId,
                request.PositionCode,
                request.PositionAliasToId,
                request.PositionAliasToCode,
                request.CompanyId,
                request.CompanyName,
                request.CompanyLogo,
                request.Freelance,
                request.Permanent,
                request.StartDate,
                request.DeadlineDate);
            await _mediator.Publish(command);

            return NoContent();
        }
    }
}
