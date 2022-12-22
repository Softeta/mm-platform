using Contracts.Candidate.Courses.Requests;
using Contracts.Candidate.Educations.Requests;
using Contracts.Candidate.WorkExperiences.Requests;
using Contracts.Shared;
using Contracts.Shared.Requests;
using Domain.Seedwork.Enums;
using System.ComponentModel.DataAnnotations;

namespace Contracts.Candidate.Candidates.Requests
{
    public class InitializeCandidateRequest
    {
        public string? Email { get; set; }

        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        public AddFileCacheRequest? Picture { get; set; }

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

        public AddFileCacheRequest? CurriculumVitae { get; set; }

        public AddFileCacheRequest? Video { get; set; }

        public ICollection<WorkType> WorkTypes { get; set; } = new List<WorkType>();

        public ICollection<WorkingHoursType> WorkingHourTypes { get; set; } = new List<WorkingHoursType>();

        public ICollection<FormatType> Formats { get; set; } = new List<FormatType>();

        public IEnumerable<Industry> Industries { get; set; } = new List<Industry>();

        public IEnumerable<Skill> Skills { get; set; } = new List<Skill>();

        public IEnumerable<Skill> DesiredSkills { get; set; } = new List<Skill>();

        public IEnumerable<Language> Languages { get; set; } = new List<Language>();

        public IEnumerable<Hobby> Hobbies { get; set; } = new List<Hobby>();

        public IEnumerable<AddCandidateCourseRequest> Courses { get; set; } = new List<AddCandidateCourseRequest>();

        public IEnumerable<AddCandidateEducationRequest> Educations { get; set; } = new List<AddCandidateEducationRequest>();

        public IEnumerable<CandidateWorkExperienceRequest> WorkExperiences { get; set; } = new List<CandidateWorkExperienceRequest>();
    }
}
