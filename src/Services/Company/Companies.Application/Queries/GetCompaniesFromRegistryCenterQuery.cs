using Contracts.Company.Responses;
using MediatR;

namespace Companies.Application.Queries
{
    public record GetCompaniesFromRegistryCenterQuery(
        string? Search,
        int PageNumber,
        int PageSize
    ) : IRequest<GetCompaniesSearchResponse>;
}
