using AdministrationSettings.API.Helpers;
using AdministrationSettings.API.Models.Languages;
using API.Customization.Controllers;
using API.Customization.Controllers.Attributes;
using API.Customization.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace AdministrationSettings.API.Controllers
{
    public class LanguagesController : AuthorizedApiController
    {
        private const string languageDataPath = @"Data/Languages.json";
        private const string recommendedLanguagesDataPath = @"Data/RecommendedLanguages.json";

        private static List<Language>? languages = null;
        private static List<Language>? recommendedLanguages = null;

        public LanguagesController()
        {
        }

        [HttpGet("api/v1/languages", Name = nameof(GetLanguages))]
        [ProducesResponseOk]
        public async Task<ActionResult<PagedResponse<Language>>> GetLanguages([FromQuery] LanguagesFilter filterParams)
        {
            if (languages is null)
            {
                languages = new List<Language>();
                languages = await Helper.GetDataFromFileAsync<Language>(languageDataPath);
            }

            var result = GetLanguagesPagedResponse(
                filterParams,
                languages,
                nameof(GetLanguages));

            return Ok(result);
        }

        [HttpGet("api/v1/languages/recommended")]
        [ProducesResponseOk]
        public async Task<ActionResult<List<Language>>> GetRecommendedLanguages()
        {
            if (recommendedLanguages is null)
            {
                recommendedLanguages = new List<Language>();
                recommendedLanguages = await Helper.GetDataFromFileAsync<Language>(recommendedLanguagesDataPath);
            }

            return Ok(recommendedLanguages);
        }

        private PagedResponse<Language> GetLanguagesPagedResponse(
            LanguagesFilter filterParams,
            List<Language> languages,
            string routeName)
        {
            var totalResults = languages
                .Where(x =>
                    string.IsNullOrWhiteSpace(filterParams.Search) ||
                    x.Name.Contains(filterParams.Search, StringComparison.OrdinalIgnoreCase));

            var itemsToSkip = 
                filterParams.PageNumber > 1 ? (filterParams.PageNumber - 1) * filterParams.PageSize : 0;

            var results = totalResults
                .Skip(itemsToSkip)
                .Take(filterParams.PageSize)
                .Select(x => new Language
                {
                    Id = x.Id,
                    Code = x.Code,
                    Name = x.Name
                }).ToList();

            var pagedResponse = new PagedResponse<Language>(
                totalResults.Count(),
                results,
                filterParams.PageNumber,
                filterParams.PageSize,
                Url.RouteUrl(routeName)!,
                Request.QueryString.ToString());

            return pagedResponse;
        }
    }
}
