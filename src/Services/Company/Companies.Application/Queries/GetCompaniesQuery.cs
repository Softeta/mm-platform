using Contracts.Company.Responses;
using Domain.Seedwork.Enums;
using MediatR;

namespace Companies.Application.Queries
{
    public record GetCompaniesQuery(
        double? Longitude,
        double? Latitude,
        int? RadiusInKm,
        int PageNumber,
        int PageSize,
        string? Search,
        IEnumerable<Guid>? Companies,
        IEnumerable<Guid>? Industries,
        IEnumerable<CompanyStatus>? CompanyStatuses
    ) : IRequest<GetCompaniesResponse>;
}
