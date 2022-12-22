using Jobs.Domain.Aggregates.JobAggregate;
using MediatR;

namespace Jobs.Application.Queries.Jobs
{
    public record GetJobsByContactPersonIdQuery(Guid ContactPersonId) : IRequest<List<Guid>>;
}
