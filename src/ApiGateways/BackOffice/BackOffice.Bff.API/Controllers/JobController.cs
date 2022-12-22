using API.Customization.Authorization.Constants;
using API.Customization.Constants;
using API.Customization.Controllers;
using API.Customization.Controllers.Attributes;
using API.Customization.Pagination;
using API.WebClients.Clients;
using BackOffice.Bff.API.Builders;
using BackOffice.Bff.API.Infrastructure.Constants;
using BackOffice.Bff.API.Models.Job.Requests;
using BackOffice.Bff.API.Models.Job.Responses;
using BackOffice.Bff.API.Models.Job.ServiceRequests;
using BackOffice.Bff.API.Models.Users;
using BackOffice.Shared.Queries;
using Contracts.Company.Responses.ContactPersons;
using Contracts.Job.Companies.Responses;
using Contracts.Job.Jobs.Responses;
using Domain.Seedwork.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Company = Contracts.Company.Responses;
using Job = Contracts.Job.Jobs.Requests;
using JobCompany = Contracts.Job.Companies.Requests;

namespace BackOffice.Bff.API.Controllers
{
    public class JobController : AuthorizedApiController
    {
        private readonly IJobServiceWebApiClient _jobServiceProvider;
        private readonly ICompanyServiceWebApiClient _companyServiceProvider;
        private readonly ICandidateWebApiClient _candidateServiceProvider;
        private readonly IMediator _mediator;

        public JobController(
            IJobServiceWebApiClient jobServiceProvider,
            ICompanyServiceWebApiClient companyServiceProvider,
            ICandidateWebApiClient candidateServiceProvider,
            IMediator mediator)
        {
            _jobServiceProvider = jobServiceProvider;
            _companyServiceProvider = companyServiceProvider;
            _candidateServiceProvider = candidateServiceProvider;
            _mediator = mediator;
        }

        [HttpPost("api/v1/jobs")]
        [Authorize(CustomPolicies.IsAllowedModifyJob)]
        [ProducesResponseOk]
        public async Task<ActionResult<GetCreatedJobResponse>> CreateJob([FromBody] CreateJobRequest request)
        {
            var companyEndpoint = string.Format(Endpoints.CompanyService.Company, request.Company.Id);
            var companyTask = _companyServiceProvider.GetAsync<Company.GetCompanyResponse>(companyEndpoint);
            var backOfficeUsersTask = GetOwnerAndAssignedEmployeesAsync(request.OwnerId, request.AssignedEmployees);
            var interestedCandidatesTask = GetInterestedCandidatesAsync(request.InterestedCandidates);
            
            await Task.WhenAll(companyTask, backOfficeUsersTask, interestedCandidatesTask);

            var interestedCandidatesRequest = interestedCandidatesTask.Result.Select(InterestedCandidateServiceRequest.ToServiceRequest);

            var payload = CreateJobServiceRequest.ToServiceRequest(
                request,
                backOfficeUsersTask.Result.Owner,
                backOfficeUsersTask.Result.AssignedEmployees,
                companyTask.Result!,
                interestedCandidatesRequest);

            var result = await _jobServiceProvider.PostAsync<Job.CreateJobRequest, GetCreatedJobResponse>(payload, Endpoints.JobService.Jobs);
            return Ok(result);
        }

        [HttpPut("api/v1/jobs/{jobId}")]
        [Authorize(CustomPolicies.IsAllowedModifyJob)]
        [ProducesResponseOk]
        public async Task<ActionResult<GetJobResponse>> UpdateJob([FromRoute, Required] Guid jobId, [FromBody] UpdateJobRequest request)
        {
            var backOfficeUsersTask = GetOwnerAndAssignedEmployeesAsync(request.OwnerId, request.AssignedEmployees);
            var interestedCandidatesTask = GetInterestedCandidatesAsync(request.InterestedCandidates);

            await Task.WhenAll(backOfficeUsersTask, interestedCandidatesTask);

            var interestedCandidatesRequest = interestedCandidatesTask.Result.Select(InterestedCandidateServiceRequest.ToServiceRequest);

            var payload = UpdateJobServiceRequest.ToServiceRequest(
                request,
                backOfficeUsersTask.Result.Owner,
                backOfficeUsersTask.Result.AssignedEmployees,
                interestedCandidatesRequest);

            var updateEndpoint = string.Format(Endpoints.JobService.UpdateJob, jobId);
            var result = await _jobServiceProvider.PutAsync<Job.UpdateJobRequest, GetJobResponse>(payload, updateEndpoint);
            return Ok(result);
        }

