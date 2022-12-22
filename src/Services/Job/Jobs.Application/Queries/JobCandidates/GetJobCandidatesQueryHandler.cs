using Jobs.Domain.Aggregates.JobCandidatesAggregate;
using Jobs.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jobs.Application.Queries.JobsCandidates
{
    public class GetJobCandidatesQueryHandler : IRequestHandler<GetJobCandidatesQuery, JobCandidates?>
    {
        private readonly IJobContext _context;

        public GetJobCandidatesQueryHandler(IJobContext context)
        {
            _context = context;
        }

        public async Task<JobCandidates?> Handle(GetJobCandidatesQuery request, CancellationToken cancellationToken)
        {
            return await _context.JobCandidates.Where(x => x.Id == request.Id)
                .Include(j => j.SelectedCandidates)
                .Include(j => j.ArchivedCandidates)
                .AsSplitQuery()
                .SingleOrDefaultAsync(cancellationToken);
        }
    }
}
