using API.Customization.Pagination;

namespace Companies.API.Models
{
    public class CompanySearchFilters : PagedFilter
    {
        public string? Search { get; set; }
    }
}
