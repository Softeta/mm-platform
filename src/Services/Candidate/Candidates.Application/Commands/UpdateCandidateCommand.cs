using Candidates.Domain.Aggregates.CandidateAggregate;
using Contracts.Candidate;
using Contracts.Shared;
using Contracts.Shared.Requests;
using Domain.Seedwork.Enums;

namespace Candidates.Application.Commands
{
    public record UpdateCandidateCommand(
        Guid CandidateId,
        string? Email,
        string? FirstName,
        string? LastName,
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
        UpdateFileCacheRequest CurriculumVitae,
        UpdateFileCacheRequest Video,
        UpdateFileCacheRequest Picture,
        IEnumerable<ActivityStatus> ActivityStatuses,
        ICollection<WorkingHoursType> WorkingHourTypes,
        ICollection<WorkType> WorkTypes,
        ICollection<FormatType> Formats,
        IEnumerable<Industry> Industries,
        IEnumerable<Skill> Skills,
        IEnumerable<Skill> DesiredSkills,
        IEnumerable<Language> Languages,
        IEnumerable<Hobby> Hobbies,
        Guid UserId,
        string Scope
    ) : ModifyCandidateBaseCommand<Candidate>(CandidateId, UserId, Scope);
}
