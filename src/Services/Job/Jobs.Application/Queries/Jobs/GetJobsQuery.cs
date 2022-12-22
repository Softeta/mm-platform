using Contracts.Job.Jobs.Responses;
using Domain.Seedwork.Enums;
using MediatR;

namespace Jobs.Application.Queries.Jobs
{
    public record GetJobsQuery(
        Guid? UserId,
        double? Longitude,
        double? Latitude,
        int? RadiusInKm,
        IEnumerable<Guid>? AssignedEmployees,
        IEnumerable<Guid>? Companies,
        IEnumerable<Guid>? Positions,
        DateTimeOffset? DeadLineDate,
        IEnumerable<WorkType>? WorkTypes,
        IEnumerable<JobStage>? JobStages,
        IEnumerable<Guid>? ExcludedJobIds,
        Guid? Owner,
        DateTimeOffset? CreatedAt,
        JobOrderBy? OrderBy,
        IEnumerable<Guid>? JobIds,
        int PageNumber,
        int PageSize,
        Guid? CompanyId,
        string Scope,
        string? Search
        ) : IRequest<GetJobsResponse>;
}
