using Contracts.Shared;
using Domain.Seedwork.Enums;
using System.ComponentModel.DataAnnotations;

namespace Contracts.Job.Jobs.Requests
{
    public class UpdateJobRequest
    {
        public Employee? Owner { get; set; } 

        [Required]
        public Position Position { get; set; } = null!;

        public DateTimeOffset? DeadLineDate { get; set; }

        public string? Description { get; set; }

        public DateTimeOffset? StartDate { get; set; }

        public DateTimeOffset? EndDate { get; set; }

        [StringLength(3)]
        public string? Currency { get; set; }

        public int? WeeklyWorkHours { get; set; }

        public Freelance? Freelance { get; set; }

        public Permanent? Permanent { get; set; }

        public YearExperience? YearExperience { get; set; }

        public bool IsPriority { get; set; }

        public bool IsUrgent { get; set; }

        public ICollection<WorkingHoursType> WorkingHourTypes { get; set; } = new List<WorkingHoursType>();

        public IEnumerable<WorkType> WorkTypes { get; set; } = new List<WorkType>();

        public IEnumerable<Employee> AssignedEmployees { get; set; } = new List<Employee>();

        public IEnumerable<Skill> Skills { get; set; } = new List<Skill>();

        public IEnumerable<Industry> Industries { get; set; } = new List<Industry>();

        public IEnumerable<SeniorityLevel> Seniorities { get; set; } = new List<SeniorityLevel>();

        public IEnumerable<Language> Languages { get; set; } = new List<Language>();

        public IEnumerable<FormatType> Formats { get; set; } = new List<FormatType>();

        public IEnumerable<InterestedCandidate> InteresedCandidates { get; set; } = new List<InterestedCandidate>();

        public IEnumerable<string> InterestedLinkedIns { get; set; } = new List<string>();
    }
}
