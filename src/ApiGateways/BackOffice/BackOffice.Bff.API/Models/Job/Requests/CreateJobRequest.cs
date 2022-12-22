using Contracts.Job;
using Contracts.Shared;
using Domain.Seedwork.Enums;
using System.ComponentModel.DataAnnotations;

namespace BackOffice.Bff.API.Models.Job.Requests
{
    public class CreateJobRequest
    {
        [Required]
        public JobCompanyRequest Company { get; set; } = null!;

        [Required]
        public Guid OwnerId { get; set; }

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

        public bool IsPriority { get; set; }

        public bool IsUrgent { get; set; }

        public ICollection<WorkingHoursType> WorkingHourTypes { get; set; } = new List<WorkingHoursType>();

        public ICollection<WorkType> WorkTypes { get; set; } = new List<WorkType>();

        public ICollection<Guid> AssignedEmployees { get; set; } = new List<Guid>();

        public ICollection<Skill> Skills { get; set; } = new List<Skill>();

        public ICollection<Industry> Industries { get; set; } = new List<Industry>();

        public ICollection<SeniorityLevel> Seniorities { get; set; } = new List<SeniorityLevel>();

        public ICollection<Language> Languages { get; set; } = new List<Language>();

        public ICollection<FormatType> Formats { get; set; } = new List<FormatType>();

        public ICollection<Guid> InterestedCandidates { get; set; } = new List<Guid>();

        public ICollection<string> InterestedLinkedIns { get; set; } = new List<string>();
    }
}
