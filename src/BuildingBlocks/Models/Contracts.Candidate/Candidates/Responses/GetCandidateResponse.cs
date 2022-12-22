using Contracts.Candidate.Courses.Responses;
using Contracts.Candidate.Educations.Responses;
using Contracts.Candidate.Notes.Responses;
using Contracts.Candidate.WorkExperiences.Responses;
using Contracts.Shared;
using Contracts.Shared.Responses;
using Domain.Seedwork.Enums;

namespace Contracts.Candidate.Candidates.Responses
{
    public class GetCandidateResponse
    {
        public Guid Id { get; set; }
        public string? Email { get; set; }
        public bool? IsEmailVerified { get; set; }
        public string? FullName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public CandidateStatus Status { get; set; }
        public ImageResponse? Picture { get; set; }
        public Position? CurrentPosition { get; set; }
        public DateTimeOffset? BirthDate { get; set; }
        public bool OpenForOpportunities { get; set; }
        public string? LinkedInUrl { get; set; }
        public string? PersonalWebsiteUrl { get; set; }
        public int? YearsOfExperience { get; set; } = null!;
        public ICollection<ActivityStatus> ActivityStatuses { get; set; } = new List<ActivityStatus>();
        public AddressWithLocation? Address { get; set; }
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public string? Currency { get; set; }
        public CandidateFreelance? Freelance { get; set; }
        public CandidatePermanent? Permanent { get; set; }
        public int? WeeklyWorkHours { get; set; }
        public PhoneFullResponse? Phone { get; set; }
        public bool IsShortlisted { get; set; }
        public SystemLanguage? SystemLanguage { get; set; }
        public LegalInformationAgreement? TermsAndConditions { get; set; }
        public LegalInformationAgreement? MarketingAcknowledgement { get; set; }
        public string? Bio { get; set; }
        public DocumentResponse? CurriculumVitae { get; set; }
        public DocumentResponse? Video { get; set; }
        public NoteResponse? Note { get; set; }
        public IEnumerable<Language> Languages { get; set; } = new List<Language>();
        public IEnumerable<Skill> Skills { get; set; } = new List<Skill>();
        public IEnumerable<Skill> DesiredSkills { get; set; } = new List<Skill>();
        public IEnumerable<Industry> Industries { get; set; } = new List<Industry>();
        public IEnumerable<WorkType> WorkTypes { get; set; } = new List<WorkType>();
        public IEnumerable<WorkingHoursType> WorkingHourTypes { get; set; } = new List<WorkingHoursType>();
        public IEnumerable<FormatType> Formats { get; set; } = new List<FormatType>();
        public IEnumerable<CandidateCourseResponse> CandidateCourses { get; set; } = new List<CandidateCourseResponse>();
        public IEnumerable<CandidateEducationResponse> CandidateEducations { get; set; } = new List<CandidateEducationResponse>();
        public IEnumerable<CandidateWorkExperienceResponse> CandidateWorkExperiences { get; set; } = new List<CandidateWorkExperienceResponse>();
        public IEnumerable<Hobby> Hobbies { get; set; } = new List<Hobby>();
    }
}
