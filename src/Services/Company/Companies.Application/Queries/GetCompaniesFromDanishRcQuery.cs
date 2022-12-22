using Contracts.Company.Responses;
using MediatR;

namespace Companies.Application.Queries
{
    public record GetCompaniesFromDanishRcQuery(
        string? Search,
        int PageSize
    ) : IRequest<GetCompaniesSearchResponse>;
}