        [HttpPut("api/v1/jobs/{jobId}/assigned-employees")]
        [Authorize(CustomPolicies.IsAllowedModifyJob)]
        [ProducesResponseOk]
        public async Task<ActionResult<GetJobAssignedEmployeesResponse>> UpdateJobAssignedEmployees(
            [FromRoute, Required] Guid jobId, 
            [FromBody] UpdateJobAssignedEmployeesRequest request)
        {
            var backOfficeUsersTask = GetAssignedEmployeesAsync(request.AssignedEmployees);

            var jobEndpoint = string.Format(Endpoints.JobService.Job, jobId);
            var job = await _jobServiceProvider.GetAsync<GetJobResponse>(jobEndpoint);

            if (job is null)
            {
                throw new NotFoundException($"Job does not exist. Id: {jobId}",
                    ErrorCodes.NotFound.JobNotFound);
            }

            var payload = UpdateJobServiceRequest.ToServiceRequest(job, await backOfficeUsersTask);
            var result = await _jobServiceProvider.PutAsync<Job.UpdateJobRequest, GetJobResponse>(payload, jobEndpoint);

            return Ok(new GetJobAssignedEmployeesResponse { AssignedEmployees = result!.AssignedEmployees });
        }

        [HttpPut("api/v1/jobs/{jobId}/company")]
        [Authorize(CustomPolicies.IsAllowedModifyJob)]
        [ProducesResponseOk]
        public async Task<ActionResult<GetUpdateCompanyResponse>> UpdateJobCompany([FromRoute, Required] Guid jobId, [FromBody] JobCompanyRequest request)
        {
            var jobEndpoint = string.Format(Endpoints.JobService.Job, jobId);
            var job = await _jobServiceProvider.GetAsync<GetJobResponse>(jobEndpoint);

            if (job is null)
            {
                throw new NotFoundException("Job not found", ErrorCodes.NotFound.JobNotFound);
            }

            var jobContactPersonIds = new HashSet<Guid>(job.Company.ContactPersons.Select(x => x.Id));
            var contactPersonsToAdd = request.ContactPersons.Where(x => !jobContactPersonIds.Contains(x.Id));

            var newContactPersonIds = new HashSet<Guid>(request.ContactPersons.Select(x => x.Id));
            var contactPersonsToRemove = jobContactPersonIds.Where(x => !newContactPersonIds.Contains(x));

            var companyContactPersons = await GetCompanyContactPersonsAsync(contactPersonsToAdd, job.Company.Id);

            var mainContactId = request.ContactPersons.Where(x => x.IsMainContact).Select(x => x.Id).FirstOrDefault();

            var payload = UpdateJobCompanyServiceRequest.ToServiceRequest(
                request, 
                contactPersonsToAdd,
                companyContactPersons,
                contactPersonsToRemove,
                mainContactId);

            var updateEndpoint = string.Format(Endpoints.JobService.UpdateJobCompany, jobId);
            var result = await _jobServiceProvider.PutAsync<JobCompany.UpdateJobCompanyRequest, GetUpdateCompanyResponse>(payload, updateEndpoint);
            return Ok(result);
        }

