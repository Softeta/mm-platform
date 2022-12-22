using Jobs.Domain.Aggregates.JobAggregate.Entities;
using MediatR;

namespace Jobs.Application.Queries.Jobs
{
    public record GetJobContactPersonByEmailQuery(Guid JobId, string EmailAddress) : IRequest<JobContactPerson>;
}
