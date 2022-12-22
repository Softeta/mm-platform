using Domain.Seedwork.Enums;

namespace BackOffice.Bff.API.Models.Dashboard.Responses
{
    public class DashboardJobResponse
    {
        public Guid JobId { get; set; }

        public string CompanyName { get; set; } = null!;

        public string? CompanyLogoUri { get; set; }

        public string Position { get; set; } = null!;

        public WorkType? Freelance { get; set; }

        public WorkType? Permanent { get; set; }

        public JobStage JobStage { get; set; }

        public DateTimeOffset? DeadlineDate { get; set; }

        public bool IsPriority { get; set; }
    }
}
