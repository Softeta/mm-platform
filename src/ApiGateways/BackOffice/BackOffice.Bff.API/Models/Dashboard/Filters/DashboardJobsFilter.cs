using API.Customization.Pagination;
using Domain.Seedwork.Enums;
using System.Collections.ObjectModel;

namespace BackOffice.Bff.API.Models.Dashboard.Filters
{
    public class DashboardJobsFilter : PagedFilter
    {
        public Collection<Guid>? Companies { get; set; }
        public Collection<Guid>? Positions { get; set; }
        public DateTimeOffset? DeadLineDate { get; set; }
        public Collection<WorkType>? WorkTypes { get; set; }
        public Collection<JobStage>? JobStages { get; set; }
        public Collection<string>? Locations { get; set; }
        public Guid? Owner { get; set; }
        public string? Search { get; set; }
        public JobOrderBy? OrderBy { get; set; }
    }
}
