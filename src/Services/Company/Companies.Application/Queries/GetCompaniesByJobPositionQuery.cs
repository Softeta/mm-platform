using MediatR;

namespace Companies.Application.Queries
{
    public record GetCompaniesByJobPositionQuery(Guid Id) : IRequest<List<Guid>>;
}
