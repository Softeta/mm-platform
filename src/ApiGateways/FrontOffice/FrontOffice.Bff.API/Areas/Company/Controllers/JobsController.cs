using API.Customization.Authorization.Constants;
using API.Customization.Controllers;
using API.Customization.Controllers.Attributes;
using API.WebClients.Clients;
using Contracts.Company.Responses;
using Contracts.Job.Companies.Requests;
using Contracts.Job.Companies.Responses;
using Contracts.Job.Jobs.Requests;
using Contracts.Job.Jobs.Responses;
using Domain.Seedwork.Exceptions;
using FrontOffice.Bff.API.Areas.Company.Models.Jobs;
using FrontOffice.Bff.API.Infrastructure;
using FrontOffice.Bff.API.Infrastructure.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace FrontOffice.Bff.API.Areas.Company.Controllers
{
    public class JobsController : AuthorizedApiController
    {
        private readonly IJobServiceWebApiClient _jobServiceClient;
        private readonly ICompanyServiceWebApiClient _companyServiceClient;
        private readonly CountrySettings _countrySettings;

        public JobsController(
            IJobServiceWebApiClient jobServiceClient,
            ICompanyServiceWebApiClient companyServiceClient,
            IOptions<CountrySettings> countrySettings)
        {
            _jobServiceClient = jobServiceClient;
            _companyServiceClient = companyServiceClient;
            _countrySettings = countrySettings.Value;
        }

        [HttpPost("api/v1/jobs/step1")]
        [Authorize(CustomPolicies.IsAllowedModifyJob)]
        [ProducesResponseOk]
        public async Task<ActionResult<GetJobResponse>> Step1([FromBody] Step1Request request)
        {
            ValidateEmail();

            var companyEndpiont = string.Format(Endpoints.CompanyService.Company, request.CompanyId);
            var company = await _companyServiceClient.GetAsync<GetCompanyResponse>(companyEndpiont);

            var pendingJobRequest = request.ToPendingJobServiceRequest(company!, Email!);

            var job = await _jobServiceClient.PostAsync<InitializePendingJobRequest, GetJobResponse>(
                pendingJobRequest,
                Endpoints.JobService.Initialization);

            return Ok(job);
        }

        [HttpPut("api/v1/jobs/{jobId}/step4")]
        [Authorize(CustomPolicies.IsAllowedModifyJob)]
        [ProducesResponseOk]
        public async Task<ActionResult<GetJobResponse>> Step4([FromRoute] Guid jobId, [FromBody] Step4Request request)
        {
            var (job, endpoint) = await GetJobAsync(jobId);

            var update = UpdateJobServiceRequest.ToServiceRequestStep4(job, request);

           var updatedJob = await _jobServiceClient.PutAsync<UpdateJobRequest, GetJobResponse>(update, endpoint);

            return Ok(updatedJob);
        }

        [HttpPut("api/v1/jobs/{jobId}/step5")]
        [Authorize(CustomPolicies.IsAllowedModifyJob)]
        [ProducesResponseOk]
        public async Task<ActionResult<GetJobResponse>> Step5([FromRoute] Guid jobId, [FromBody] Step5Request request)
        {
            var (job, endpoint) = await GetJobAsync(jobId);

            var update = UpdateJobServiceRequest.ToServiceRequestStep5(job, request);

            var updatedJob = await _jobServiceClient.PutAsync<UpdateJobRequest, GetJobResponse>(update, endpoint);

            return Ok(updatedJob);
        }

        [HttpPut("api/v1/jobs/{jobId}/step6/freelance")]
        [Authorize(CustomPolicies.IsAllowedModifyJob)]
        [ProducesResponseOk]
        public async Task<ActionResult<GetJobResponse>> Step6Freelance([FromRoute] Guid jobId, [FromBody] Step6FreelanceRequest request)
        {
            var (job, endpoint) = await GetJobAsync(jobId);

            var update = UpdateJobServiceRequest.ToServiceRequestStep6Freelance(job, request, _countrySettings);

            if (request.ShouldUpdateAddress())
            {
                await UpdateCompanyAddressAsync(job, request);
            }

            var updatedJob = await _jobServiceClient.PutAsync<UpdateJobRequest, GetJobResponse>(update, endpoint);

            return Ok(updatedJob);
        }

        [HttpPut("api/v1/jobs/{jobId}/step6/permanent")]
        [Authorize(CustomPolicies.IsAllowedModifyJob)]
        [ProducesResponseOk]
        public async Task<ActionResult<GetJobResponse>> Step6Permanent([FromRoute] Guid jobId, [FromBody] Step6PermanentRequest request)
        {
            var (job, endpoint) = await GetJobAsync(jobId);

            var update = UpdateJobServiceRequest.ToServiceRequestStep6Permanent(job, request, _countrySettings);

            if (request.ShouldUpdateAddress())
            {
                await UpdateCompanyAddressAsync(job, request);
            }

            var updatedJob = await _jobServiceClient.PutAsync<UpdateJobRequest, GetJobResponse>(update, endpoint);

            return Ok(updatedJob);
        }

        [HttpPut("api/v1/jobs/{jobId}/step6")]
        [Authorize(CustomPolicies.IsAllowedModifyJob)]
        [ProducesResponseOk]
        public async Task<ActionResult<GetJobResponse>> Step6([FromRoute] Guid jobId, [FromBody] Step6Request request)
        {
            var (job, endpoint) = await GetJobAsync(jobId);

            var update = UpdateJobServiceRequest.ToServiceRequestStep6(job, request, _countrySettings);

            if (request.ShouldUpdateAddress())
            {
                await UpdateCompanyAddressAsync(job, request);
            }

            var updatedJob = await _jobServiceClient.PutAsync<UpdateJobRequest, GetJobResponse>(update, endpoint);

            return Ok(updatedJob);
        }

        private async Task UpdateCompanyAddressAsync(GetJobResponse job, Step6RequestBase request)
        {
            var jobCompanyEnpoint = string.Format(Endpoints.JobService.Company, job.Id);
            var companyRequest = UpdateJobCompanyServiceRequest.ToServiceRequestStep6(job, request);

            await _jobServiceClient.PutAsync<UpdateJobCompanyRequest, GetUpdateCompanyResponse>(companyRequest, jobCompanyEnpoint);
        }

        private async Task<(GetJobResponse, string)> GetJobAsync(Guid jobId)
        {
            var endpoint = string.Format(Endpoints.JobService.Job, jobId);

            var job = await _jobServiceClient.GetAsync<GetJobResponse>(endpoint);

            if (job is null)
            {
                throw new NotFoundException("Job not found", ErrorCodes.NotFound.JobNotFound);
            }

            return (job, endpoint);
        }
    }
}
