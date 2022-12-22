using API.Customization.Pagination;
using Domain.Seedwork.Enums;

namespace Companies.API.Models
{
    public class CompanyFilters : PagedFilter
    {
        public string? Search { get; set; }

        public double? Longitude { get; set; }

        public double? Latitude { get; set; }

        public int? RadiusInKm { get; set; }

        public IEnumerable<CompanyStatus>? Statuses { get; set; }

        public IEnumerable<Guid>? Companies { get; set; }

        public IEnumerable<Guid>? Industries { get; set; }
    }
}
