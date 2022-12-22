using Jobs.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jobs.Application.Queries.Jobs
{
    public class GetJobsHavingContactPersonQueryHandler : IRequestHandler<GetJobsHavingContactPersonQuery, IEnumerable<Guid>>
    {
        private readonly IJobContext _context;

        public GetJobsHavingContactPersonQueryHandler(IJobContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Guid>> Handle(GetJobsHavingContactPersonQuery request, CancellationToken cancellationToken)
        {
            return await _context.Jobs
                .Where(x => x.Company.ContactPersons.Any(c => c.PersonId == request.ContactPersonId))
                .Select(x => x.Id)
                .ToListAsync(cancellationToken);
        }
    }
}
