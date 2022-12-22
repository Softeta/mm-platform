using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Customization.Extensions;
using API.Customization.Pagination;
using Azure.Search.Documents.Models;
using ElasticSearch.Search.Builders;
using ElasticSearch.Search.Models.Indexes;
using ElasticSearch.Search.Models.Requests;
using ElasticSearch.Search.Models.Responses;
using Microsoft.AspNetCore.Http;

namespace ElasticSearch.Search.Functions
{
    public abstract class BaseFunction
    {
        private readonly string _functionName;

        protected BaseFunction(string functionName)
        {
            _functionName = functionName;
        }

        protected static T? GetQueryStringFilter<T>(HttpRequest req)
        {
            return req.QueryString.HasValue 
                ? req.QueryString.Value!.QueryStringToObject<T>() 
                : default;
        }

        protected PagedResponse<SearchResponse> PrepareResponse<TIndex>(
            ICollection<SearchResult<TIndex>> documents, 
            long? count, 
            PagedFilter filter,
            string queryString) where TIndex : IndexBase
        {
            var response = documents
                .Select(x => SearchResponse.ToResponse(x.Document.id, x.Score))
                .ToList();

            return new PagedResponse<SearchResponse>(
                count ?? 0,
                response,
                filter.PageNumber,
                filter.PageSize,
                $"api/{_functionName}",
                queryString
            );
        }

        protected static async Task<string> BuildSearchTextFromRequestAsync<TRequest>(HttpRequest req) 
            where TRequest : SearchRequestBase
        {
            var searchRequest = await req.ParseRequestObjectAsync<TRequest>();

            return new SearchTextBuilder()
                .AddPosition(searchRequest.Position)
                .AddSkills(searchRequest.Skills)
                .AddSeniorities(searchRequest.Seniorities)
                .AddWorkTypes(searchRequest.WorkTypes)
                .AddWorkingFormats(searchRequest.WorkingFormats)
                .AddWorkingHourTypes(searchRequest.WorkingHourTypes)
                .AddIndustries(searchRequest.Industries)
                .AddLanguages(searchRequest.Languages)
                .AddLocation(searchRequest.Location)
                .Build();
        }
    }
}
