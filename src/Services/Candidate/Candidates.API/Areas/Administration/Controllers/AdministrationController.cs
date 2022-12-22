using API.Customization.Authorization.Constants;
using API.Customization.Controllers;
using API.Customization.Controllers.Attributes;
using Candidates.API.Areas.Administration.Models.Requests;
using Candidates.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Candidates.API.Areas.Administration.Controllers;

public class AdministrationController : AuthorizedApiController
{
    private readonly IMediator _mediator;

    public AdministrationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("api/v1/candidates/sync")]
    [Authorize(CustomPolicies.IsAdministrationAction)]
    [ProducesResponseNoContent]
    public async Task<ActionResult> SyncCandidates([FromBody] CandidatesSyncRequest request)
    {
        await _mediator.Publish(new SyncCandidatesCommand(request.CandidateIds));

        return NoContent();
    }

    [HttpPost("api/v1/candidates/sync/all")]
    [Authorize(CustomPolicies.IsAdministrationAction)]
    [ProducesResponseNoContent]
    public async Task<ActionResult> SyncAllCandidates()
    {
        await _mediator.Publish(new SyncAllCandidatesCommand());

        return NoContent();
    }
}
