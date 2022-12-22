using Domain.Seedwork.Enums;
using MediatR;

namespace Candidates.Application.Commands.CandidateInJobs
{
    public record CandidateApplyToJobCommand(
        Guid CandidateId,
        Guid JobId,
        JobStage JobStage,
        Guid PositionId,
        string PositionCode,
        Guid? PositionAliasToId,
        string? PositionAliasToCode,
        Guid CompanyId,
        string CompanyName,
        string? CompanyLogo,
        WorkType? Freelance,
        WorkType? Permanent,
        DateTimeOffset? StartDate,
        DateTimeOffset? DeadlineDate) : INotification;
}
