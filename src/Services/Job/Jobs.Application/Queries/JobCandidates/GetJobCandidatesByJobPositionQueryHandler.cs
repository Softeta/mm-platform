using Jobs.Domain.Aggregates.JobCandidatesAggregate;
using Jobs.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Jobs.Application.Queries.JobsCandidates
{
    public class GetJobCandidatesByJobPositionQueryHandler : IRequestHandler<GetJobCandidatesByJobPositionQuery, List<Guid>>
    {
        private readonly IJobContext _context;

        public GetJobCandidatesByJobPositionQueryHandler(IJobContext context)
        {
            _context = context;
        }

        public async Task<List<Guid>> Handle(GetJobCandidatesByJobPositionQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<JobCandidates, bool>> findByJobPosition = jobCandidate =>
                jobCandidate.Position.Id == request.Id ||
                jobCandidate.SelectedCandidates.Any(x => x.Position != null && x.Position.Id == request.Id) ||
                jobCandidate.ArchivedCandidates.Any(x => x.Position != null && x.Position.Id == request.Id);

            return await _context.JobCandidates
                .Where(findByJobPosition)
                .Select(x => x.Id)
                .ToListAsync();
        }
    }
}
