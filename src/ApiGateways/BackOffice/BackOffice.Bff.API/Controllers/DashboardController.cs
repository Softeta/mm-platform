using API.Customization.Authorization.Constants;
using API.Customization.Controllers;
using API.Customization.Controllers.Attributes;
using API.Customization.Pagination;
using API.WebClients.Clients;
using BackOffice.Bff.API.Builders;
using BackOffice.Bff.API.Infrastructure.Constants;
using BackOffice.Bff.API.Models.Dashboard.Filters;
using BackOffice.Bff.API.Models.Dashboard.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackOffice.Bff.API.Controllers
{
    public class DashboardController : AuthorizedApiController
    {
        private readonly IJobServiceWebApiClient _jobServiceProvider;

        public DashboardController(IJobServiceWebApiClient jobServiceProvider)
        {
            _jobServiceProvider = jobServiceProvider;
        }

        [HttpGet("api/v1/dashboard", Name = nameof(GetDashboard))]
        [Authorize(CustomPolicies.CanSeeDashboard)]
        [ProducesResponseOk]
        public async Task<ActionResult<PagedResponse<DashboardJobResponse>>> GetDashboard([FromQuery] DashboardJobsFilter filterParams)
        {
            var endpoint = new JobsEndpointBuilder()
                .ForEndpoint(Endpoints.JobService.Jobs)
                .WithUser(UserId)
                .WithCompanies(filterParams.Companies)
                .WithPositions(filterParams.Positions)
                .WithDeadLineDate(filterParams.DeadLineDate)
                .WithWorkTypes(filterParams.WorkTypes)
                .WithJobStages(filterParams.JobStages)
                .WithLocations(filterParams.Locations)
                .WithOwner(filterParams.Owner)
                .WithPageNumber(filterParams.PageNumber)
                .WithPageSize(filterParams.PageSize)
                .WithSearch(filterParams.Search)
                .WithOrderBy(filterParams.OrderBy)
                .Build();

            var result = await _jobServiceProvider.GetAsync<PagedResponse<DashboardJobResponse>>(endpoint);

            if (result?.Data is null)
            {
                Ok(result);
            }

            var response = new PagedResponse<DashboardJobResponse>(
                result!.Count,
                result.Data!,
                filterParams.PageNumber,
                filterParams.PageSize,
                Url.RouteUrl(nameof(GetDashboard))!,
                Request.QueryString.ToString());

            return Ok(response);
        }
    }
}