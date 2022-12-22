using Jobs.Domain.Aggregates.JobAggregate;
using Jobs.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Jobs.Application.Queries.Jobs
{
    public class GetJobsByContactPersonIdQueryHandler : IRequestHandler<GetJobsByContactPersonIdQuery, List<Guid>>
    {
        private readonly IJobContext _context;

        public GetJobsByContactPersonIdQueryHandler(IJobContext context)
        {
            _context = context;
        }

        public async Task<List<Guid>> Handle(GetJobsByContactPersonIdQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Job, bool>> findByContactPersonId = job =>
                job.Company.ContactPersons.Any(x => x.PersonId == request.ContactPersonId);

            return await _context.Jobs
                .Where(findByContactPersonId)
                .Select(x => x.Id)
                .ToListAsync();
        }
    }
}
