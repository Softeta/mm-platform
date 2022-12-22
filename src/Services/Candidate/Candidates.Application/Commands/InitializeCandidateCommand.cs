using Candidates.Domain.Aggregates.CandidateAggregate;
using Contracts.Candidate;
using Contracts.Candidate.Courses.Requests;
using Contracts.Candidate.Educations.Requests;
using Contracts.Candidate.WorkExperiences.Requests;
using Contracts.Shared;
using Contracts.Shared.Requests;
using Domain.Seedwork.Enums;
using MediatR;

namespace Candidates.Application.Commands
{
    public record InitializeCandidateCommand(
        string? Email,
        string FirstName,
        string LastName,
        AddFileCacheRequest? Picture,
        Position? CurrentPosition,
        DateTimeOffset? BirthDate,
        bool OpenForOpportunities,
        string? LinkedInUrl,
        string? PersonalWebsiteUrl,
        AddressWithLocation? Address,
        DateTimeOffset? StartDate,
        DateTimeOffset? EndDate,
        string? Currency,
        int? WeeklyWorkHours,
        CandidateFreelance? Freelance,
        CandidatePermanent? Permanent,
        PhoneRequest? Phone,
        int? YearsOfExperience,
        string? Bio,
        AddFileCacheRequest? CurriculumVitae,
        AddFileCacheRequest? Video,
        ICollection<WorkingHoursType> WorkingHourTypes,
        ICollection<WorkType> WorkTypes,
        ICollection<FormatType> Formats,
        IEnumerable<Industry> Industries,
        IEnumerable<Skill> Skills,
        IEnumerable<Skill> DesiredSkills,
        IEnumerable<Language> Languages,
        IEnumerable<Hobby> Hobbies,
        IEnumerable<AddCandidateCourseRequest> Courses,
        IEnumerable<AddCandidateEducationRequest> Educations,
        IEnumerable<CandidateWorkExperienceRequest> WorkExperiences
    ) : IRequest<Candidate>;
}
