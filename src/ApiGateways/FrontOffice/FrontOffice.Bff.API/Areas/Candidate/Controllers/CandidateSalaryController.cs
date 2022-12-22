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

namespace FrontOffice.Bff.API.Candidate.Controllers
{
    public class CandidateSalaryController : AuthorizedApiController
    {
        private readonly ICandidateWebApiClient _candidateServiceProvider;

        public CandidateSalaryController(ICandidateWebApiClient candidateServiceProvider)
        {
            _candidateServiceProvider = candidateServiceProvider;
        }

        [HttpPut("api/v1/candidates/{candidateId}/salary")]
        [Authorize(CustomPolicies.IsAllowedModifyCandidate)]
        [ProducesResponseOk]
        public async Task<ActionResult<GetCandidateResponse>> UpdateSalary(
            [FromRoute] Guid candidateId,
            [FromBody] UpdateCandidateSalaryRequest request)
        {
            var candidateEndpoint = string.Format(Endpoints.CandidateService.Candidate, candidateId);
            var candidate = await _candidateServiceProvider.GetAsync<GetCandidateResponse>(candidateEndpoint);
            if (candidate is null)
            {
                throw new NotFoundException($"Candidate does not exist. Id: {candidateId}",
                    ErrorCodes.NotFound.CandidateNotFound);
            }

            if (request.Currency is null)
            {
                return Ok(candidate);
            }

            var workTypes = new HashSet<WorkType>();
            candidate.Currency = request.Currency;
            if (request.FreelanceMonthlySalary != null || request.FreelanceHourlySalary != null)
            {
                workTypes.Add(WorkType.Freelance);
                candidate.Freelance = new Contracts.Candidate.CandidateFreelance
                {
                    MonthlySalary = request.FreelanceMonthlySalary,
                    HourlySalary = request.FreelanceHourlySalary
                };
            }

            if (request.FullTimeMonthlySalary != null)
            {
                workTypes.Add(WorkType.Permanent);
                candidate.Permanent = new Contracts.Candidate.CandidatePermanent
                {
                    MonthlySalary = request.FullTimeMonthlySalary
                };
            }

            if (workTypes.Count == 0)
            {
                return Ok(candidate);
            }
            
            workTypes.UnionWith(candidate.WorkTypes);

            candidate.WorkTypes = workTypes;

            var payload = UpdateCandidateServiceRequest.ToServiceRequest(candidate);

            var endpoint = string.Format(Endpoints.CandidateService.Candidate, candidateId);
            var result = await _candidateServiceProvider.PutAsync<UpdateCandidateRequest, GetCandidateResponse>(payload, endpoint);

            return Ok(result);
        }
    }
}
