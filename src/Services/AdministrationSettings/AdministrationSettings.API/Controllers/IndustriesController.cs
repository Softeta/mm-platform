using AdministrationSettings.API.Helpers;
using AdministrationSettings.API.Models.Industries;
using API.Customization.Constants;
using API.Customization.Controllers;
using API.Customization.Controllers.Attributes;
using API.Customization.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdministrationSettings.API.Controllers
{
    public class IndustriesController : AuthorizedApiController
    {
        private static List<Industry>? industries = null;
        private static List<Industry>? recommendedIndustries = null;

        private const string industriesDataPath = @"Data/Industries.json";
        private const string recommendedIndustreisDataPath = @"Data/RecommendedIndustries.json";

        [HttpGet("api/v1/industries", Name = nameof(GetIndustries))]
        [Authorize]
        [ProducesResponseOk]
        [ResponseCache(Duration = CacheDurations.Month, VaryByQueryKeys = new[] { "search", "pageNumber", "pageSize" })]
        public async Task<ActionResult<PagedResponse<Industry>>> GetIndustries([FromQuery] IndustriesFilter filterParams)
        {
            if (industries is null)
            {
                industries = new List<Industry>();
                industries = await Helper.GetDataFromFileAsync<Industry>(industriesDataPath);
            }

            var totalResults = industries
                .Where(x =>
                    string.IsNullOrWhiteSpace(filterParams.Search) ||
                    x.Code.ToLower().Contains(filterParams.Search.ToLower()));

            var results = totalResults
                .Skip(filterParams.PageNumber > 1 ? (filterParams.PageNumber - 1) * filterParams.PageSize : 0)
                .Take(filterParams.PageSize)
                .Select(x => new Industry
                {
                    Id = x.Id,
                    Code = x.Code,
                })
                .ToList();

            var pagedResponse = new PagedResponse<Industry>(
                totalResults.Count(),
                results,
                filterParams.PageNumber,
                filterParams.PageSize,
                Url.RouteUrl(nameof(GetIndustries))!,
                Request.QueryString.ToString());

            return Ok(pagedResponse);
        }

        [HttpGet("api/v1/industries/recommended")]
        [ProducesResponseOk]
        [Authorize]
        public async Task<ActionResult<List<Industry>>> GetRecommendedIndustries()
        {
            if (recommendedIndustries is null)
            {
                recommendedIndustries = new List<Industry>();
                recommendedIndustries = await Helper.GetDataFromFileAsync<Industry>(recommendedIndustreisDataPath);
            }

            return Ok(recommendedIndustries);
        }
    }
}
