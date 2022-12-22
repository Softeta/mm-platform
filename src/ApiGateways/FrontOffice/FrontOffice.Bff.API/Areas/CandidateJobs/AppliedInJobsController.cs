using API.Customization.Authorization.Constants;
using API.Customization.Constants;
using API.Customization.Controllers;
using API.Customization.Controllers.Attributes;
using API.Customization.Pagination;
using API.WebClients.Clients;
using Contracts.Candidate.CandidateJobs.Responses;
using Contracts.Job.Jobs.Responses;
using Domain.Seedwork.Exceptions;
using FrontOffice.Bff.API.Areas.AppliedInJobs.Models.SelectedInJobs.ServiceRequests;
using FrontOffice.Bff.API.Infrastructure.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FrontOffice.Bff.API.Areas.CandidateJobs
{
    public class AppliedInJobsController : AuthorizedApiController
    {
        private readonly ICandidateWebApiClient _candidateServiceProvider;
        private readonly IJobServiceWebApiClient _jobServiceProvider;

        public AppliedInJobsController(ICandidateWebApiClient candidateServiceProvider, IJobServiceWebApiClient jobServiceProvider)
        {
            _candidateServiceProvider = candidateServiceProvider;
            _jobServiceProvider = jobServiceProvider;
        }

        [HttpPost("api/v1/candidates/{candidateId}/applied-jobs/{jobId}")]
        [Authorize(CustomPolicies.IsAllowedModifyCandidate)]
        [ProducesResponseNoContent]
        public async Task<ActionResult> ApplyToJob(
            [FromRoute] Guid candidateId,
            [FromRoute] Guid jobId)
        {
            var jobEndpoint = string.Format(Endpoints.JobService.Job, jobId);
            var job = await _jobServiceProvider.GetAsync<GetJobResponse>(jobEndpoint);

            if (job is null)
            {
                throw new NotFoundException($"Job does not exist. Id: {jobId}",
                    ErrorCodes.NotFound.JobNotFound);
            }
            var requestPayload = CandidateApplyToJobServiceRequest.ToServiceRequest(job);

            var applyToJobEndpoint = string.Format(Endpoints.CandidateService.ApplyToJob, candidateId, jobId);
            await _candidateServiceProvider.PostAsync<CandidateApplyToJobServiceRequest, object>(requestPayload, applyToJobEndpoint);

            return NoContent();
        }

        [HttpGet("api/v1/candidates/{candidateId}/applied-jobs/job-ids")]
        [Authorize(CustomPolicies.IsAllowedReadCandidate)]
        [ProducesResponseOk]
        public async Task<ActionResult<List<Guid>>> GetAppliedJobIds([FromRoute] Guid candidateId)
        {
            var appliedToJobIds = new List<Guid>();
            var pageNumber = 1;

            while (true)
            {
                var getAppliedJobsEndpoint = string.Format(
                    Endpoints.CandidateService.GetPagedAppliedJobs,
                    candidateId, 
                    pageNumber,
                    PaginationConstants.MaxPageSize);

                var pagedCandidates = await _candidateServiceProvider.GetAsync<PagedResponse<GetCandidateAppliedToJobResponse>>(getAppliedJobsEndpoint);

                if (pagedCandidates != null && pagedCandidates.Data != null)
                {
                    appliedToJobIds.AddRange(pagedCandidates.Data.Select(x => x.JobId));
                }
                if (pagedCandidates?.NextPagePath is null)
                {
                    break;
                }
                pageNumber++;
            }
            
            return Ok(appliedToJobIds);
        }
    }
}
