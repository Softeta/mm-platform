using Candidates.Domain.Aggregates.CandidateJobsAggregate;
using Candidates.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Candidates.Application.Queries
{
    public class GetCandidateJobsByJobPositionQueryHandler : IRequestHandler<GetCandidateJobsByJobPositionQuery, List<Guid>>
    {
        private readonly ICandidateContext _context;

        public GetCandidateJobsByJobPositionQueryHandler(ICandidateContext context)
        {
            _context = context;
        }

        public async Task<List<Guid>> Handle(GetCandidateJobsByJobPositionQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<CandidateJobs, bool>> findByJobPosition = candidateJobs =>
                candidateJobs.AppliedInJobs.Any(x => x.Position.Id == request.Id) || 
                candidateJobs.ArchivedInJobs.Any(x => x.Position.Id == request.Id) ||
                candidateJobs.SelectedInJobs.Any(x => x.Position.Id == request.Id);

            return await _context.CandidateJobs
                .Where(findByJobPosition)
                .Select(x => x.Id)
                .ToListAsync();
        }
    }
}
