using API.Customization.Authorization.Constants;
using API.Customization.Controllers;
using API.Customization.Controllers.Attributes;
using Contracts.Job.JobCandidatesFilters.Requests;
using Contracts.Job.JobCandidatesFilters.Responses;
using Jobs.Application.Commands.Filters;
using Jobs.Application.Queries.JobCandidatesFilters;
using Jobs.Domain.Aggregates.JobCandidatesAggregate.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Jobs.API.Controllers
{
    public class JobCandidatesFiltersController : AuthorizedApiController
    {
        private readonly IMediator _mediator;

        public JobCandidatesFiltersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("api/v1/job-candidates/filters/{jobId}")]
        [Authorize(CustomPolicies.IsAllowedBackOfficeUserModifyJob)]
        [ProducesResponseOk]
        public async Task<ActionResult> GetFilters([FromRoute, Required] Guid jobId)
        {
            var command = new GetJobCandidatesFiltersQuery(jobId, UserId);
            var filters = await _mediator.Send(command);

            return Ok(filters.Select(ToResponse));
        }

        [HttpPost("api/v1/job-candidates/filters/{jobId}")]
        [Authorize(CustomPolicies.IsAllowedBackOfficeUserModifyJob)]
        [ProducesResponseOk]
        public async Task<ActionResult> AddFilter([FromRoute, Required] Guid jobId, [FromBody] AddCandidatesFilterRequest request)
        {
            var command = new AddCandidatesFilterCommand(jobId, UserId, request.Index, request.Title, request.FilterParams);
            var filter = await _mediator.Send(command);
            return Ok(ToResponse(filter));
        }

        [HttpDelete("api/v1/job-candidates/filters/{jobId}/{index}")]
        [Authorize(CustomPolicies.IsAllowedBackOfficeUserModifyJob)]
        [ProducesResponseNoContent]
        public async Task<ActionResult> DeleteFilter([FromRoute, Required] Guid jobId, [FromRoute, Required] int index)
        {
            var command = new DeleteCandidatesFilterCommand(jobId, UserId, index);
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPatch("api/v1/job-candidates/filters/{jobId}/{index}")]
        [Authorize(CustomPolicies.IsAllowedBackOfficeUserModifyJob)]
        [ProducesResponseNoContent]
        public async Task<ActionResult> UpdateFilterTitle(
            [FromRoute, Required] Guid jobId, 
            [FromRoute, Required] int index,
            [FromBody] UpdateCandidatesFilterTitleRequest request)
        {
            var command = new UpdateCandidatesFilterTitleCommand(jobId, UserId, index, request.Title);
            var filter = await _mediator.Send(command);
            return Ok(ToResponse(filter));
        }

        private static GetCandidatesFilterResponse ToResponse(JobCandidatesFilter filterEntity)
        {
            return new GetCandidatesFilterResponse
            {
                FilterParams = filterEntity.FilterParams,
                Index = filterEntity.Index,
                Title = filterEntity.Title
            };
        }
    }
}
