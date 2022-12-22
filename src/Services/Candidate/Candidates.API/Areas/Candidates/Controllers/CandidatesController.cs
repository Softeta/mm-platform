using API.Customization.Authorization.Constants;
using API.Customization.Controllers.Attributes;
using API.Customization.Pagination;
using Candidates.API.Areas.Candidates.Models.Filters;
using Candidates.Application.Commands;
using Candidates.Application.Queries;
using Candidates.Application.Validations;
using Contracts.Candidate.Candidates.Requests;
using Contracts.Candidate.Candidates.Responses;
using Contracts.Candidate.Notes.Requests;
using Contracts.Candidate.Notes.Responses;
using Contracts.Shared.Requests;
using Domain.Seedwork.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence.Customization.FileStorage.Clients.Private;
using Local = Candidates.Application.Contracts.Candidates.Responses;

namespace Candidates.API.Areas.Candidates.Controllers;

public class CandidatesController : AuthorizedCandidateServiceController
{
    private readonly IMediator _mediator;
    private readonly IPrivateBlobClient _privateBlobClient;

    public CandidatesController(
        IMediator mediator,
        IPrivateBlobClient privateBlobClient)
    {
        _mediator = mediator;
        _privateBlobClient = privateBlobClient;
    }

    [HttpGet("api/v1/candidates/self")]
    [Authorize(CustomPolicies.IsAllowedCandidateModifyHimself)]
    [ProducesResponseOk]
    public async Task<ActionResult<GetCandidateResponse>> GetSelf()
    {
        var query = new GetCandidateByExternalIdQuery(UserId);

        var result = await _mediator.Send(query);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(Local.GetCandidateResponse.FromDomain(result, _privateBlobClient));
    }

    [HttpPost("api/v1/candidates/register")]
    [Authorize(CustomPolicies.IsAllowedCandidateModifyHimself)]
    [ProducesResponseOk]
    public async Task<ActionResult<GetCandidateResponse>> RegisterMyself([FromBody] RegisterMyselfRequest request)
    {
        if (UserId != request.ExternalId)
        {
            return Forbid();
        }

        var query = new RegisterMyselfCommand(
            request.Email,
            request.ExternalId,
            request.SystemLanguage,
            request.AcceptTermsAndConditions,
            request.AcceptMarketingAcknowledgement);

        var result = await _mediator.Send(query);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(Local.GetCandidateResponse.FromDomain(result, _privateBlobClient));
    }

    [HttpGet("api/v1/candidates/{candidateId}")]
    [Authorize(CustomPolicies.IsAllowedReadSingleCandidate)]
    [ProducesResponseOk]
    public async Task<ActionResult<GetCandidateResponse>> GetCandidateById([FromRoute] Guid candidateId)
    {
        if (Scope == CustomScopes.FrontOffice.Company)
        {
            await _mediator.Publish(new ValidateIsAllowedReadCandidateValidation(CompanyId, candidateId));
        }

        var query = new GetCandidateByIdQuery(candidateId);
        var result = await _mediator.Send(query);

        if (result is null)
        {
            throw new NotFoundException("Candidate not found", ErrorCodes.NotFound.CandidateNotFound);
        }

        Validate(result.ExternalId);

        return Ok(Local.GetCandidateResponse.FromDomain(result, _privateBlobClient));
    }

    [HttpGet("api/v1/candidates", Name = nameof(GetCandidates))]
    [Authorize(CustomPolicies.IsAllowedBackOfficeUserReadCandidate)]
    [ProducesResponseOk]
    public async Task<ActionResult<PagedResponse<GetCandidateBriefResponse>>> GetCandidates([FromQuery] CandidatesFilter filterParams)
    {
        var result = await _mediator.Send(new GetCandidatesQuery(
            filterParams.Candidates,
            filterParams.Name,
            filterParams.Positions,
            filterParams.Longitude,
            filterParams.Latitude,
            filterParams.RadiusInKm,
            filterParams.OpenForOpportunities,
            filterParams.IsFreelance,
            filterParams.IsPermanent,
            filterParams.AvailableFrom,
            filterParams.HourlyBudgetTo,
            filterParams.MonthlyBudgetTo,
            filterParams.Currency,
            filterParams.Statuses,
            filterParams.JobId,
            filterParams.OrderBy,
            filterParams.PageNumber,
            filterParams.PageSize,
            filterParams.Search));

        var pageResponse = new PagedResponse<GetCandidateBriefResponse>(
            result.Count,
            result.Candidates,
            filterParams.PageNumber,
            filterParams.PageSize,
            Url.RouteUrl(nameof(GetCandidates))!,
            Request.QueryString.ToString());

        return Ok(pageResponse);
    }

