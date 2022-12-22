using Jobs.Domain.Aggregates.JobAggregate;
using Jobs.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Jobs.Application.Queries.Jobs
{
    public class GetJobsByPositionQueryHandler : IRequestHandler<GetJobsByPositionQuery, List<Guid>>
    {
        private readonly IJobContext _context;

        public GetJobsByPositionQueryHandler(IJobContext context)
        {
            _context = context;
        }

        public async Task<List<Guid>> Handle(GetJobsByPositionQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Job, bool>> findByPosition = job =>
                job.Position.Id == request.Id ||
                job.Company.ContactPersons.Any(x => x.Position != null && x.Position.Id == request.Id);

            return await _context.Jobs
                .Where(findByPosition)
                .Select(x => x.Id)
                .ToListAsync();
        }
    }
}
