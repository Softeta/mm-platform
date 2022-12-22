using API.Customization.Extensions;
using API.Customization.Pagination;
using ElasticSearch.Search.Builders;
using ElasticSearch.Search.Constants;
using ElasticSearch.Search.Models.Filters;
using ElasticSearch.Search.Models.Indexes;
using ElasticSearch.Search.Models.Requests;
using ElasticSearch.Search.Models.Responses;
using ElasticSearch.Shared.Clients;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ElasticSearch.Search.Functions
{
    public class JobsSearchFunction : BaseFunction
    {
        private const string FunctionName = "suggested-jobs";

        private readonly IJobsSearchClient _documentsServiceClient;
        private readonly ILogger<JobsSearchFunction> _logger;

        public JobsSearchFunction(
            IJobsSearchClient documentsServiceClient,
            ILogger<JobsSearchFunction> logger) : base(FunctionName)
        {
            _documentsServiceClient = documentsServiceClient;
            _logger = logger;
        }

        [FunctionName(FunctionName)]
        [OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
        [OpenApiRequestBody("application/json", typeof(JobsSearchRequest), Required = true)]
        [OpenApiParameter(name: JobsSearchQueryParams.PageNumber, In = ParameterLocation.Query, Required = false, Type = typeof(int))]
        [OpenApiParameter(name: JobsSearchQueryParams.PageSize, In = ParameterLocation.Query, Required = false, Type = typeof(int))]
        [OpenApiParameter(name: JobsSearchQueryParams.CandidateId, In = ParameterLocation.Query, Required = false, Type = typeof(string))]
        [OpenApiParameter(name: JobsSearchQueryParams.Stages, In = ParameterLocation.Query, Required = false, Type = typeof(ICollection<string>))]
        [OpenApiParameter(name: JobsSearchQueryParams.IsPublished, In = ParameterLocation.Query, Required = false, Type = typeof(bool))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(PagedResponse<SearchResponse>))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req)
        {
            try
            {
                var filterParams = GetQueryStringFilter<JobsSearchFilter>(req) ?? new JobsSearchFilter();
                var searchText = await BuildSearchTextFromRequestAsync<JobsSearchRequest>(req);

                var searchOptions = new JobsSearchOptionsBuilder()
                    .WithPageSize(filterParams.PageSize)
                    .WithPageNumber(filterParams.PageNumber)
                    .WithCandidateId(filterParams.CandidateId)
                    .WithStages(filterParams.Stages)
                    .WithIsPublished(filterParams.IsPublished)
                    .Build();

                var (documents, count) = await _documentsServiceClient.SearchDocumentsAsync<JobIndex>(searchText, searchOptions);
                var response = PrepareResponse(documents, count, filterParams, req.QueryString.ToString());

                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Error occurred in {FunctionName}");
                throw;
            }
        }
    }
}
