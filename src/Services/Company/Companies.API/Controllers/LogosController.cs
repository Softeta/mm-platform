using API.Customization.Authorization.Constants;
using API.Customization.Controllers;
using API.Customization.Controllers.Attributes;
using Companies.Infrastructure.Settings;
using Contracts.Shared.Requests;
using Contracts.Shared.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Persistence.Customization.Commands.Files;
using Persistence.Customization.FileStorage.Clients.Public;
using Persistence.Customization.TableStorage;

namespace Companies.API.Controllers
{
    public class LogosController : AuthorizedApiController
    {
        private readonly string _blobCompanyLogoContainer;
        private readonly IMediator _mediator;
        private readonly CacheSettings _cacheSettings;

        public LogosController(
            IMediator mediator,
            IOptions<BlobContainerSettings> companyContainers,
            IOptions<CacheSettings> cacheSettings)
        {
            _blobCompanyLogoContainer = companyContainers.Value.LogosContainer;
            _mediator = mediator;
            _cacheSettings = cacheSettings.Value;
        }

        [HttpPost("api/v1/companies/logos")]
        [Authorize(CustomPolicies.IsAllowedModifyCompany)]
        [ProducesResponseOk]
        public async Task<ActionResult<FileCacheResponse>> AddCompanyLogo([FromForm] ImageRequest request)
        {
            var command = new UploadImageCommand<IPublicFileUploadClient>(
                request.File,
                _blobCompanyLogoContainer,
                FileCacheTableStorage.Company.FilePartitionKey,
                FileCacheTableStorage.Company.Category.Logo,
                _cacheSettings.FileExpiresInMinutes);

            var result = await _mediator.Send(command);

            var response = new FileCacheResponse
            {
                CacheId = result
            };

            return Ok(response);
        }

        [HttpPut("api/v1/companies/logos/{cacheId}")]
        [Authorize(CustomPolicies.IsAllowedModifyCompany)]
        [ProducesResponseOk]
        public async Task<ActionResult<FileCacheResponse>> UpdateCompanyLogo(
            [FromRoute] Guid cacheId,
            [FromForm] ImageRequest request)
        {
            var command = new UpdateImageCommand<IPublicFileDeleteClient, IPublicFileUploadClient>(
                _blobCompanyLogoContainer,
                FileCacheTableStorage.Company.FilePartitionKey,
                FileCacheTableStorage.Company.Category.Logo,
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

        [HttpDelete("api/v1/companies/logos/{cacheId}")]
        [Authorize(CustomPolicies.IsAllowedModifyCompany)]
        [ProducesResponseNoContent]
        public async Task<ActionResult> DeleteCompanyLogo([FromRoute] Guid cacheId)
        {
            var command = new DeleteFileCommand<IPublicFileDeleteClient>(
                _blobCompanyLogoContainer,
                FileCacheTableStorage.Company.FilePartitionKey,
                cacheId);

            await _mediator.Publish(command);

            return NoContent();
        }
    }
}
