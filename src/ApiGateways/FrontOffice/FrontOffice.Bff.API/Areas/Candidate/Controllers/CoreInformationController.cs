using API.Customization.Authorization.Constants;
using API.Customization.Controllers;
using API.Customization.Controllers.Attributes;
using API.WebClients.Clients;
using Contracts.Candidate.Candidates.Requests;
using Contracts.Candidate.Candidates.Responses;
using Domain.Seedwork.Enums;
using Domain.Seedwork.Exceptions;
using FrontOffice.Bff.API.Areas.Candidate.Models.Candidate.Requests;
using FrontOffice.Bff.API.Areas.Candidate.Models.Candidate.ServiceRequests;
using FrontOffice.Bff.API.Infrastructure.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FrontOffice.Bff.API.Areas.Candidate.Controllers
{
    public class CoreInformationController : AuthorizedApiController
    {
        private readonly ICandidateWebApiClient _candidateServiceProvider;

        public CoreInformationController(ICandidateWebApiClient candidateServiceProvider)
        {
            _candidateServiceProvider = candidateServiceProvider;
        }

        [HttpPut("api/v1/candidates/{candidateId}/core-information/step-1")]
        [Authorize(CustomPolicies.IsAllowedModifyCandidate)]
        [ProducesResponseOk]
        public async Task<ActionResult<GetCandidateResponse>> Step1(
            [FromRoute] Guid candidateId,
            [FromBody] UpdateCandidateActivityStatusesRequest request)
        {
            var candidateEndpoint = string.Format(Endpoints.CandidateService.Candidate, candidateId);
            var candidate = await _candidateServiceProvider.GetAsync<GetCandidateResponse>(candidateEndpoint);
            if (candidate is null)
            {
                throw new NotFoundException($"Candidate does not exist. Id: {candidateId}",
                    ErrorCodes.NotFound.CandidateNotFound);
            }

            candidate.ActivityStatuses = request.ActivityStatuses;

            var payload = UpdateCandidateServiceRequest.ToServiceRequest(candidate);

            var endpoint = string.Format(Endpoints.CandidateService.Candidate, candidateId);
            var result = await _candidateServiceProvider.PutAsync<UpdateCandidateRequest, GetCandidateResponse>(payload, endpoint);

            if (!request.ActivityStatuses.Any(x => x == ActivityStatus.Freelancer) &&
                !request.ActivityStatuses.Any(x => x == ActivityStatus.Permanent) &&
                candidate.CandidateWorkExperiences.Count() > 0)
            {
                var workExperienceEndpoint = string.Format(
                    Endpoints.CandidateService.WorkExperienceById,
                    candidateId, 
                    candidate.CandidateWorkExperiences.First().Id);
                result = await _candidateServiceProvider.DeleteAsync<GetCandidateResponse>(workExperienceEndpoint);
            }

            return Ok(result);
        }

        [HttpPut("api/v1/candidates/{candidateId}/core-information/step-3")]
        [Authorize(CustomPolicies.IsAllowedModifyCandidate)]
        [ProducesResponseOk]
        public async Task<ActionResult<GetCandidateResponse>> Step3(
            [FromRoute] Guid candidateId,
            [FromBody] UpdateCandidateWorkTypesRequest request)
        {
            var candidateEndpoint = string.Format(Endpoints.CandidateService.Candidate, candidateId);
            var candidate = await _candidateServiceProvider.GetAsync<GetCandidateResponse>(candidateEndpoint);
            if (candidate is null)
            {
                throw new NotFoundException($"Candidate does not exist. Id: {candidateId}",
                    ErrorCodes.NotFound.CandidateNotFound);
            }

            candidate.WorkTypes = request.WorkTypes;

            var payload = UpdateCandidateServiceRequest.ToServiceRequest(candidate);

            var endpoint = string.Format(Endpoints.CandidateService.Candidate, candidateId);
            var result = await _candidateServiceProvider.PutAsync<UpdateCandidateRequest, GetCandidateResponse>(payload, endpoint);

            return Ok(result);
        }

        [HttpPut("api/v1/candidates/{candidateId}/core-information/step-4")]
        [Authorize(CustomPolicies.IsAllowedModifyCandidate)]
        [ProducesResponseOk]
        public async Task<ActionResult<GetCandidateResponse>> Step4(
            [FromRoute] Guid candidateId,
            [FromBody] UpdateCandidateContactInformationRequest request)
        {
            var candidateEndpoint = string.Format(Endpoints.CandidateService.Candidate, candidateId);
            var candidate = await _candidateServiceProvider.GetAsync<GetCandidateResponse>(candidateEndpoint);
            if (candidate is null)
            {
                throw new NotFoundException($"Candidate does not exist. Id: {candidateId}",
                    ErrorCodes.NotFound.CandidateNotFound);
            }

            candidate.FirstName = request.FirstName;
            candidate.LastName = request.LastName;
            candidate.Address = request.Address;
            candidate.Phone = request.Phone;

            var payload = UpdateCandidateServiceRequest.ToServiceRequest(candidate);

            var endpoint = string.Format(Endpoints.CandidateService.Candidate, candidateId);
            var result = await _candidateServiceProvider.PutAsync<UpdateCandidateRequest, GetCandidateResponse>(payload, endpoint);

            return Ok(result);
        }

        [HttpPut("api/v1/candidates/{candidateId}/core-information/completed")]
        [Authorize(CustomPolicies.IsAllowedModifyCandidate)]
        [ProducesResponseOk]
        public async Task<ActionResult<GetCandidateResponse>> Complete(
            [FromRoute] Guid candidateId,
            [FromBody] UpdateCandidateLinkedInUrlRequest request)
        {
            var candidateEndpoint = string.Format(Endpoints.CandidateService.Candidate, candidateId);
            var candidate = await _candidateServiceProvider.GetAsync<GetCandidateResponse>(candidateEndpoint);
            if (candidate is null)
            {
                throw new NotFoundException($"Candidate does not exist. Id: {candidateId}",
                    ErrorCodes.NotFound.CandidateNotFound);
            }

            candidate.LinkedInUrl = request.LinkedInUrl;

            var payload = UpdateCandidateServiceRequest.ToServiceRequest(candidate);

            var updateEndpoint = string.Format(Endpoints.CandidateService.Candidate, candidateId);
            await _candidateServiceProvider.PutAsync<UpdateCandidateRequest, GetCandidateResponse>(payload, updateEndpoint);

            var completeEndpoint = string.Format(Endpoints.CandidateService.CoreInformationCompleted, candidateId);
            var result = await _candidateServiceProvider.PutAsync<GetCandidateResponse>(completeEndpoint);
            return Ok(result);
        }
    }
}
