using Contracts.Job;
using Contracts.Shared;
using Domain.Seedwork.Enums;
using Jobs.Domain.Aggregates.JobAggregate;
using MediatR;

namespace Jobs.Application.Commands
{
    public record UpdateJobCommand(
        Guid JobId,
        Employee? Owner,
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
        IEnumerable<WorkingHoursType> WorkingHourTypes,
        IEnumerable<WorkType> WorkTypes,
        IEnumerable<Employee> AssignedEmployees,
        IEnumerable<Skill> Skills,
        IEnumerable<Industry> Industries,
        IEnumerable<SeniorityLevel> Seniorities,
        IEnumerable<Language> Languages,
        IEnumerable<FormatType> Formats,
        IEnumerable<InterestedCandidate> InterestedCandidates,
        IEnumerable<string> InterestedLinkedIns,
        string Scope
    ) : IRequest<Job>;
}
