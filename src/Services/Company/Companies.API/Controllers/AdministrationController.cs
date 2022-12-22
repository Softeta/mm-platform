using API.Customization.Authorization.Constants;
using API.Customization.Controllers.Attributes;
using Companies.Application.Commands.RegistryCenter;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Companies.API.Controllers;

public class AdministrationController : AuthorizedCompanyServiceController
{
    private readonly IMediator _mediator;

    public AdministrationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("api/v1/companies/registry-center/sync")]
    [Authorize(CustomPolicies.IsAdministrationAction)]
    [ProducesResponseNoContent]
    public async Task<IActionResult> SyncRegistryCenterSync()
    {
        await _mediator.Publish(new SyncDanishCompaniesCommand());

        return NoContent();
    }

    [HttpPost("api/v1/companies/registry-center/fill-indexes")]
    [Authorize(CustomPolicies.IsAdministrationAction)]
    [ProducesResponseNoContent]
    public async Task<IActionResult> FillSearchIndexes()
    {
        await _mediator.Publish(new FillSearchIndexesCommand());

        return NoContent();
    }
}
