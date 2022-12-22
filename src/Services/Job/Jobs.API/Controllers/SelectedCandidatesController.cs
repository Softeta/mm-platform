using API.Customization.Authorization.Constants;
using API.Customization.Controllers;
using API.Customization.Controllers.Attributes;
using API.Customization.Pagination;
using Contracts.Job.SelectedCandidates.Responses;
using Domain.Seedwork.Exceptions;
using Jobs.API.Models.Jobs.Filters;
using Jobs.Application.Contracts.JobCandidates.Responses;
using Jobs.Application.Queries.JobsCandidates;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Jobs.API.Controllers;

public class SelectedCandidatesController : AuthorizedApiController
{
    private readonly IMediator _mediator;

    public SelectedCandidatesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("api/v1/job-candidates/{jobId}/shortlist", Name = nameof(GetShortlistedCandidates))]
    [Authorize(CustomPolicies.IsAllowedModifyJob)]
    [ProducesResponseOk]
    public async Task<ActionResult<PagedResponse<JobSelectedCandidateResponse>>> GetShortlistedCandidates(
        [FromRoute, Required] Guid jobId,
        [FromQuery] ShortlistedCandidatesFilter filterParams)
    {
        var result = await _mediator.Send(new GetJobShortlistedCandidatesQuery(
            jobId,
            filterParams.PageNumber,
            filterParams.PageSize));

        var pageResponse = new PagedResponse<JobSelectedCandidateResponse>(
            result.Count,
            result.Candidates,
            filterParams.PageNumber,
            filterParams.PageSize,
            Url.RouteUrl(nameof(GetShortlistedCandidates))!,
            Request.QueryString.ToString());

        return Ok(pageResponse);
    }

    [HttpGet("api/v1/job-candidates/{jobId}/shortlist/candidates/{candidateId}")]
    [Authorize(CustomPolicies.IsAllowedModifyJob)]
    [ProducesResponseOk]
    public async Task<ActionResult<GetJobSelectedCandidateResponse>> GetShortlistedCandidateById(
        [FromRoute, Required] Guid jobId,
        [FromRoute, Required] Guid candidateId)
    {
        var command = new GetJobShortlistedCandidateQuery(jobId, candidateId);
        var result = await _mediator.Send(command);

        if (result is null)
        {
            throw new NotFoundException($"Shortlisted candidate does not exist. CandidateId: {candidateId}. JobId: {jobId}", 
                ErrorCodes.NotFound.ShortlistedCandidateNotFound);
        }

        return Ok(GetJobSelectedCandidateResponse.FromDomain(result));
    }
}
