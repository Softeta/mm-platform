using Jobs.Domain.Aggregates.JobAggregate;
using MediatR;

namespace Jobs.Application.Queries.Jobs
{
    public record GetJobQuery(Guid Id, Guid? CompanyId, string Scope) : IRequest<Job>;
}
