using API.Customization.Pagination;
using Domain.Seedwork.Enums;

namespace Candidates.API.Areas.Candidates.Models.Filters
{
    public class CandidatesFilter : PagedFilter
    {
        public string? Name { get; set; }
        public IEnumerable<Guid>? Positions { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public int? RadiusInKm { get; set; }
        public bool? OpenForOpportunities { get; set; }
        public bool? IsFreelance { get; set; }
        public bool? IsPermanent { get; set; }
        public DateTimeOffset? AvailableFrom { get; set; }
        public decimal? HourlyBudgetTo { get; set; }
        public decimal? MonthlyBudgetTo { get; set; }
        public string? Currency { get; set; }
        public IEnumerable<CandidateStatus>? Statuses { get; set; }
        public Guid? JobId { get; set; }
        public string? OrderBy { get; set; }
        public IEnumerable<Guid>? Candidates { get; set; }
        public string? Search { get; set; }
    }
}
