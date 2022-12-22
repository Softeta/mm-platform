using Candidates.Domain.Aggregates.CandidateAggregate;
using Candidates.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Candidates.Application.Queries
{
    public class GetCandidateByIdQueryHandler : IRequestHandler<GetCandidateByIdQuery, Candidate?>
    {
        private readonly ICandidateContext _context;

        public GetCandidateByIdQueryHandler(ICandidateContext context)
        {
            _context = context;
        }

        public async Task<Candidate?> Handle(GetCandidateByIdQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.Candidates
                .Include(c => c.Skills)
                .Include(c => c.DesiredSkills)
                .Include(c => c.Industries)
                .Include(c => c.Languages)
                .Include(c => c.Hobbies)
                .Include(c => c.Courses)
                .Include(c => c.Educations)
                .Include(c => c.WorkExperiences)
                    .ThenInclude(c => c.Skills)
                .Include(c => c.ActivityStatuses)
                .AsSplitQuery()
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        }
    }
}
