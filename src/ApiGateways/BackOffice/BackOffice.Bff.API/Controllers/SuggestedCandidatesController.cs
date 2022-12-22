using API.Customization.Authorization.Constants;
using API.Customization.Controllers.Attributes;
using API.Customization.Pagination;
using API.WebClients.Clients;
using BackOffice.Bff.API.Builders;
using BackOffice.Bff.API.Controllers.BaseControllers;
using BackOffice.Bff.API.Infrastructure;
using BackOffice.Bff.API.Infrastructure.Constants;
using BackOffice.Bff.API.Infrastructure.Extensions;
using BackOffice.Bff.API.Models.Candidate.Response;
using BackOffice.Bff.API.Models.Job.Filters;
using Contracts.Job.Jobs.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using BackOffice.Bff.API.Models.ElasticSearch.Requests;
using BackOffice.Bff.API.Models.ElasticSearch.Responses;

namespace BackOffice.Bff.API.Controllers
{
    public class SuggestedCandidatesController : CandidatesBaseController
    {
        private readonly ICandidateWebApiClient _candidateServiceProvider;
        private readonly IJobServiceWebApiClient _jobServiceProvider;
        private readonly IElasticSearchWebApiClient _elasticSearchProvider;
        private readonly string _suggestedCandidatesSearchKey;

        public SuggestedCandidatesController(
            ICandidateWebApiClient candidateServiceProvider,
            IJobServiceWebApiClient jobServiceProvider,
            IElasticSearchWebApiClient elasticSearchProvider,
            IOptions<ElasticSearchConfigurations> elasticSearchConfigurations)
        {
            _candidateServiceProvider = candidateServiceProvider;
            _jobServiceProvider = jobServiceProvider;
            _elasticSearchProvider = elasticSearchProvider;
            _suggestedCandidatesSearchKey = elasticSearchConfigurations.Value.SuggestedCandidatesKey;
        }

        [HttpPost("api/v1/jobs/suggested-candidates", Name = nameof(GetSuggestedCandidates))]
        [Authorize(CustomPolicies.IsAllowedModifyJob)]
        [ProducesResponseOk]
        public async Task<ActionResult<PagedResponse<GetCandidateResponse>>> GetSuggestedCandidates(
            [FromQuery] SuggestedCandidatesFilter filterParams,
            [FromBody] SuggestedCandidatesRequest request
        )
        {
            var jobRequest = JobRequest.FromSuggestedCandidatesRequest(request);
            
            var queryStringForElasticSearch = $"?{nameof(filterParams.PageNumber)}={filterParams.PageNumber}&{nameof(filterParams.PageSize)}={filterParams.PageSize}";

            var elasticSearchEndpoint = new CandidateSearchEndpointBuilder()
                .WithFunctionName(Endpoints.ElasticSearchService.CandidatesSearch)
                .WithFunctionKey(_suggestedCandidatesSearchKey)
                .WithQueryStrings(queryStringForElasticSearch)
                .WithJobId(request.JobId)
                .Build();

            var response = await GetSuggestedCandidatesResponseAsync(jobRequest, elasticSearchEndpoint, filterParams, nameof(GetSuggestedCandidates));

            return Ok(response);
        }

        private async Task<PagedResponse<GetCandidateResponse>> GetCandidatesAsync(
            PagedResponse<CandidateElasticSearchResponse> suggestedCandidates,
            PagedFilter  filterParams)
        {
            var candidatesEndpoint = new CandidatesEndpointBuilder()
                .ForEndpoint(Endpoints.CandidateService.Candidates)
                .WithAsFirstPage()
                .WithPageSize(filterParams.PageSize)
                .WithCandidates(suggestedCandidates.Data!.Select(x => x.Id).ToList())
                .Build();

            var candidates = await _candidateServiceProvider.GetAsync<PagedResponse<GetCandidateResponse>>(candidatesEndpoint);

            if (candidates?.Data is null || candidates.Count == 0)
            {
                return new PagedResponse<GetCandidateResponse>();
            }

            candidates.CopyResponseDataFrom(suggestedCandidates);
            candidates.OrderByScore(suggestedCandidates);

            return candidates;
        }

        private async Task<PagedResponse<GetCandidateResponse>> GetSuggestedCandidatesResponseAsync(
            JobRequest jobRequest,
            string elasticSearchEndpoint,
            PagedFilter filterParams,
            string routeUrl)
        {
            var suggestedCandidates = await _elasticSearchProvider.PostAsync<JobRequest, PagedResponse<CandidateElasticSearchResponse>>(jobRequest, elasticSearchEndpoint);

            if (suggestedCandidates?.Data is null || suggestedCandidates.Count == 0)
            {
                return PrepareEmptyResponse(filterParams, routeUrl);
            }

            var candidates = await GetCandidatesAsync(suggestedCandidates, filterParams);

            if (candidates.Count == 0)
            {
                return PrepareEmptyResponse(filterParams, routeUrl);
            }

            candidates.CopyResponseDataFrom(suggestedCandidates);
            candidates.OrderByScore(suggestedCandidates);

            var response = new PagedResponse<GetCandidateResponse>(
                candidates.Count,
                candidates.Data!,
                filterParams.PageNumber,
                filterParams.PageSize,
                Url.RouteUrl(routeUrl)!,
                Request.QueryString.ToString());

            return response;
        }
    }
}
