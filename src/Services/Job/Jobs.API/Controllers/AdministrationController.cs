using API.Customization.Authorization.Constants;
using API.Customization.Controllers;
using API.Customization.Controllers.Attributes;
using Jobs.API.Models.Administration.Requests;
using Jobs.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jobs.API.Controllers;

public class AdministrationController : AuthorizedApiController
{
    private readonly IMediator _mediator;

    public AdministrationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("api/v1/jobs-candidates/selected-candidates/sync")]
    [Authorize(CustomPolicies.IsAdministrationAction)]
    [ProducesResponseNoContent]
    public async Task<ActionResult> SyncSelectedCandidates([FromBody] JobSelectedCandidatesSyncRequest request)
    {
        var command = new SyncSelectedCandidatesCommand(request.JobIds);
        await _mediator.Publish(command);

        return NoContent();
    }

    [HttpPost("api/v1/jobs-candidates/archived-candidates/sync")]
    [Authorize(CustomPolicies.IsAdministrationAction)]
    [ProducesResponseNoContent]
    public async Task<ActionResult> SyncArchivedCandidates([FromBody] JobArchivedCandidatesSyncRequest request)
    {
        var command = new SyncArchivedCandidatesCommand(request.JobIds);
        await _mediator.Publish(command);

        return NoContent();
    }

    [HttpPost("api/v1/jobs/updated/sync")]
    [Authorize(CustomPolicies.IsAdministrationAction)]
    [ProducesResponseNoContent]
    public async Task<ActionResult> SyncJobsUpdated([FromBody] JobsUpdatedSyncRequest request)
    {
        var command = new SyncJobsAsUpdatedCommand(request.JobIds);
        await _mediator.Publish(command);

        return NoContent();
    }
}
