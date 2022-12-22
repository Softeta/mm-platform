using Candidates.Domain.Aggregates.CandidateAggregate;
using Candidates.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Candidates.Application.Queries
{
    public class GetCandidatesByJobPositionQueryHandler : IRequestHandler<GetCandidatesByJobPositionQuery, List<Guid>>
    {
        private readonly ICandidateContext _context;

        public GetCandidatesByJobPositionQueryHandler(ICandidateContext context)
        {
            _context = context;
        }

        public async Task<List<Guid>> Handle(GetCandidatesByJobPositionQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Candidate, bool>> findByJobPosition = candidate =>
                candidate.CurrentPosition != null && candidate.CurrentPosition.Id == request.Id ||
                candidate.WorkExperiences.Any(x => x.Position.Id == request.Id);

            return await _context.Candidates
                .Where(findByJobPosition)
                .Select(x => x.Id)
                .ToListAsync();
        }
    }
}
