using Candidates.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Candidates.Application.Queries
{
    public class GetAllCandidateIdsQueryHandler : IRequestHandler<GetAllCandidateIdsQuery, List<Guid>>
    {
        private readonly ICandidateContext _context;
        public GetAllCandidateIdsQueryHandler(ICandidateContext context)
        {
            _context = context;
        }

        public async Task<List<Guid>> Handle(GetAllCandidateIdsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Candidates
                .Select(x => x.Id)
                .ToListAsync();
        }
    }
}
