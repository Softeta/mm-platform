using API.Customization.Authorization.Constants;
using API.Customization.Controllers.Attributes;
using API.Customization.Pagination;
using Companies.API.Models;
using Companies.Application.Commands.ContactPersons;
using Companies.Application.Queries;
using Contracts.Company.Responses.ContactPersons;
using Contracts.Shared.Requests;
using Contracts.Shared.Services.Company.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Local = Companies.Application.Contracts.Company.Responses;

namespace Companies.API.Controllers;

public class ContactPersonsController : AuthorizedCompanyServiceController
{
    private readonly IMediator _mediator;

    public ContactPersonsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("api/v1/companies/contact-persons/self")]
    [Authorize(CustomPolicies.IsAllowedContactPersonRegisterHimself)]
    [ProducesResponseOk]
    public async Task<ActionResult<GetContactPersonResponse>> GetMyself()
    {
        var result = await _mediator.Send(new GetContactPersonByExternalIdQuery(UserId));

        if (result is null)
        {
            return NotFound();
        }

        return Ok(Local.GetContactPersonResponse.FromDomain(result));
    }

    [HttpPost("api/v1/companies/contact-persons/register")]
    [Authorize(CustomPolicies.IsAllowedContactPersonRegisterHimself)]
    [ProducesResponseOk]
    public async Task<ActionResult<GetContactPersonResponse>> RegisterMyself([FromBody] RegisterMyselfRequest request)
    {
        if (UserId != request.ExternalId)
        {
            return Forbid();
        }

        var command = new RegisterMyselfCommand(
            request.Email,
            request.ExternalId,
            request.SystemLanguage,
            request.AcceptTermsAndConditions,
            request.AcceptMarketingAcknowledgement);

        var result = await _mediator.Send(command);

        return Ok(Local.GetContactPersonResponse.FromDomain(result));
    }

    [HttpPost("api/v1/companies/{companyId}/contact-persons")]
    [Authorize(CustomPolicies.IsAllowedModifyCompany)]
    [ProducesResponseOk]
    public async Task<ActionResult<GetContactPersonResponse>> AddCompanyContactPerson(
        [FromRoute] Guid companyId,
        [FromBody] CreatePersonRequest request)
    {
        Validate(companyId);

        var command = new AddCompanyContactPersonCommand(
            companyId,
            request,
            UserId,
            CustomScopes.GetScopeEnumValue(Scope));

        var result = await _mediator.Send(command);

        return Ok(Local.GetContactPersonResponse.FromDomain(result));
    }

    [HttpPut("api/v1/companies/{companyId}/contact-persons/{contactId}")]
    [Authorize(CustomPolicies.IsAllowedModifyCompany)]
    [ProducesResponseOk]
    public async Task<ActionResult<GetContactPersonResponse>> UpdateCompanyContactPerson(
        [FromRoute] Guid companyId,
        [FromRoute] Guid contactId,
        [FromBody] UpdatePersonRequest request)
    {
        Validate(companyId);
        ValidateContactUpdate(contactId);

        var command = new UpdateContactPersonCommand(
            companyId,
            contactId,
            request);

        var result = await _mediator.Send(command);

        return Ok(Local.GetContactPersonResponse.FromDomain(result));
    }

    [HttpPut("api/v1/companies/{companyId}/contact-persons/{contactId}/verify/{key}")]
    [ProducesResponseOk]
    public async Task<ActionResult<GetContactPersonResponse>> VerifyContactPerson(
        [FromRoute] Guid companyId,
        [FromRoute] Guid contactId,
        [FromRoute] Guid key)
    {
        var command = new VerifyContactPersonCommand(companyId, contactId, key);
        var result = await _mediator.Send(command);

        return Ok(Local.GetContactPersonResponse.FromDomain(result));
    }

    [HttpPut("api/v1/companies/contact-persons/request-verification")]
    [Authorize(CustomPolicies.IsAllowedContactPersonRegisterHimself)]
    [ProducesResponseNoContent]
    public async Task<ActionResult> RequestEmailVerification()
    {
        var command = new RequestEmailVerificationCommand(UserId);
        await _mediator.Publish(command);

        return NoContent();
    }

    [HttpPut("api/v1/companies/{companyId}/contact-persons/{contactId}/rejected")]
    [Authorize(CustomPolicies.IsAllowedModifyCompany)]
    [ProducesResponseNoContent]
    public async Task<ActionResult<GetContactPersonResponse>> Reject(
        [FromRoute] Guid companyId,
        [FromRoute] Guid contactId)
    {
        var command = new RejectContactPersonCommand(companyId, contactId);
        await _mediator.Publish(command);

        return NoContent();
    }

    [HttpPut("api/v1/companies/{companyId}/contact-persons/{contactId}/legal-terms")]
    [Authorize(CustomPolicies.IsAllowedModifyCompany)]
    [ProducesResponseOk]
    public async Task<ActionResult<GetContactPersonResponse>> UpdateLegalTerms(
        [FromRoute] Guid companyId,
        [FromRoute] Guid contactId,
        [FromBody] UpdateLegalTermsRequest request)
    {
        Validate(companyId, contactId);

        var command = new UpdateContactPersonLegalTermsCommand(companyId, contactId, request.TermsAgreement, request.MarketingAgreement);
        var result = await _mediator.Send(command);

        return Ok(Local.GetContactPersonResponse.FromDomain(result));
    }

    [HttpPut("api/v1/companies/{companyId}/contact-persons/{contactId}/settings")]
    [Authorize(CustomPolicies.IsAllowedModifyCompany)]
    [ProducesResponseOk]
    public async Task<ActionResult<GetContactPersonResponse>> UpdateSettings(
    [FromRoute] Guid companyId,
    [FromRoute] Guid contactId,
    [FromBody] UpdateSettingsRequest request)
    {
        Validate(companyId, contactId);

        var command = new UpdateContactPersonSettingsCommand(
            companyId,
            contactId,
            request.SystemLanguage, 
            request.MarketingAcknowledgement);

        var result = await _mediator.Send(command);

        return Ok(Local.GetContactPersonResponse.FromDomain(result));
    }

    [HttpGet("api/v1/companies/{companyId}/contact-persons", Name = nameof(GetContactPersons))]
    [Authorize(CustomPolicies.IsAllowedBackOfficeUserModifyCompany)]
    [ProducesResponseOk]
    public async Task<ActionResult<PagedResponse<GetContactPersonBriefResponse>>> GetContactPersons(
        [FromRoute] Guid companyId,
        [FromQuery] ContactPersonFilters filterParams)
    {
        var result = await _mediator.Send(new GetContactPersonsQuery(
            companyId,
            filterParams.ContactPersons,
            filterParams.PageNumber,
            filterParams.PageSize));

        var pageResponse = new PagedResponse<GetContactPersonBriefResponse>(
            result.Count,
            result.ContactPersons,
            filterParams.PageNumber,
            filterParams.PageSize,
            Url.RouteUrl(nameof(GetContactPersons))!,
            Request.QueryString.ToString());

        return Ok(pageResponse);
    }
}
