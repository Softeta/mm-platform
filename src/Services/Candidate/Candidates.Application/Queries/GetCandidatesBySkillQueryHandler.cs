using Candidates.Domain.Aggregates.CandidateAggregate;
using Candidates.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Candidates.Application.Queries
{
    internal class GetCandidatesBySkillQueryHandler : IRequestHandler<GetCandidatesBySkillQuery, List<Guid>>
    {
        private readonly ICandidateContext _context;

        public GetCandidatesBySkillQueryHandler(ICandidateContext context)
        {
            _context = context;
        }

        public async Task<List<Guid>> Handle(GetCandidatesBySkillQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Candidate, bool>> findBySkills = candidate =>
                candidate.Skills.Any(x => x.SkillId == request.Id) ||
                candidate.DesiredSkills.Any(x => x.SkillId == request.Id) ||
                candidate.WorkExperiences.Any(x => x.Skills.Any(x => x.SkillId == request.Id));

            return await _context.Candidates
                .Where(findBySkills)
                .Select(x => x.Id)
                .ToListAsync();
        }
    }
}
