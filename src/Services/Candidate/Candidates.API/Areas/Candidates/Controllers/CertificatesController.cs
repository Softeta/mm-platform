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
    public class CertificatesController : AuthorizedApiController
    {
        private readonly string _blobCertificateContainer;
        private readonly IMediator _mediator;
        private readonly CacheSettings _cacheSettings;

        public CertificatesController(
            IMediator mediator, 
            IOptions<BlobContainerSettings> candidateContainers,
            IOptions<CacheSettings> cacheSettings)
        {
            _blobCertificateContainer = candidateContainers.Value.CandidateCertificatesContainer;
            _mediator = mediator;
            _cacheSettings = cacheSettings.Value;
        }

        [HttpPost("api/v1/candidates/certificates")]
        [Authorize(CustomPolicies.IsAllowedModifyCandidate)]
        [ProducesResponseOk]
        public async Task<ActionResult<FileCacheResponse>> AddCandidateCertificate([FromForm] DocumentRequest request)
        {
            var command = new UploadFileCommand<IPrivateFileUploadClient>(
                _blobCertificateContainer,
                FileCacheTableStorage.Candidate.FilePartitionKey,
                FileCacheTableStorage.Candidate.Category.Certificate,
                _cacheSettings.FileExpiresInMinutes,
                request.File);

            var result = await _mediator.Send(command);

            var response = new FileCacheResponse
            {
                CacheId = result
            };

            return Ok(response);
        }

        [HttpPut("api/v1/candidates/certificates/{cacheId}")]
        [Authorize(CustomPolicies.IsAllowedModifyCandidate)]
        [ProducesResponseOk]
        public async Task<ActionResult<FileCacheResponse>> UpdateCandidateCertificate(
            [FromRoute] Guid cacheId,
            [FromForm] DocumentRequest request)
        {
            var command = new UpdateFileCommand<IPrivateFileDeleteClient, IPrivateFileUploadClient>(
                _blobCertificateContainer,
                FileCacheTableStorage.Candidate.FilePartitionKey,
                FileCacheTableStorage.Candidate.Category.Certificate,
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

        [HttpDelete("api/v1/candidates/certificates/{cacheId}")]
        [Authorize(CustomPolicies.IsAllowedModifyCandidate)]
        [ProducesResponseNoContent]
        public async Task<ActionResult> DeleteCandidateCertificate([FromRoute] Guid cacheId)
        {
            var command = new DeleteFileCommand<IPrivateFileDeleteClient>(
                _blobCertificateContainer,
                FileCacheTableStorage.Candidate.FilePartitionKey,
                cacheId);

            await _mediator.Publish(command);

            return NoContent();
        }
    }
}
