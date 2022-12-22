using API.Customization.Pagination;

namespace AdministrationSettings.API.Models.Hobbies
{
    public class HobbiesFilter : PagedFilter
    {
        public string? Search { get; set; }
    }
}
