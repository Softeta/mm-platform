using MediatR;

namespace Jobs.Application.Queries.Jobs
{
    public record GetJobsHavingContactPersonQuery(Guid ContactPersonId) : IRequest<IEnumerable<Guid>>;
}
