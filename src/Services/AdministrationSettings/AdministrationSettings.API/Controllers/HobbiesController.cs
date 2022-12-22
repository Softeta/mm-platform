using AdministrationSettings.API.Helpers;
using AdministrationSettings.API.Models.Hobbies;
using API.Customization.Constants;
using API.Customization.Controllers;
using API.Customization.Controllers.Attributes;
using API.Customization.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdministrationSettings.API.Controllers
{
    public class HobbiesController : AuthorizedApiController
    {
        private static List<Hobby>? hobbies = null;
        private const string hobbyDataPath = @"Data/Hobbies.json";

        [HttpGet("api/v1/hobbies", Name = nameof(GetHobbies))]
        [Authorize]
        [ProducesResponseOk]
        [ResponseCache(Duration = CacheDurations.Month, VaryByQueryKeys = new[] { "search", "pageNumber", "pageSize" })]
        public async Task<ActionResult<PagedResponse<Hobby>>> GetHobbies([FromQuery] HobbiesFilter filterParams)
        {
            if (hobbies is null)
            {
                hobbies = new List<Hobby>();
                hobbies = await Helper.GetDataFromFileAsync<Hobby>(hobbyDataPath);
            }

            var totalResults = hobbies
                .Where(x =>
                    string.IsNullOrWhiteSpace(filterParams.Search) ||
                    x.Code.ToLower().Contains(filterParams.Search.ToLower()));

            var results = totalResults
                .Skip(filterParams.PageNumber > 1 ? (filterParams.PageNumber - 1) * filterParams.PageSize : 0)
                .Take(filterParams.PageSize)
                .Select(x => new Hobby
                {
                    Id = x.Id,
                    Code = x.Code,
                })
                .ToList();

            var pagedResponse = new PagedResponse<Hobby>(
                totalResults.Count(),
                results,
                filterParams.PageNumber,
                filterParams.PageSize,
                Url.RouteUrl(nameof(GetHobbies))!,
                Request.QueryString.ToString());

            return Ok(pagedResponse);
        }
    }
}
