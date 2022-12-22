using API.Customization.Controllers;
using API.Customization.Controllers.Attributes;
using Candidates.Application.Commands.Tests;
using Candidates.Infrastructure.Constants;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph.ExternalConnectors;

namespace Candidates.API.Areas.Tests.Controllers
{
    public class NotificationsController : AuthorizedApiController
    {  
        private readonly IMediator _mediator;

        public NotificationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("api/v1/candidate-tests/{candidateId}/{externalId}/notifications")]
        [ProducesResponseOk]
        public async Task<ActionResult> Notify([FromRoute] Guid candidateId, [FromRoute] Guid externalId, [FromBody] dynamic request)
        {
            var command = new AcceptNotificationCommand(candidateId, externalId, request.ToString());
            await _mediator.Publish(command);

            return Accepted();
        }
    }
}
