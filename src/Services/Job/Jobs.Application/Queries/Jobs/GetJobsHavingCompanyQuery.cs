using MediatR;

namespace Jobs.Application.Queries.Jobs
{
    public record GetJobsHavingCompanyQuery(Guid CompanyId) : IRequest<IEnumerable<Guid>>;
}
