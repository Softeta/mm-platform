using Jobs.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jobs.Application.Queries.Jobs
{
    public class GetJobsHavingCompanyQueryHandler : IRequestHandler<GetJobsHavingCompanyQuery, IEnumerable<Guid>>
    {
        private readonly IJobContext _context;

        public GetJobsHavingCompanyQueryHandler(IJobContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Guid>> Handle(GetJobsHavingCompanyQuery request, CancellationToken cancellationToken)
        {
            return await _context.Jobs
                .Where(x => x.Company.Id == request.CompanyId)
                .Select(x => x.Id)
                .ToListAsync(cancellationToken);
        }
    }
}
