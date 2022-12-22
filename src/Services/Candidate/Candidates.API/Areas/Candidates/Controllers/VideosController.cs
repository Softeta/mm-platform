using API.Customization.Authorization.Constants;
using API.Customization.Controllers;
using API.Customization.Controllers.Attributes;
using Candidates.Infrastructure.Settings;
using Contracts.Shared.Requests;
using Contracts.Shared.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Persistence.Customization.Commands.Files;
using Persistence.Customization.FileStorage;
using Persistence.Customization.FileStorage.Clients.Private;
using Persistence.Customization.TableStorage;

namespace Candidates.API.Areas.Candidates.Controllers
{
    public class VideosController : AuthorizedApiController
    {
        private readonly string _blobVideoContainer;
        private readonly IMediator _mediator;
        private readonly CacheSettings _cacheSettings;

        public VideosController(
            IMediator mediator, 
            IOptions<BlobContainerSettings> candidateContainers,
            IOptions<CacheSettings> cacheSettings)
        {
            _blobVideoContainer = candidateContainers.Value.CandidateVideosContainer;
            _mediator = mediator;
            _cacheSettings = cacheSettings.Value;
        }

        [HttpPost("api/v1/candidates/videos")]
        [Authorize(CustomPolicies.IsAllowedModifyCandidate)]
        [ProducesResponseOk]
        public async Task<ActionResult<FileCacheResponse>> AddCandidateVideo([FromForm] VideoRequest request)
        {
            var command = new UploadFileCommand<IPrivateFileUploadClient>(
                _blobVideoContainer,
                FileCacheTableStorage.Candidate.FilePartitionKey,
                FileCacheTableStorage.Candidate.Category.Video,
                _cacheSettings.FileExpiresInMinutes,
                request.File);

            var result = await _mediator.Send(command);

            var response = new FileCacheResponse
            {
                CacheId = result
            };

            return Ok(response);
        }

        [HttpPut("api/v1/candidates/videos/{cacheId}")]
        [Authorize(CustomPolicies.IsAllowedModifyCandidate)]
        [ProducesResponseOk]
        public async Task<ActionResult<FileCacheResponse>> UpdateCandidateVideo(
            [FromRoute] Guid cacheId,
            [FromForm] VideoRequest request)
        {
            var command = new UpdateFileCommand<IPrivateFileDeleteClient, IPrivateFileUploadClient>(
                _blobVideoContainer,
                FileCacheTableStorage.Candidate.FilePartitionKey,
                FileCacheTableStorage.Candidate.Category.Video,
                _cacheSettings.FileExpiresInMinutes,
                request.File,
                cacheId);

            var result = await _mediator.Send(command);

            var response = new FileCacheResponse
            {
                CacheId = result
            };

            return Ok(response);
        }

        [HttpDelete("api/v1/candidates/videos/{cacheId}")]
        [Authorize(CustomPolicies.IsAllowedModifyCandidate)]
        [ProducesResponseNoContent]
        public async Task<ActionResult> DeleteCandidateVideo([FromRoute] Guid cacheId)
        {
            var command = new DeleteFileCommand<IPrivateFileDeleteClient>(
                _blobVideoContainer,
                FileCacheTableStorage.Candidate.FilePartitionKey,
                cacheId);

            await _mediator.Publish(command);

            return NoContent();
        }
    }
}
