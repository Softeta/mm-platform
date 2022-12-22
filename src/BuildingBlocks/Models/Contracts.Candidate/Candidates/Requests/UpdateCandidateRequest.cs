using Contracts.Shared;
using Contracts.Shared.Requests;
using Domain.Seedwork.Enums;
using System.ComponentModel.DataAnnotations;

namespace Contracts.Candidate.Candidates.Requests
{
    public class UpdateCandidateRequest
    {
        public string? Email { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public Position? CurrentPosition { get; set; }

        public DateTimeOffset? BirthDate { get; set; }

        [Required]
        public bool OpenForOpportunities { get; set; }

        public string? LinkedInUrl { get; set; }

        public string? PersonalWebsiteUrl { get; set; }

        public AddressWithLocation? Address { get; set; }

        public DateTimeOffset? StartDate { get; set; }

        public DateTimeOffset? EndDate { get; set; }

        public string? Currency { get; set; }

        public int? WeeklyWorkHours { get; set; }

        public CandidateFreelance? Freelance { get; set; }

        public CandidatePermanent? Permanent { get; set; }

        public PhoneRequest? Phone { get; set; }

        public int? YearsOfExperience { get; set; }

        public string? Bio { get; set; }

        public UpdateFileCacheRequest CurriculumVitae { get; set; } = new UpdateFileCacheRequest { CacheId = null, HasChanged = false }!;

        public UpdateFileCacheRequest Video { get; set; } = new UpdateFileCacheRequest { CacheId = null, HasChanged = false };

        public UpdateFileCacheRequest Picture { get; set; } = new UpdateFileCacheRequest { CacheId = null, HasChanged = false };

        public ICollection<ActivityStatus> ActivityStatuses { get; set; } = new List<ActivityStatus>();

        public ICollection<WorkType> WorkTypes { get; set; } = new List<WorkType>();

        public ICollection<WorkingHoursType> WorkingHourTypes { get; set; } = new List<WorkingHoursType>();

        public ICollection<FormatType> Formats { get; set; } = new List<FormatType>();

        public IEnumerable<Industry> Industries { get; set; } = new List<Industry>();

        public IEnumerable<Skill> Skills { get; set; } = new List<Skill>();

        public IEnumerable<Skill> DesiredSkills { get; set; } = new List<Skill>();

        public IEnumerable<Language> Languages { get; set; } = new List<Language>();

        public IEnumerable<Hobby> Hobbies { get; set; } = new List<Hobby>();
    }
}
