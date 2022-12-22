using API.Customization.Authorization.Constants;
using API.Customization.Controllers;
using API.Customization.Controllers.Attributes;
using API.Customization.Exceptions;
using API.WebClients.Clients;
using Contracts.Company.Responses;
using Contracts.Job.Jobs.Requests;
using Contracts.Job.Jobs.Responses;
using Contracts.Shared.Services.Company.Requests;
using Domain.Seedwork.Exceptions;
using FrontOffice.Bff.API.Areas.Company.Models.Registration;
using FrontOffice.Bff.API.Infrastructure.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FrontOffice.Bff.API.Areas.Company.Controllers
{
    public class RegistrationController : AuthorizedApiController
    {
        private readonly ICompanyServiceWebApiClient _companyServiceClient;
        private readonly IJobServiceWebApiClient _jobServiceClient;

        public RegistrationController(
            ICompanyServiceWebApiClient companyServiceClient, 
            IJobServiceWebApiClient jobServiceClient)
        {
            _companyServiceClient = companyServiceClient;
            _jobServiceClient = jobServiceClient;
        }

        [HttpPost("api/v1/companies/registration")]
        [Authorize(CustomPolicies.IsAllowedContactPersonRegisterHimself)]
        [ProducesResponseOk]
        public async Task<ActionResult<CompanyRegistrationResponse>> RegisterCompany([FromBody] CompanyRegistrationRequest request)
        {
            ValidateEmail();

            var companyRequest = request.ToCompanyCreateRequest(Email!);

            var company = await _companyServiceClient.PostAsync<RegisterCompanyRequest, GetCompanyResponse>(
                companyRequest, 
                Endpoints.CompanyService.Registered);

            if (company is null)
            {
                throw new NotFoundException("No company returned", ErrorCodes.NotFound.CompanyNotFound);
            }

            try
            {
                var pendingJobRequest = request.Job.ToPendingJobServiceRequest(company!, Email!);

                var job = await _jobServiceClient.PostAsync<InitializePendingJobRequest, GetJobResponse>(
                     pendingJobRequest,
                     Endpoints.JobService.Initialization);

                return Ok(CompanyRegistrationResponse.FromResponses(company, job, null));
            }
            catch (Exception ex)
            {
                var message = JsonConvert.DeserializeObject<ExceptionResponse>(ex.Message);
                return Ok(CompanyRegistrationResponse.FromResponses(company, null, message?.Code));
            } 
        }
    }
}
