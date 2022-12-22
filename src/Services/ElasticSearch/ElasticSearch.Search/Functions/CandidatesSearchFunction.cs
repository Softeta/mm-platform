using System;
using System.Net;
using System.Threading.Tasks;
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

namespace ElasticSearch.Search.Functions
{
    public class CandidatesSearchFunction : BaseFunction
    {
        private const string FunctionName = "suggested-candidates";

        private readonly ICandidatesSearchClient _documentsServiceClient;
        private readonly ILogger<CandidatesSearchFunction> _logger;

        public CandidatesSearchFunction(
            ICandidatesSearchClient documentsServiceClient,
            ILogger<CandidatesSearchFunction> logger) : base(FunctionName)
        {
            _documentsServiceClient = documentsServiceClient;
            _logger = logger;
        }

        [FunctionName(FunctionName)]
        [OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
        [OpenApiRequestBody("application/json", typeof(CandidatesSearchRequest), Required = true)]
        [OpenApiParameter(name: CandidatesSearchQueryParams.PageNumber, In = ParameterLocation.Query, Required = false, Type = typeof(int))]
        [OpenApiParameter(name: CandidatesSearchQueryParams.PageSize, In = ParameterLocation.Query, Required = false, Type = typeof(int))]
        [OpenApiParameter(name: CandidatesSearchQueryParams.JobId, In = ParameterLocation.Query, Required = false, Type = typeof(string))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(PagedResponse<SearchResponse>))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req)
        {
            try
            {
                var filterParams = GetQueryStringFilter<CandidatesSearchFilter>(req) ?? new CandidatesSearchFilter();
                var searchText = await BuildSearchTextFromRequestAsync<CandidatesSearchRequest>(req);

                var searchOptions = new CandidatesSearchOptionsBuilder()
                    .WithPageSize(filterParams.PageSize)
                    .WithPageNumber(filterParams.PageNumber)
                    .AddJobId(filterParams.JobId)
                    .Build();

                var (documents, count) = await _documentsServiceClient.SearchDocumentsAsync<CandidateIndex>(searchText, searchOptions);
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
