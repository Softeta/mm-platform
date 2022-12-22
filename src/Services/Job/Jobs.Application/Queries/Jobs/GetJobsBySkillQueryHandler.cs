using Jobs.Domain.Aggregates.JobAggregate;
using Jobs.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Jobs.Application.Queries.Jobs
{
    internal class GetJobsBySkillQueryHandler : IRequestHandler<GetJobsBySkillQuery, List<Guid>>
    {
        private readonly IJobContext _context;

        public GetJobsBySkillQueryHandler(IJobContext context)
        {
            _context = context;
        }

        public async Task<List<Guid>> Handle(GetJobsBySkillQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Job, bool>> findBySkills = job =>
                job.Skills.Any(x => x.SkillId == request.Id);

            return await _context.Jobs
                 .Where(findBySkills)
                 .Select(j => j.Id)
                 .ToListAsync();
        }
    }
}
