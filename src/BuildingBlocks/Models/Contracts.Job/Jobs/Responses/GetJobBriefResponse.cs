using Contracts.Job.Companies.Responses;
using Domain.Seedwork.Enums;
using System.Text.Json.Serialization;

namespace Contracts.Job.Jobs.Responses
{
    public class GetJobBriefResponse
    {
        public Guid JobId { get; set; }

        public string CompanyName { get; set; } = null!;

        public string? CompanyLogoUri { get; set; }

        public string Position { get; set; } = null!;

        public WorkType? Freelance { get; set; }

        public WorkType? Permanent { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public JobStage JobStage { get; set; }

        public DateTimeOffset? DeadlineDate { get; set; }

        public IEnumerable<Employee> AssignedTo { get; set; } = new List<Employee>();

        public Employee? Owner { get; set; }

        public JobContactPersonResponse? MainContact { get; set; }

        public bool IsPriority { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public bool IsArchived { get; set; }
    }
}
