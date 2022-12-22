using Candidates.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Candidates.Application.Queries
{
    public class GetAppliedJobIdsQueryHandler : IRequestHandler<GetAppliedJobIdsQuery, List<Guid>>
    {
        private readonly ICandidateContext _context;

        public GetAppliedJobIdsQueryHandler(ICandidateContext context)
        {
            _context = context;
        }

        public async Task<List<Guid>> Handle(GetAppliedJobIdsQuery request, CancellationToken cancellationToken)
        {
            return await _context.CandidateAppliedInJobs
                .AsNoTracking()
                .Where(x => x.CandidateId == request.CandidateId)
                .Select(x => x.JobId)        
                .ToListAsync();
        }
    }
}
