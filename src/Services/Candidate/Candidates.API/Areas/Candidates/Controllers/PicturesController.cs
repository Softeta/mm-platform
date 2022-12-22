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
using Persistence.Customization.FileStorage.Clients.Public;
using Persistence.Customization.TableStorage;

namespace Candidates.API.Areas.Candidates.Controllers
{
    public class PicturesController : AuthorizedApiController
    {
        private readonly string _blobPictureContainer;
        private readonly IMediator _mediator;
        private readonly CacheSettings _cacheSettings;

        public PicturesController(
            IMediator mediator, 
            IOptions<BlobContainerSettings> candidateContainers,
            IOptions<CacheSettings> cacheSettings)
        {
            _blobPictureContainer = candidateContainers.Value.CandidatePicturesContainer;
            _mediator = mediator;
            _cacheSettings = cacheSettings.Value;
        }

        [HttpPost("api/v1/candidates/pictures")]
        [Authorize(CustomPolicies.IsAllowedModifyCandidate)]
        [ProducesResponseOk]
        public async Task<ActionResult<FileCacheResponse>> AddCandidatePicture([FromForm] ImageRequest request)
        {
            var command = new UploadImageCommand<IPublicFileUploadClient>(
                request.File,
                _blobPictureContainer,
                FileCacheTableStorage.Candidate.FilePartitionKey,
                FileCacheTableStorage.Candidate.Category.Picture,
                _cacheSettings.FileExpiresInMinutes);

            var result = await _mediator.Send(command);

            var response = new FileCacheResponse
            {
                CacheId = result
            };

            return Ok(response);
        }

        [HttpPut("api/v1/candidates/pictures/{cacheId}")]
        [Authorize(CustomPolicies.IsAllowedModifyCandidate)]
        [ProducesResponseOk]
        public async Task<ActionResult<FileCacheResponse>> UpdateCandidatePicture(
            [FromRoute] Guid cacheId,
            [FromForm] ImageRequest request)
        {
            var command = new UpdateImageCommand<IPublicFileDeleteClient, IPublicFileUploadClient>(
                _blobPictureContainer,
                FileCacheTableStorage.Candidate.FilePartitionKey,
                FileCacheTableStorage.Candidate.Category.Picture,
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

        [HttpDelete("api/v1/candidates/pictures/{cacheId}")]
        [Authorize(CustomPolicies.IsAllowedModifyCandidate)]
        [ProducesResponseNoContent]
        public async Task<ActionResult> DeleteCandidatePicture([FromRoute] Guid cacheId)
        {
            var command = new DeleteFileCommand<IPublicFileDeleteClient>(
                _blobPictureContainer,
                FileCacheTableStorage.Candidate.FilePartitionKey,
                cacheId);

            await _mediator.Publish(command);

            return NoContent();
        }
    }
}