    [HttpPost("api/v1/candidates")]
    [Authorize(CustomPolicies.IsAllowedBackOfficeUserModifyCandidate)]
    [ProducesResponseOk]
    public async Task<ActionResult<GetCandidateResponse>> InitializeCandidate([FromBody] InitializeCandidateRequest request)
    {
        var command = new InitializeCandidateCommand(
            request.Email,
            request.FirstName,
            request.LastName,
            request.Picture,
            request.CurrentPosition,
            request.BirthDate,
            request.OpenForOpportunities,
            request.LinkedInUrl,
            request.PersonalWebsiteUrl,
            request.Address,
            request.StartDate,
            request.EndDate,
            request.Currency,
            request.WeeklyWorkHours,
            request.Freelance,
            request.Permanent,
            request.Phone,
            request.YearsOfExperience,
            request.Bio,
            request.CurriculumVitae,
            request.Video,
            request.WorkingHourTypes,
            request.WorkTypes,
            request.Formats,
            request.Industries,
            request.Skills,
            request.DesiredSkills,
            request.Languages,
            request.Hobbies,
            request.Courses,
            request.Educations,
            request.WorkExperiences);

        var result = await _mediator.Send(command);

        return Ok(Local.GetCandidateResponse.FromDomain(result, _privateBlobClient));
    }

    [HttpPut("api/v1/candidates/{candidateId}")]
    [Authorize(CustomPolicies.IsAllowedModifyCandidate)]
    [ProducesResponseOk]
    public async Task<ActionResult<GetCandidateResponse>> UpdateCandidate([FromRoute] Guid candidateId, [FromBody] UpdateCandidateRequest request)
    {
        var command = new UpdateCandidateCommand(
            candidateId,
            request.Email,
            request.FirstName,
            request.LastName,
            request.CurrentPosition,
            request.BirthDate,
            request.OpenForOpportunities,
            request.LinkedInUrl,
            request.PersonalWebsiteUrl,
            request.Address,
            request.StartDate,
            request.EndDate,
            request.Currency,
            request.WeeklyWorkHours,
            request.Freelance,
            request.Permanent,
            request.Phone,
            request.YearsOfExperience,
            request.Bio,
            request.CurriculumVitae,
            request.Video,
            request.Picture,
            request.ActivityStatuses,
            request.WorkingHourTypes,
            request.WorkTypes,
            request.Formats,
            request.Industries,
            request.Skills,
            request.DesiredSkills,
            request.Languages,
            request.Hobbies,
            UserId,
            Scope);

        var result = await _mediator.Send(command);

        return Ok(Local.GetCandidateResponse.FromDomain(result, _privateBlobClient));
    }

    [HttpPut("api/v1/candidates/{candidateId}/open-for-opportunities")]
    [Authorize(CustomPolicies.IsAllowedModifyCandidate)]
    [ProducesResponseNoContent]
    public async Task<ActionResult> UpdateCandidateOpenForOpportunities(
        [FromRoute] Guid candidateId, 
        [FromBody] UpdateCandidateOpenForOpportunitiesRequest request)
    {
        var command = new UpdateCandidateOpenForOpportunitiesCommand(
            candidateId,
            request.OpenForOpportunities,
            UserId, 
            Scope);

        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPut("api/v1/candidates/{candidateId}/verify/{key}")]
    [ProducesResponseOk]
    public async Task<ActionResult<GetCandidateResponse>> VerifyCandidate([FromRoute] Guid candidateId, [FromRoute] Guid key)
    {
        var command = new VerifyCandidateCommand(candidateId, key);
        var result = await _mediator.Send(command);

        return Ok(Local.GetCandidateResponse.FromDomain(result, _privateBlobClient));
    }

