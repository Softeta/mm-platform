using Candidates.Domain.Aggregates.CandidateAggregate;
using Candidates.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Candidates.Application.Queries
{
    public class GetCandidateByExternalIdQueryHandler : IRequestHandler<GetCandidateByExternalIdQuery, Candidate?>
    {
        private readonly CandidateContext _context;

        public GetCandidateByExternalIdQueryHandler(CandidateContext context)
        {
            _context = context;
        }

        public async Task<Candidate?> Handle(GetCandidateByExternalIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Candidates
                .Include(c => c.Skills)
                .Include(c => c.DesiredSkills)
                .Include(c => c.Industries)
                .Include(c => c.Courses)
                .Include(c => c.Educations)
                .Include(c => c.WorkExperiences)
                    .ThenInclude(w => w.Skills)
                .Include(c => c.ActivityStatuses)
                .Include(c => c.Languages)
                .AsSplitQuery()
                .AsNoTracking()
                .SingleOrDefaultAsync(x =>
                    x.ExternalId != null &&
                    x.ExternalId == request.ExternalId,
                 cancellationToken);
        }
    }
}
