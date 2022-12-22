using Contracts.Job;
using Contracts.Job.Companies.Requests;
using Contracts.Shared;
using Domain.Seedwork.Enums;
using MediatR;

namespace Jobs.Application.Commands;

public record CreateJobCommand(
    CreateJobCompanyRequest Company,
    Employee Owner,
    Position Position,
    DateTimeOffset? DeadLineDate,
    string? Description,
    DateTimeOffset? StartDate,
    DateTimeOffset? EndDate,
    int? WeeklyWorkHours,
    string? Currency,
    Freelance? Freelance,
    Permanent? Permanent,
    YearExperience? YearExperience,
    bool IsPriority,
    bool IsUrgent,
    ICollection<WorkingHoursType> WorkingHourTypes,
    ICollection<WorkType> WorkTypes,
    IEnumerable<Employee> AssignedEmployees,
    IEnumerable<Skill> Skills,
    IEnumerable<Industry> Industries,
    IEnumerable<SeniorityLevel> Seniorities,
    IEnumerable<Language> Languages,
    ICollection<FormatType> Formats,
    IEnumerable<InterestedCandidate> InterestedCandidates,
    IEnumerable<string> InterestedLinkedIns
    ) : IRequest<Guid>;
