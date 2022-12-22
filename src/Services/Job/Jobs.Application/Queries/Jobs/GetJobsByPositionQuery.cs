using MediatR;

namespace Jobs.Application.Queries.Jobs
{
    public record GetJobsByPositionQuery(Guid Id) : IRequest<List<Guid>>;
}
