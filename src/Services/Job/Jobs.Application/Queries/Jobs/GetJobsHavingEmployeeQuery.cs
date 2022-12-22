using MediatR;

namespace Jobs.Application.Queries.Jobs
{
    public record GetJobsHavingEmployeeQuery(Guid EmployeeId) : IRequest<IEnumerable<Guid>>;
}
