using Contracts.Job.Companies.Responses;
using Contracts.Shared;
using Domain.Seedwork.Enums;

namespace Contracts.Job.Jobs.Responses
{
    public class GetJobResponse
    {
        public Guid Id { get; set; }

        public CompanyResponse Company { get; set; } = null!;

        public Employee? Owner { get; set; }

        public Position Position { get; set; } = null!;

        public YearExperience? YearExperience { get; set; }

        public DateTimeOffset? DeadLineDate { get; set; }

        public string? Description { get; set; }

        public JobStage Stage { get; set; }

        public bool IsPublished { get; set; }

        public string? Industry { get; set; }

        public DateTimeOffset? SharingDate { get; set; }

        public DateTimeOffset? StartDate { get; set; }

        public DateTimeOffset? EndDate { get; set; }

        public string? Currency { get; set; }

        public Freelance? Freelance { get; set; }

        public Permanent? Permanent { get; set; }

        public bool IsPriority { get; set; }

        public int? WeeklyHours { get; set; }

        public bool IsArchived { get; set; }

        public bool IsUrgent { get; set; }

        public bool IsActivated { get; set; }

        public Guid? ParentJobId { get; set; }

        public string? Location { get; set; }

        public bool IsSelectionStarted { get; set; }

        public ICollection<WorkingHoursType> WorkingHourTypes { get; set; } = new List<WorkingHoursType>();

        public IEnumerable<WorkType> WorkTypes { get; set; } = new List<WorkType>();

        public IEnumerable<Employee> AssignedEmployees { get; set; } = new List<Employee>();

        public IEnumerable<Skill> Skills { get; set; } = new List<Skill>();

        public IEnumerable<Industry> Industries { get; set; } = new List<Industry>();

        public IEnumerable<SeniorityLevel> Seniorities { get; set; } = new List<SeniorityLevel>();

        public IEnumerable<Language> Languages { get; set; } = new List<Language>();

        public IEnumerable<FormatType> Formats { get; set; } = new List<FormatType>();

        public IEnumerable<InterestedCandidate> InterestedCandidates { get; set; } = new List<InterestedCandidate>();

        public IEnumerable<string> InterestedLinkedIns { get; set; } = new List<string>();
    }
}
