using API.Customization.Authorization.Constants;
using API.Customization.Controllers.Attributes;
using API.Customization.Pagination;
using Companies.API.Models;
using Companies.Application.Commands.Companies;
using Companies.Application.Queries;
using Contracts.Company.Responses;
using Contracts.Shared.Services.Company.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Local = Companies.Application.Contracts.Company.Responses;
using Enums = Domain.Seedwork.Enums;

namespace Companies.API.Controllers;

public class CompaniesController : AuthorizedCompanyServiceController
{
    private readonly IMediator _mediator;

    public CompaniesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("api/v1/companies/search", Name = nameof(GetAllCompanies))]
    [Authorize(CustomPolicies.IsAllowedReadPublicCompany)]
    [ProducesResponseOk]
    public async Task<ActionResult<PagedResponse<GetCompanySearchResponse>>> GetAllCompanies([FromQuery] CompanySearchFilters filterParams)
    {
        var result = await _mediator.Send(new GetCompaniesByNameOrNumberQuery(
            filterParams.Search,
            filterParams.PageNumber,
            filterParams.PageSize,
            null,
            Scope));

        if (!result.ExistsInternally)
        {
            result = await _mediator.Send(new GetCompaniesFromDanishRcQuery(
            filterParams.Search,
            filterParams.PageSize));
        }

        var pageResponse = new PagedResponse<GetCompanySearchResponse>(
            result.Count,
            result.Companies,
            filterParams.PageNumber,
            filterParams.PageSize,
            Url.RouteUrl(nameof(GetAllCompanies))!,
            Request.QueryString.ToString());

        return Ok(pageResponse);
    }

    [HttpGet("api/v1/companies", Name = nameof(GetCompanies))]
    [Authorize(CustomPolicies.IsAllowedReadCompany)] 
    [ProducesResponseOk]
    public async Task<ActionResult<PagedResponse<GetCompanyBriefResponse>>> GetCompanies([FromQuery] CompanyFilters filterParams)
    {
        var result = await _mediator.Send(new GetCompaniesQuery(
            filterParams.Longitude,
            filterParams.Latitude,
            filterParams.RadiusInKm,
            filterParams.PageNumber,
            filterParams.PageSize,
            filterParams.Search,
            filterParams.Companies,
            filterParams.Industries,
            filterParams.Statuses));

        var pageResponse = new PagedResponse<GetCompanyBriefResponse>(
            result.Count,
            result.Companies,
            filterParams.PageNumber,
            filterParams.PageSize,
            Url.RouteUrl(nameof(GetCompanies))!,
            Request.QueryString.ToString());

        return Ok(pageResponse);
    }

    [HttpGet("api/v1/companies/{companyId}")]
    [Authorize(CustomPolicies.IsAllowedReadCompany)]
    [ProducesResponseOk]
    public async Task<ActionResult<GetCompanyResponse>> GetCompanyById([FromRoute] Guid companyId)
    {
        Validate(companyId);

        var query = new GetCompanyByIdQuery(companyId);
        var result = await _mediator.Send(query);

        if (result is null)
        {
            return NotFound(); 
        }

        return Ok(Local.GetCompanyResponse.FromDomain(result));
    }

    [HttpPost("api/v1/companies/registered")]
    [Authorize(CustomPolicies.IsAllowedContactPersonRegisterHimself)]
    [ProducesResponseOk]
    public async Task<ActionResult<GetCompanyResponse>> RegisterCompany([FromBody] RegisterCompanyRequest request)
    {
        var command = new RegisterCompanyCommand(
            request.Name,
            request.RegistrationNumber,
            request.Address,
            request.Person.Email,
            request.Person.FirstName,
            request.Person.LastName,
            request.Person.Phone?.CountryCode,
            request.Person.Phone?.Number,
            request.Person.Position,
            request.Industries);

        var result = await _mediator.Send(command);

        return Ok(Local.GetCompanyResponse.FromDomain(result));
    }

    [HttpPost("api/v1/companies")]
    [Authorize(CustomPolicies.IsAllowedBackOfficeUserModifyCompany)]
    [ProducesResponseOk]
    public async Task<ActionResult<GetCompanyResponse>> CreateCompany([FromBody] CreateCompanyRequest request)
    {
        var command = new CreateCompanyCommand(
            request.Name,
            request.RegistrationNumber,
            request.WebsiteUrl,
            request.LinkedInUrl,
            request.GlassdoorUrl,
            request.Address,
            request.Logo,
            request.Person.Email,
            request.Person.FirstName,
            request.Person.LastName,
            request.Person.Phone?.CountryCode,
            request.Person.Phone?.Number,
            request.Person.Position,
            request.Person.Picture,
            request.Industries,
            UserId,
            Enums.Scope.BackOffice);

        var result = await _mediator.Send(command);

        return Ok(Local.GetCompanyResponse.FromDomain(result));
    }

    [HttpPut("api/v1/companies/{companyId}")]
    [Authorize(CustomPolicies.IsAllowedModifyCompany)]
    [ProducesResponseOk]
    public async Task<ActionResult<GetCompanyResponse>> UpdateCompany([FromRoute] Guid companyId, [FromBody] UpdateCompanyRequest request)
    {
        Validate(companyId);

        var command = new UpdateCompanyCommand(
            companyId,
            request.WebsiteUrl,
            request.LinkedInUrl,
            request.GlassdoorUrl,
            request.Address,
            request.Logo,
            request.Industries);

        var result = await _mediator.Send(command);

        return Ok(Local.GetCompanyResponse.FromDomain(result));
    }

    [HttpPut("api/v1/companies/{companyId}/approved")]
    [Authorize(CustomPolicies.IsAllowedBackOfficeUserModifyCompany)]
    [ProducesResponseNoContent]
    public async Task<ActionResult> ApproveCompany([FromRoute] Guid companyId)
    {
        var command = new ApproveCompanyCommand(companyId);
        await _mediator.Publish(command);

        return NoContent();
    }

    [HttpPut("api/v1/companies/{companyId}/rejected")]
    [Authorize(CustomPolicies.IsAllowedBackOfficeUserModifyCompany)]
    [ProducesResponseNoContent]
    public async Task<ActionResult> RejectCompany([FromRoute] Guid companyId)
    {
        var command = new RejectCompanyCommand(companyId);
        await _mediator.Publish(command);

        return NoContent();
    }
}
