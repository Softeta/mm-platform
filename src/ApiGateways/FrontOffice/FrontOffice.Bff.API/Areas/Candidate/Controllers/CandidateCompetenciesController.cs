using API.Customization.Authorization.Constants;
using API.Customization.Controllers;
using API.Customization.Controllers.Attributes;
using API.WebClients.Clients;
using Contracts.Candidate.Candidates.Requests;
using Contracts.Candidate.Candidates.Responses;
using Domain.Seedwork.Exceptions;
using FrontOffice.Bff.API.Areas.Candidate.Models.Candidate.ServiceRequests;
using FrontOffice.Bff.API.Candidate.Models.Candidate.Requests;
using FrontOffice.Bff.API.Infrastructure.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FrontOffice.Bff.API.Candidate.Controllers
{
    public class CandidateCompetenciesController : AuthorizedApiController
    {
        private readonly ICandidateWebApiClient _candidateServiceProvider;

        public CandidateCompetenciesController(ICandidateWebApiClient candidateServiceProvider)
        {
            _candidateServiceProvider = candidateServiceProvider;
        }

        [HttpPut("api/v1/candidates/{candidateId}/competencies")]
        [Authorize(CustomPolicies.IsAllowedModifyCandidate)]
        [ProducesResponseOk]
        public async Task<ActionResult<GetCandidateResponse>> UpdateCompetencies(
            [FromRoute] Guid candidateId,
            [FromBody] UpdateCandidateCompetenciesRequest request)
        {
            var candidateEndpoint = string.Format(Endpoints.CandidateService.Candidate, candidateId);
            var candidate = await _candidateServiceProvider.GetAsync<GetCandidateResponse>(candidateEndpoint);
            if (candidate is null)
            {
                throw new NotFoundException($"Candidate does not exist. Id: {candidateId}",
                    ErrorCodes.NotFound.CandidateNotFound);
            }

            if (request.Skills != null)
            {
                candidate.Skills = request.Skills;
            }

            if (request.Languages != null)
            {
                candidate.Languages = request.Languages;
            }

            if (request.Industries != null)
            {
                candidate.Industries = request.Industries;
            }

            var payload = UpdateCandidateServiceRequest.ToServiceRequest(candidate);

            var endpoint = string.Format(Endpoints.CandidateService.Candidate, candidateId);
            var result = await _candidateServiceProvider.PutAsync<UpdateCandidateRequest, GetCandidateResponse>(payload, endpoint);

            return Ok(result);
        }
    }
}