    [HttpPut("api/v1/candidates/request-verification")]
    [Authorize(CustomPolicies.IsAllowedCandidateModifyHimself)]
    [ProducesResponseNoContent]
    public async Task<ActionResult> RequestEmailVerification()
    {
        var command = new RequestEmailVerificationCommand(UserId);
        var result = await _mediator.Send(command);

        if (result is null)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPut("api/v1/candidates/{candidateId}/notes")]
    [Authorize(CustomPolicies.IsAllowedBackOfficeUserModifyCandidate)]
    [ProducesResponseOk]
    public async Task<ActionResult<NoteResponse>> UpdateNote(
        [FromRoute] Guid candidateId, 
        [FromBody] UpdateNoteRequest request)
    {
        var command = new UpdateCandidateNoteCommand(
            candidateId,
            UserId,
            Scope,
            request.Value,
            request.EndDate);

        var note = await _mediator.Send(command);

        return Ok(new NoteResponse { 
            Value = note?.Value, 
            EndDate = note?.EndDate
        });
    }

    [HttpPut("api/v1/candidates/{candidateId}/approved")]
    [Authorize(CustomPolicies.IsAllowedBackOfficeUserModifyCandidate)]
    [ProducesResponseNoContent]
    public async Task<ActionResult> ApproveCandidate([FromRoute] Guid candidateId)
    {
        var command = new ApproveCandidateCommand(candidateId);
        await _mediator.Publish(command);

        return NoContent();
    }

    [HttpPut("api/v1/candidates/{candidateId}/rejected")]
    [Authorize(CustomPolicies.IsAllowedBackOfficeUserModifyCandidate)]
    [ProducesResponseNoContent]
    public async Task<ActionResult> RejectCandidate([FromRoute] Guid candidateId)
    {
        var command = new RejectCandidateCommand(candidateId);
        await _mediator.Publish(command);

        return NoContent();
    }

    [HttpPut("api/v1/candidates/{candidateId}/core-information/completed")]
    [Authorize(CustomPolicies.IsAllowedModifyCandidate)]
    [ProducesResponseOk]
    public async Task<ActionResult> CompleteCoreInformation([FromRoute] Guid candidateId)
    {
        var command = new CompleteCandidateCoreInformationCommand(candidateId, UserId, Scope);
        var result = await _mediator.Send(command);

        return Ok(Local.GetCandidateResponse.FromDomain(result, _privateBlobClient));
    }

    [HttpPut("api/v1/candidates/{candidateId}/legal-terms")]
    [Authorize(CustomPolicies.IsAllowedModifyCandidate)]
    [ProducesResponseOk]
    public async Task<ActionResult<GetCandidateResponse>> UpdateLegalTerms(
        [FromRoute] Guid candidateId,
        [FromBody] UpdateLegalTermsRequest request)
    {
        var command = new UpdateCandidateLegalTermsCommand(candidateId, UserId, Scope,
            request.TermsAgreement, request.MarketingAgreement);

        var result = await _mediator.Send(command);

        return Ok(Local.GetCandidateResponse.FromDomain(result, _privateBlobClient));
    }

    [HttpPut("api/v1/candidates/{candidateId}/settings")]
    [Authorize(CustomPolicies.IsAllowedModifyCandidate)]
    [ProducesResponseOk]
    public async Task<ActionResult<GetCandidateResponse>> UpdateSettings(
        [FromRoute] Guid candidateId,
        [FromBody] UpdateSettingsRequest request)
    {
        var command = new UpdateCandidateSettingsCommand(candidateId, UserId, Scope,
            request.SystemLanguage, request.MarketingAcknowledgement);

        var result = await _mediator.Send(command);

        return Ok(Local.GetCandidateResponse.FromDomain(result, _privateBlobClient));
    }
}
