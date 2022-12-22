using API.Customization.Authorization.Constants;
using API.Customization.Controllers;
using API.Customization.Controllers.Attributes;
using API.Customization.Pagination;
using Contracts.Job.Companies.Requests;
using Contracts.Job.Companies.Responses;
using Contracts.Job.Jobs.Requests;
using Contracts.Job.Jobs.Responses;
using Jobs.API.Models.Jobs.Filters;
using Jobs.Application.Commands;
using Jobs.Application.Queries.Jobs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Local = Jobs.Application.Contracts.Jobs.Responses;

namespace Jobs.API.Controllers;

public class JobsController : AuthorizedApiController
{
    private readonly IMediator _mediator;

    public JobsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("api/v1/jobs", Name = nameof(GetJobs))]
    [Authorize]
    [ProducesResponseOk]
    public async Task<ActionResult<PagedResponse<GetJobBriefResponse>>> GetJobs([FromQuery] JobsFilter filterParams)
    {
        // TODO: #3061 Add policy to read jobs.
        if (filterParams.UserId.HasValue && UserId != filterParams.UserId)
        {
            return Unauthorized($"User {UserId} is not allowed to access data of user {filterParams.UserId} ");
        }

        var result = await _mediator.Send(new GetJobsQuery(
            filterParams.UserId,
            filterParams.Longitude,
            filterParams.Latitude,
            filterParams.RadiusInKm,
            filterParams.AssignedEmployees,
            filterParams.Companies,
            filterParams.Positions,
            filterParams.DeadLineDate,
            filterParams.WorkTypes,
            filterParams.JobStages,
            filterParams.ExcludedJobIds,
            filterParams.Owner,
            filterParams.CreatedAt,
            filterParams.OrderBy,
            filterParams.JobIds,
            filterParams.PageNumber,
            filterParams.PageSize,
            CompanyId,
            Scope,
            filterParams.Search));

        var pageResponse = new PagedResponse<GetJobBriefResponse>(
            result.Count,
            result.Jobs,
            filterParams.PageNumber,
            filterParams.PageSize,
            Url.RouteUrl(nameof(GetJobs))!,
            Request.QueryString.ToString());

        return Ok(pageResponse);
    }

    [HttpPost("api/v1/jobs/initialization")]
    [Authorize(CustomPolicies.IsAllowedModifyJob)]
    [ProducesResponseOk]
    public async Task<ActionResult<GetJobResponse>> InitializePendingJob([FromBody] InitializePendingJobRequest request)
    {
        var command = new InitializePendingJobCommand(
            request.Company,
            request.Position,
            request.StartDate,
            request.EndDate,
            request.WorkTypes,
            request.IsUrgent);

        var result = await _mediator.Send(command);

        return Ok(Local.GetJobResponse.FromDomain(result));
    }

    [HttpPost("api/v1/jobs")]
    [Authorize(CustomPolicies.IsAllowedModifyJob)]
    [ProducesResponseOk]
    public async Task<ActionResult<GetCreatedJobResponse>> CreateJob([FromBody] CreateJobRequest request)
    {
        var command = new CreateJobCommand(
            request.Company,
            request.Owner,
            request.Position,
            request.DeadLineDate,
            request.Description,
            request.StartDate,
            request.EndDate,
            request.WeeklyWorkHours,
            request.Currency,
            request.Freelance,
            request.Permanent,
            request.YearExperience,
            request.IsPriority,
            request.IsUrgent,
            request.WorkingHourTypes,
            request.WorkTypes,
            request.AssignedEmployees,
            request.Skills,
            request.Industries,
            request.Seniorities,
            request.Languages,
            request.Formats,
            request.InteresedCandidates,
            request.InterestedLinkedIns);

        var result = await _mediator.Send(command);

        return Ok(new GetCreatedJobResponse { JobId = result });
    }

    [HttpGet("api/v1/jobs/{jobId}")]
    [Authorize]
    [ProducesResponseOk]
    public async Task<ActionResult<GetJobResponse>> GetJob([FromRoute, Required] Guid jobId)
    {
        // TODO: #3061 Add policy to read job.
        var job = await _mediator.Send(new GetJobQuery(jobId, CompanyId, Scope));

        return Ok(Local.GetJobResponse.FromDomain(job));
    }

    [HttpPut("api/v1/jobs/{jobId}")]
    [Authorize(CustomPolicies.IsAllowedModifyJob)]
    [ProducesResponseOk]
    public async Task<ActionResult<GetJobResponse>> UpdateJob([FromRoute, Required] Guid jobId, [FromBody] UpdateJobRequest request)
    {
        var command = new UpdateJobCommand(
            jobId,
            request.Owner,
            request.Position,
            request.DeadLineDate,
            request.Description,
            request.StartDate,
            request.EndDate,
            request.WeeklyWorkHours,
            request.Currency,
            request.Freelance,
            request.Permanent,
            request.YearExperience,
            request.IsPriority,
            request.IsUrgent,
            request.WorkingHourTypes,
            request.WorkTypes,
            request.AssignedEmployees,
            request.Skills,
            request.Industries,
            request.Seniorities,
            request.Languages,
            request.Formats,
            request.InteresedCandidates,
            request.InterestedLinkedIns,
            Scope);

        var job = await _mediator.Send(command);

        return Ok(Local.GetJobResponse.FromDomain(job));
    }

    [HttpPut("api/v1/jobs/{jobId}/company")]
    [Authorize(CustomPolicies.IsAllowedModifyJob)]
    [ProducesResponseOk]
    public async Task<ActionResult<GetUpdateCompanyResponse>> UpdateCompany([FromRoute, Required] Guid jobId, [FromBody] UpdateJobCompanyRequest request)
    {
        var command = new UpdateCompanyCommand(
                jobId,
                request.Address,
                request.Description,
                request.ContactPersonsToAdd,
                request.ContactPersonsToRemove,
                request.MainContactId);

        var company = await _mediator.Send(command);

        return Ok(Local.GetUpdateCompanyResponse.FromDomain(jobId, company));
    }

    [HttpPut("api/v1/jobs/{jobId}/shared-email")]
    [Authorize(CustomPolicies.IsAllowedModifyJob)]
    [ProducesResponseOk]
    public async Task<ActionResult<ShareJobViaEmailResponse>> ShareJobViaEmail(
        [FromRoute, Required] Guid jobId,
        [FromBody] ShareJobViaEmailRequest request)
    {
        var command = new ShareJobViaEmailCommand(jobId, request.ReceiverEmail);
        var shareDate = await _mediator.Send(command);

        var result = new ShareJobViaEmailResponse
        {
            Date = shareDate
        };
        return Ok(result);
    }

    [HttpPut("api/v1/jobs/{jobId}/shared-link")]
    [Authorize(CustomPolicies.IsAllowedModifyJob)]
    [ProducesResponseOk]
    public async Task<ActionResult<ShareJobViaLinkResponse>> ShareJobViaLink([FromRoute, Required] Guid jobId)
    {
        var command = new ShareJobViaLinkCommand(jobId);
        var result = await _mediator.Send(command);

        return Ok(result);
    }

    [HttpPut("api/v1/jobs/{jobId}/published")]
    [Authorize(CustomPolicies.IsAllowedModifyJob)]
    [ProducesResponseNoContent]
    public async Task<ActionResult> PublishJob([FromRoute, Required] Guid jobId)
    {
        var command = new PublishJobCommand(jobId);
        await _mediator.Publish(command);

        return NoContent();
    }

    [HttpPut("api/v1/jobs/{jobId}/search-selection-started")]
    public async Task<ActionResult<JobStageResponse>> StartSearchAndSelection([FromRoute, Required] Guid jobId)
    {
        var command = new StartJobSearchAndSelectionCommand(jobId);
        var jobStage = await _mediator.Send(command);

        return Ok(new JobStageResponse { JobStage = jobStage });
    }

    [HttpPatch("api/v1/jobs/{jobId}/archived")]
    [Authorize(CustomPolicies.IsAllowedModifyJob)]
    [ProducesResponseNoContent]
    public async Task<ActionResult> ArchiveJob([FromRoute, Required] Guid jobId, [FromBody] ArchiveJobRequest request)
    {
        var command = new ArchiveJobCommand(jobId, request.Stage);
        await _mediator.Send(command);

        return NoContent();
    }

    [HttpPatch("api/v1/jobs/{jobId}/activated")]
    [Authorize(CustomPolicies.IsAllowedModifyJob)]
    [ProducesResponseOk]
    public async Task<ActionResult<GetCreatedJobResponse>> ActivateJob([FromRoute, Required] Guid jobId)
    {
        var command = new ActivateJobCommand(jobId);
        var result = await _mediator.Send(command);

        return Ok(new GetCreatedJobResponse { JobId = result });
    }

    [HttpPatch("api/v1/jobs/{jobId}/approved")]
    [Authorize(CustomPolicies.IsAllowedBackOfficeUserModifyJob)]
    [ProducesResponseNoContent]
    public async Task<ActionResult> ApproveJob([FromRoute] Guid jobId)
    {
        var command = new ApproveJobCommand(jobId);
        await _mediator.Publish(command);

        return NoContent();
    }

    [HttpDelete("api/v1/jobs/{jobId}/rejected")]
    [Authorize(CustomPolicies.IsAllowedBackOfficeUserModifyJob)]
    [ProducesResponseNoContent]
    public async Task<ActionResult> RejectJob([FromRoute] Guid jobId)
    {
        var command = new RejectJobCommand(jobId);
        await _mediator.Publish(command);

        return NoContent();
    }
}
