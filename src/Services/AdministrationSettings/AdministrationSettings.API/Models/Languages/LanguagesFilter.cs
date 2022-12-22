using API.Customization.Pagination;

namespace AdministrationSettings.API.Models.Languages
{
    public class LanguagesFilter : PagedFilter
    {
        public string? Search { get; set; }
    }
}
