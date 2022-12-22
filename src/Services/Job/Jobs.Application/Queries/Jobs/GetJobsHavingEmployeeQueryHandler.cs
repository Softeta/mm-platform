using Jobs.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jobs.Application.Queries.Jobs
{
    public class GetJobsHavingEmployeeQueryHandler : IRequestHandler<GetJobsHavingEmployeeQuery, IEnumerable<Guid>>
    {
        private readonly IJobContext _context;

        public GetJobsHavingEmployeeQueryHandler(IJobContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Guid>> Handle(GetJobsHavingEmployeeQuery request, CancellationToken cancellationToken)
        {
            var jobsOfAssignedEmployees = _context.JobAssignedEmployees
                .Where(x => x.Employee.Id == request.EmployeeId)
                .Select(x => x.JobId);

            var jobIds = await _context.Jobs
                .Where(x => x.Owner != null && x.Owner.Id == request.EmployeeId)
                .Select(x => x.Id)
                .Concat(jobsOfAssignedEmployees)
                .Distinct()
                .ToListAsync(cancellationToken);

            return jobIds;
        }
    }
}
