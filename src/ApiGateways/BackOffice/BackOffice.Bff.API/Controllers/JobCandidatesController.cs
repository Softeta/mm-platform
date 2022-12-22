using API.Customization.Authorization.Constants;
using API.Customization.Controllers;
using API.Customization.Controllers.Attributes;
using API.Customization.Pagination;
using API.WebClients.Clients;
using BackOffice.Bff.API.Builders;
using BackOffice.Bff.API.Infrastructure.Constants;
using BackOffice.Bff.API.Models.Job.Requests;
using BackOffice.Bff.API.Models.Job.Responses;
using BackOffice.Bff.API.Models.Job.ServiceRequests;
using Domain.Seedwork.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Common = Contracts.Job.JobCandidates.Responses;

namespace BackOffice.Bff.API.Controllers
{
    public class JobCandidatesController : AuthorizedApiController
    {
        private readonly ICandidateWebApiClient _candidateServiceProvider;
        private readonly IJobServiceWebApiClient _jobServiceProvider;

        public JobCandidatesController(
            ICandidateWebApiClient candidateServiceProvider,
            IJobServiceWebApiClient jobServiceProvider)
        {
            _candidateServiceProvider = candidateServiceProvider;
            _jobServiceProvider = jobServiceProvider;
        }


        [HttpPost("api/v1/job-candidates/{jobId}/selected-candidates")]
        [Authorize(CustomPolicies.IsAllowedModifyJob)]
        [ProducesResponseOk]
        public async Task<ActionResult<Common.GetJobCandidatesResponse>> AddSelectedCandidatesAsync([FromRoute, Required] Guid jobId, [FromBody] AddSelectedCandidatesRequest request)
        {
            var candidates = await GetCandidatesAsync(request.Candidates);
            var payload = AddSelectedCandidatesServiceRequest.ToServiceRequest(candidates!);

            var addSelectedCandidatesEndpoint = string.Format(Endpoints.JobService.AddSelectedCandidates, jobId);
            var result = await _jobServiceProvider.PostAsync<AddSelectedCandidatesServiceRequest, Common.GetJobCandidatesResponse>(payload, addSelectedCandidatesEndpoint);

            return Ok(result);
        }

        private async Task<List<GetSelectedCandidateResponse>?> GetCandidatesAsync(Collection<Guid> candidatesIds)
        {
            if (candidatesIds.Count == 0)
            {
                throw new BadRequestException($"Candidates collection should not be empty.",
                    ErrorCodes.BadRequest.CandidateCollectionEmpty);
            }

            var candidatesEndpoint = new CandidatesEndpointBuilder()
                .ForEndpoint(Endpoints.CandidateService.Candidates)
                .WithCandidates(candidatesIds)
                .Build();

            var candidates = await _candidateServiceProvider.GetAsync<PagedResponse<GetSelectedCandidateResponse>>(candidatesEndpoint);

            if (candidates?.Data is null || candidates.Data.Count() < candidatesIds.Count)
            {
                throw new BadRequestException($"One or more candidate does not exist.",
                    ErrorCodes.BadRequest.OneOrMoreCandidateNotExists);
            }

            return candidates.Data?.ToList();
        }
    }
}
