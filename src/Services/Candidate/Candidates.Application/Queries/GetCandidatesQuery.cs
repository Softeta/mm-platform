using Contracts.Candidate.Candidates.Responses;
using Domain.Seedwork.Enums;
using MediatR;

namespace Candidates.Application.Queries
{
    public record GetCandidatesQuery(
        IEnumerable<Guid>? Ids,
        string? Name,
        IEnumerable<Guid>? Positions,
        double? Longitude,
        double? Latitude,
        int? RadiusInKm,
        bool? OpenForOpportunities,
        bool? IsFreelance,
        bool? IsPermanent,
        DateTimeOffset? AvailableFrom,
        decimal? HourlyBudgetTo,
        decimal? MonthlyBudgetTo,
        string? Currency,
        IEnumerable<CandidateStatus>? Statuses,
        Guid? JobId,
        string? OrderBy,
        int PageNumber,
        int PageSize,
        string? Search
    ) : IRequest<GetCandidatesResponse>;
}
