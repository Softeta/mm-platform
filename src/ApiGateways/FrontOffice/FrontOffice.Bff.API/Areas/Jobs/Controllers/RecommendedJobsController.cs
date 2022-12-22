using API.Customization.Authorization.Constants;
using API.Customization.Controllers;
using API.Customization.Controllers.Attributes;
using API.Customization.Pagination;
using API.WebClients.Clients;
using Domain.Seedwork.Enums;
using FrontOffice.Bff.API.Areas.Jobs.Models;
using FrontOffice.Bff.API.Areas.Jobs.Models.ElasticSearch.Requests;
using FrontOffice.Bff.API.Areas.Jobs.Models.ElasticSearch.Responses;
using FrontOffice.Bff.API.Areas.Jobs.Models.Filters;
using FrontOffice.Bff.API.Builders;
using FrontOffice.Bff.API.Infrastructure;
using FrontOffice.Bff.API.Infrastructure.Constants;
using FrontOffice.Bff.API.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

namespace FrontOffice.Bff.API.Areas.Jobs.Controllers
{
    public class RecommendedJobsController : AuthorizedApiController
    {
        private readonly IElasticSearchWebApiClient _elasticSearchProvider;
        private readonly IJobServiceWebApiClient _jobServiceProvider;
        private readonly string _recommendedJobsSearchKey;

        public RecommendedJobsController(
            IOptions<ElasticSearchConfigurations> elasticSearchOptions,
            IElasticSearchWebApiClient elasticSearchProvider,
            IJobServiceWebApiClient jobServiceProvider)
        {
            _recommendedJobsSearchKey = elasticSearchOptions.Value.RecommendedJobsKey;
            _elasticSearchProvider = elasticSearchProvider;
            _jobServiceProvider = jobServiceProvider;
        }

        [HttpPost("api/v1/jobs/{candidateId}/recommended-jobs", Name = nameof(GetRecommendedJobs))]
        [Authorize(CustomPolicies.IsAllowedReadCandidate)]
        [ProducesResponseOk]
        public async Task<ActionResult<PagedResponse<GetRecommendedJobResponse>>> GetRecommendedJobs(
            [FromRoute, Required] Guid candidateId,
            [FromQuery] RecommendedJobsFilter filterParams,
            [FromBody] GetRecommendedJobsRequest request
            )
        {
            var elasticSearchEndpoint = new JobsSearchEndpointBuilder()
                .WithFunctionName(Endpoints.ElasticSearchService.JobsSearch)
                .WithFunctionKey(_recommendedJobsSearchKey)
                .WithPageSize(filterParams.PageSize)
                .WithPageNumber(filterParams.PageNumber)
                .WithCandidateId(candidateId)
                .AddStage(JobStage.Calibration)
                .AddStage(JobStage.CandidateSelection)
                .AddStage(JobStage.ShortListed)
                .WithIsPublished(true)
                .Build();

            var jobsSearchRequest = JobsSearchRequest.FromFilter(request);

            var recommendedJobs = await _elasticSearchProvider
                .PostAsync<JobsSearchRequest, PagedResponse<JobElasticSearchResponse>>(jobsSearchRequest, elasticSearchEndpoint);

            if (recommendedJobs?.Data is null || recommendedJobs.Count == 0)
            {
                return PrepareEmptyResponse(filterParams, nameof(GetRecommendedJobs));
            }

            var jobs = await GetJobsAsync(recommendedJobs, filterParams);

            if (jobs.Count == 0)
            {
                return PrepareEmptyResponse(filterParams, nameof(GetRecommendedJobs));
            }

            var response = new PagedResponse<GetRecommendedJobResponse>(
                jobs.Count,
                jobs.Data!,
                filterParams.PageNumber,
                filterParams.PageSize,
                Url.RouteUrl(nameof(GetRecommendedJobs))!,
                Request.QueryString.ToString());

            return Ok(response);
        }

        private async Task<PagedResponse<GetRecommendedJobResponse>> GetJobsAsync(
            PagedResponse<JobElasticSearchResponse> recommendedJobs,
            PagedFilter filterParams)
        {
            var jobsEndpoint = new JobsEndpointBuilder()
                .ForEndpoint(Endpoints.JobService.Base)
                .WithAsFirstPage()
                .WithPageSize(filterParams.PageSize)
                .WithJobs(recommendedJobs.Data!.Select(x => x.Id).ToList())
                .Build();

            var jobs = await _jobServiceProvider.GetAsync<PagedResponse<GetRecommendedJobResponse>>(jobsEndpoint);

            if (jobs?.Data is null || jobs.Count == 0)
            {
                return new PagedResponse<GetRecommendedJobResponse>();
            }

            jobs.CopyResponseDataFrom(recommendedJobs);
            jobs.OrderByScore(recommendedJobs);

            return jobs;
        }

        private PagedResponse<GetRecommendedJobResponse> PrepareEmptyResponse(PagedFilter filterParams, string methodName)
        {
            return new PagedResponse<GetRecommendedJobResponse>(
                0,
                new List<GetRecommendedJobResponse>(),
                filterParams.PageNumber,
                filterParams.PageSize,
                Url.RouteUrl(methodName)!,
                Request.QueryString.ToString());
        }
    }
}
