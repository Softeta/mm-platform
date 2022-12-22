using API.Customization.Pagination;
using API.Customization.Swagger;
using Domain.Seedwork.Enums;

namespace Jobs.API.Models.Jobs.Filters
{
    public class JobsFilter : PagedFilter
    {
        [SwaggerIgnore]
        public Guid? UserId { get; set; }

        public double? Longitude { get; set; }

        public double? Latitude { get; set; }

        public int? RadiusInKm { get; set; }

        public IEnumerable<Guid>? AssignedEmployees { get; set; }

        public IEnumerable<Guid>? Companies { get; set; }

        public IEnumerable<Guid>? Positions { get; set; }

        public DateTimeOffset? DeadLineDate { get; set; }

        public IEnumerable<WorkType>? WorkTypes { get; set; }

        public IEnumerable<JobStage>? JobStages { get; set; }

        public IEnumerable<Guid>? ExcludedJobIds { get; set; }

        public Guid? Owner { get; set; }

        public DateTimeOffset? CreatedAt { get; set; }

        public string? Search { get; set; }

        public JobOrderBy? OrderBy { get; set; }
        
        public IEnumerable<Guid>? JobIds { get; set; }
    }
}
