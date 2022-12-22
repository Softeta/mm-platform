using Jobs.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jobs.Application.Queries.JobsCandidates
{
    public class GetJobCandidatesByCandidateIdQueryHandler : IRequestHandler<GetJobCandidatesByCandidateIdQuery, ICollection<Guid>>
    {
        private readonly IJobContext _context;

        public GetJobCandidatesByCandidateIdQueryHandler(IJobContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Guid>> Handle(GetJobCandidatesByCandidateIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.JobCandidates
                .Where(x => x.SelectedCandidates.Any(s => s.CandidateId == request.CandidateId) || 
                            x.ArchivedCandidates.Any(a => a.CandidateId == request.CandidateId))
                .Select(x => x.Id)
                .ToListAsync();
        }
    }
}
