using API.Customization.Authorization.Constants;
using API.Customization.Controllers;
using API.Customization.Controllers.Attributes;
using API.WebClients.Clients;
using Contracts.Candidate.Candidates.Requests;
using Contracts.Candidate.Candidates.Responses;
using Domain.Seedwork.Exceptions;
using FrontOffice.Bff.API.Areas.Candidate.Models.Candidate.Requests;
using FrontOffice.Bff.API.Areas.Candidate.Models.Candidate.ServiceRequests;
using FrontOffice.Bff.API.Infrastructure.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FrontOffice.Bff.API.Areas.Candidate.Controllers
{
    public class CandidateVideosController : AuthorizedApiController
    {
        private readonly ICandidateWebApiClient _candidateServiceProvider;

        public CandidateVideosController(ICandidateWebApiClient candidateServiceProvider)
        {
            _candidateServiceProvider = candidateServiceProvider;
        }

        [HttpPut("api/v1/candidates/{candidateId}/videos")]
        [Authorize(CustomPolicies.IsAllowedModifyCandidate)]
        [ProducesResponseOk]
        public async Task<ActionResult<GetCandidateResponse>> UpdateVideo(
            [FromRoute] Guid candidateId,
            [FromBody] UpdateCandidateVideoRequest request)
        {
            var candidateEndpoint = string.Format(Endpoints.CandidateService.Candidate, candidateId);
            var candidate = await _candidateServiceProvider.GetAsync<GetCandidateResponse>(candidateEndpoint);
            if (candidate is null)
            {
                throw new NotFoundException($"Candidate does not exist. Id: {candidateId}",
                    ErrorCodes.NotFound.CandidateNotFound);
            }

            var payload = UpdateCandidateServiceRequest.ToServiceRequestWithNewVideo(candidate, request.Video);

            var endpoint = string.Format(Endpoints.CandidateService.Candidate, candidateId);
            var result = await _candidateServiceProvider.PutAsync<UpdateCandidateRequest, GetCandidateResponse>(payload, endpoint);

            return Ok(result);
        }
    }
}