        private async Task<(BackOfficeUser Owner, List<BackOfficeUser> AssignedEmployees)> GetOwnerAndAssignedEmployeesAsync(Guid ownerId, ICollection<Guid> assignedEmployeesIds)
        {
            var backOfficeUserIds = new HashSet<Guid>(assignedEmployeesIds) { ownerId };

            var backOfficeUsers = await GetBackOfficeUsersAsync(backOfficeUserIds);

            var owner = backOfficeUsers
                .FirstOrDefault(x => x.Id == ownerId);

            var assignedEmployees = backOfficeUsers
                .Where(x => assignedEmployeesIds.Contains(x.Id))
                .ToList();

            if (owner is null)
            {
                throw new BadRequestException($"Owner does not exist. Id: {ownerId}",
                    ErrorCodes.BadRequest.OwnerNotExists);
            }

            if (assignedEmployees.Count < assignedEmployeesIds.Count)
            {
                throw new BadRequestException($"One or more assigned employees not exist",
                    ErrorCodes.BadRequest.OneOrMoreAssignedEmployeeNotExist);
            }

            return (owner, assignedEmployees);
        }

        private async Task<List<BackOfficeUser>> GetAssignedEmployeesAsync(ICollection<Guid> assignedEmployeesIds)
        {
            var backOfficeUsers = await GetBackOfficeUsersAsync(assignedEmployeesIds);

            if (backOfficeUsers.Count < assignedEmployeesIds.Count)
            {
                throw new BadRequestException($"One or more assigned employees not exist",
                    ErrorCodes.BadRequest.OneOrMoreAssignedEmployeeNotExist);
            }
            return backOfficeUsers;
        }

        private async Task<List<BackOfficeUser>> GetBackOfficeUsersAsync(ICollection<Guid> ids)
        {
            if (!ids.Any())
            {
                return new List<BackOfficeUser>();
            }
            var backOfficeUsers = await _mediator.Send(new GetCachedBackOfficeUsersQuery(ids.ToList()));
            return backOfficeUsers.Select(BackOfficeUser.FromTableEntity).ToList();
        }

        private async Task<List<GetInterestedCandidateResponse>> GetInterestedCandidatesAsync(ICollection<Guid> candidatesIds)
        {
            if (candidatesIds.Count == 0)
            {
                return new List<GetInterestedCandidateResponse>();
            }

            var candidatesEndpoint = new CandidatesEndpointBuilder()
                .ForEndpoint(Endpoints.CandidateService.Candidates)
                .WithCandidates(candidatesIds)
                .Build();

            var candidates = await _candidateServiceProvider.GetAsync<PagedResponse<GetInterestedCandidateResponse>>(candidatesEndpoint);

            if (candidates?.Data is null || candidates.Data.Count() < candidatesIds.Count)
            {
                throw new BadRequestException($"One or more candidate does not exist.",
                    ErrorCodes.BadRequest.OneOrMoreCandidateNotExists);
            }

            return candidates.Data.ToList();
        }

        private async Task<List<GetContactPersonBriefResponse>> GetCompanyContactPersonsAsync(
            IEnumerable<AddJobContactPersonRequest> contactPersonsToAdd,
            Guid companyId)
        {
            var result = new List<GetContactPersonBriefResponse>();

            if (!contactPersonsToAdd.Any()) return result;

            var pageNumber = 1;
            var contactPersonsQueryString = $"&contactPersons={string.Join("&contactPersons=", contactPersonsToAdd.Select(x => x.Id))}";

            while (true)
            {
                var getContactPersonsEndpoint = string.Format(
                    Endpoints.CompanyService.GetPagedContactPersons,
                    companyId,
                    pageNumber,
                    PaginationConstants.MaxPageSize,
                    contactPersonsQueryString);

                var companyContactPersons = await _companyServiceProvider.GetAsync<PagedResponse<GetContactPersonBriefResponse>>
                     (getContactPersonsEndpoint);

                if (companyContactPersons != null && companyContactPersons.Data != null)
                {
                    result.AddRange(companyContactPersons.Data);
                }
                if (companyContactPersons?.NextPagePath is null)
                {
                    break;
                }

                pageNumber++;
            }
            return result;
        }
    }
}
