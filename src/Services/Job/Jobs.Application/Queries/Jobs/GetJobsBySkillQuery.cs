using Jobs.Domain.Aggregates.JobAggregate;
using MediatR;

namespace Jobs.Application.Queries.Jobs
{
    public record GetJobsBySkillQuery(Guid Id) : IRequest<List<Guid>>;
}
