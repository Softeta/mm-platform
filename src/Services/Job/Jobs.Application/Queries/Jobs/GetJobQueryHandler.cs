using API.Customization.Authorization.Constants;
using Jobs.Domain.Aggregates.JobAggregate;
using Jobs.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Jobs.Application.Queries.Jobs
{
    public class GetJobQueryHandler : IRequestHandler<GetJobQuery, Job?>
    {
        private readonly IJobContext _context;

        public GetJobQueryHandler(IJobContext context)
        {
            _context = context;
        }

        public async Task<Job?> Handle(GetJobQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Job, bool>> filterByCompanyId = job =>
                !request.CompanyId.HasValue || job.Company.Id == request.CompanyId.Value;

            Expression<Func<Job, bool>> filterByCandidateScope = job =>
                request.Scope != CustomScopes.FrontOffice.Candidate || job.IsPublished;

            return await _context.Jobs
                .Where(x => x.Id == request.Id)
                .Where(filterByCompanyId)
                .Where(filterByCandidateScope)
                .Include(j => j.Skills)
                .Include(j => j.Industries)
                .Include(j => j.AssignedEmployees)
                .Include(j => j.Languages)
                .Include(j => j.SeniorityLevels)
                .Include(j => j.InterestedCandidates)
                .Include(j => j.InterestedLinkedIns)
                .AsSplitQuery()
                .SingleOrDefaultAsync(cancellationToken);
        }
    }
}
