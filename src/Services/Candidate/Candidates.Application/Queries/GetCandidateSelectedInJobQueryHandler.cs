using Candidates.Domain.Aggregates.CandidateJobsAggregate.Entities;
using Candidates.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Candidates.Application.Queries
{
    public class GetCandidateSelectedInJobQueryHandler : IRequestHandler<GetCandidateSelectedInJobQuery, CandidateSelectedInJob?>
    {
        private readonly ICandidateContext _context;

        public GetCandidateSelectedInJobQueryHandler(ICandidateContext context)
        {
            _context = context;
        }

        public async Task<CandidateSelectedInJob?> Handle(GetCandidateSelectedInJobQuery request, CancellationToken cancellationToken)
        {
            return await _context.CandidateSelectedInJobs
                .Where(x => x.JobId == request.JobId)
                .Where(x => x.CandidateId == request.CandidateId)
                .AsNoTracking()
                .SingleOrDefaultAsync(cancellationToken);
        }
    }
}
