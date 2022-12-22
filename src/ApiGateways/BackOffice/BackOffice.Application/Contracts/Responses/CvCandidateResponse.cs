using BackOffice.Application.Contracts.Responses.Cv;
using Contracts.Shared;
using Contracts.Shared.Helpers;

namespace BackOffice.Application.Contracts.Responses;

public class CvCandidateResponse
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? FullName => FullNameHelper.GetFullName(FirstName, LastName);
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? LinkedInUrl { get; set; }
    public AddressWithLocation? Address { get; set; }
    public Position? CurrentPosition { get; set; }
    public IEnumerable<Language> Languages { get; set; } = new List<Language>();
    public IEnumerable<Skill> Skills { get; set; } = new List<Skill>();
    public IEnumerable<CandidateCvCourseResponse> CandidateCourses { get; set; } = new List<CandidateCvCourseResponse>();
    public IEnumerable<CandidateCvEducationResponse> CandidateEducations { get; set; } = new List<CandidateCvEducationResponse>();
    public IEnumerable<CandidateCvWorkExperienceResponse> CandidateWorkExperiences { get; set; } = new List<CandidateCvWorkExperienceResponse>();
}