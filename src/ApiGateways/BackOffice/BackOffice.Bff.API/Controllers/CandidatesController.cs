using API.Customization.Authorization.Constants;
using API.Customization.Controllers;
using API.Customization.Controllers.Attributes;
using BackOffice.Application.Queries;
using BackOffice.Bff.API.Models.Candidate.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackOffice.Bff.API.Controllers
{
    public class CandidatesController : AuthorizedApiController
    {
        private readonly IMediator _mediator;

        public CandidatesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("api/v1/candidates/parsed-cv")]
        [Authorize(CustomPolicies.IsAllowedModifyCandidate)]
        [ProducesResponseOk]
        public async Task<IActionResult> ParseCv([FromBody] ParseCvRequest data, CancellationToken cancellationToken)
        {
            var query = new GetCandidateFromCvQuery(data.FileUri, data.FileCacheId, data.Source);
            var response = await _mediator.Send(query, cancellationToken);

            if (response is null)
            {
                return BadRequest("Document analyze failed");
            }

            return Ok(response);
        }
    }
}
