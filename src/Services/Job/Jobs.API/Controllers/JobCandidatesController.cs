using API.Customization.Authorization.Constants;
using API.Customization.Controllers;
using API.Customization.Controllers.Attributes;
using Contracts.Job.ArchivedCandidates.Requests;
using Contracts.Job.JobCandidates.Requests;
using Contracts.Job.JobCandidates.Responses;
using Contracts.Job.SelectedCandidates.Requests;
using Jobs.Application.Commands;
using Jobs.Application.Queries.JobsCandidates;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Local = Jobs.Application.Contracts.JobCandidates.Responses;

namespace Jobs.API.Controllers;

public class JobCandidatesController : AuthorizedApiController
{
    private readonly IMediator _mediator;

    public JobCandidatesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("api/v1/job-candidates/{jobId}")]
    [Authorize(CustomPolicies.IsAllowedModifyJob)]
    [ProducesResponseOk]
    public async Task<ActionResult<GetJobCandidatesResponse>> GetJobCandidatesById([FromRoute, Required] Guid jobId)
    {
        var job = await _mediator.Send(new GetJobCandidatesQuery(jobId));

        if (job == null)
        {
            return NotFound();
        }

        return Ok(Local.GetJobCandidatesResponse.FromDomain(job));
    }

    [HttpPost("api/v1/job-candidates/{jobId}/selected-candidates")]
    [Authorize(CustomPolicies.IsAllowedModifyJob)]
    [ProducesResponseOk]
    public async Task<ActionResult<GetJobCandidatesResponse>> AddSelectedCandidates([FromRoute, Required] Guid jobId, [FromBody] AddSelectedCandidatesRequest request)
    {
        var command = new AddSelectedCandidatesCommand(jobId, request.SelectedCandidates);
        var jobCandidates = await _mediator.Send(command);

        return Ok(Local.GetJobCandidatesResponse.FromDomain(jobCandidates));
    }

    [HttpPut("api/v1/job-candidates/{jobId}/selected-candidates")]
    [Authorize(CustomPolicies.IsAllowedModifyJob)]
    [ProducesResponseOk]
    public async Task<ActionResult<GetJobCandidatesResponse>> UpdateSelectedCandidatesStage([FromRoute, Required] Guid jobId, [FromBody] UpdateCandidateStageRequest request)
    {
        var command = new UpdateCandidatesStageCommand(jobId, request.Stage, request.CandidateIds);
        var jobCandidates = await _mediator.Send(command);

        return Ok(Local.GetJobCandidatesResponse.FromDomain(jobCandidates));
    }

    [HttpPatch("api/v1/job-candidates/{jobId}/activated-shortlist-email")]
    [Authorize(CustomPolicies.IsAllowedBackOfficeUserModifyJob)]
    [ProducesResponseOk]
    public async Task<ActionResult<GetJobCandidatesResponse>> ActivateShortListViaEmail(
        [FromRoute, Required] Guid jobId, 
        [FromBody] ActivateShortlistViaEmailRequest request)
    {
        var command = new ActivateShortlistViaEmailCommand(jobId, request.Email);
        var jobCandidates = await _mediator.Send(command);

        return Ok(Local.GetJobCandidatesResponse.FromDomain(jobCandidates));
    }

    [HttpPatch("api/v1/job-candidates/{jobId}/activated-shortlist-link")]
    [Authorize(CustomPolicies.IsAllowedBackOfficeUserModifyJob)]
    [ProducesResponseOk]
    public async Task<ActionResult<GetJobCandidatesResponse>> ActivateShortListViaLink(
        [FromRoute, Required] Guid jobId)
    {
        var command = new ActivateShortlistViaLinkCommand(jobId);
        var jobCandidates = await _mediator.Send(command);

        return Ok(Local.GetJobCandidatesResponse.FromDomain(jobCandidates));
    }

    [HttpPost("api/v1/job-candidates/{jobId}/archived-candidates")]
    [Authorize(CustomPolicies.IsAllowedModifyJob)]
    [ProducesResponseOk]
    public async Task<ActionResult<GetJobCandidatesResponse>> ArchiveCandidates([FromRoute, Required] Guid jobId, [FromBody] ArchivedCandidatesRequest request)
    {
        var command = new ArchiveCandidatesCommand(jobId, request.Stage, request.CandidateIds);
        var jobCandidates = await _mediator.Send(command);

        return Ok(Local.GetJobCandidatesResponse.FromDomain(jobCandidates));
    }

    [HttpPost("api/v1/job-candidates/{jobId}/archived-candidates/activated")]
    [Authorize(CustomPolicies.IsAllowedModifyJob)]
    [ProducesResponseOk]
    public async Task<ActionResult<GetJobCandidatesResponse>> ActivateArchiveCandidates([FromRoute, Required] Guid jobId, [FromBody] ActivateArchivedCandidatesRequest request)
    {
        var command = new ActivateArchiveCandidatesCommand(jobId, request.CandidateIds);
        var jobCandidates = await _mediator.Send(command);

        return Ok(Local.GetJobCandidatesResponse.FromDomain(jobCandidates));
    }

    [HttpPut("api/v1/job-candidates/{jobId}/selected-candidates/ranking")]
    [Authorize(CustomPolicies.IsAllowedModifyJob)]
    [ProducesResponseOk]
    public async Task<ActionResult<GetJobCandidatesResponse>> UpdateSelectedCandidatesRanking(
        [FromRoute, Required] Guid jobId,
        [FromBody] UpdateCandidatesRankingRequest request)
    {
        var command = new UpdateCandidatesRankingCommand(jobId, request.CandidatesRanking);
        var jobCandidates = await _mediator.Send(command);

        return Ok(Local.GetJobCandidatesResponse.FromDomain(jobCandidates));
    }

    [HttpPut("api/v1/job-candidates/{jobId}/candidates/{candidateId}/brief")]
    [Authorize(CustomPolicies.IsAllowedModifyJob)]
    [ProducesResponseOk]
    public async Task<ActionResult<GetJobCandidatesResponse>> UpdateBrief(
        [FromRoute, Required] Guid jobId,
        [FromRoute, Required] Guid candidateId,
        [FromBody] UpdateCandidateBriefRequest request)
    {
        var command = new UpdateCandidateBriefCommand(jobId, candidateId, request.Brief);
        var jobCandidates = await _mediator.Send(command);

        return Ok(Local.GetJobCandidatesResponse.FromDomain(jobCandidates));
    }

    [HttpPut("api/v1/job-candidates/{jobId}/selected-candidates/invited-email")]
    [Authorize(CustomPolicies.IsAllowedModifyJob)]
    [ProducesResponseOk]
    public async Task<ActionResult<GetJobCandidatesResponse>> InviteSelectedCandidatesViaEmail(
        [FromRoute, Required] Guid jobId,
        [FromBody] InviteCandidatesRequest request)
    {
        var command = new InviteSelectedCandidateViaEmailCommand(jobId, request.CandidateIds);
        var jobCandidates = await _mediator.Send(command);

        return Ok(Local.GetJobCandidatesResponse.FromDomain(jobCandidates));
    }

    [HttpPut("api/v1/job-candidates/{jobId}/selected-candidates/invited-link")]
    [Authorize(CustomPolicies.IsAllowedModifyJob)]
    [ProducesResponseOk]
    public async Task<ActionResult<GetJobCandidatesResponse>> InviteSelectedCandidatesViaLink(
    [FromRoute, Required] Guid jobId,
    [FromBody] InviteCandidatesRequest request)
    {
        var command = new InviteSelectedCandidateViaLinkCommand(jobId, request.CandidateIds);
        var jobCandidates = await _mediator.Send(command);

        return Ok(Local.GetJobCandidatesResponse.FromDomain(jobCandidates));
    }
}
