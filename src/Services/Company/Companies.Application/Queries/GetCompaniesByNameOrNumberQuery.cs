using Contracts.Company.Responses;
using Domain.Seedwork.Enums;
using MediatR;

namespace Companies.Application.Queries
{
    public record GetCompaniesByNameOrNumberQuery(
        string? Search,
        int PageNumber,
        int PageSize,
        CompanyStatus? CompanyStatus,
        string Scope
    ) : IRequest<GetCompaniesSearchResponse>;
}
