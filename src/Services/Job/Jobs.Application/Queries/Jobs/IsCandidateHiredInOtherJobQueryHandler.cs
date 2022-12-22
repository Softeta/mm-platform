using Domain.Seedwork.Enums;
using Jobs.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jobs.Application.Queries.Jobs
{
    public class IsCandidateHiredInOtherJobQueryHandler
        : IRequestHandler<IsCandidateHiredInOtherJobQuery, bool>
    {
        private readonly IJobContext _context;

        public IsCandidateHiredInOtherJobQueryHandler(IJobContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(IsCandidateHiredInOtherJobQuery request, CancellationToken cancellationToken)
        {
            return await _context.JobSelectedCandidates
                .Where(x => x.JobId != request.JobId)
                .Where(x => x.CandidateId == request.CandidateId)
                .Where(x => x.Stage == SelectedCandidateStage.Hired)
                .AnyAsync(cancellationToken);
        }
    }
}
