using API.Customization.Pagination;

namespace AdministrationSettings.API.Models.Industries
{
    public class IndustriesFilter : PagedFilter
    {
        public string? Search { get; set; }
    }
}
