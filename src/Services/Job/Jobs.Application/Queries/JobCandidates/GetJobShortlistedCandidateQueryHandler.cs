using Domain.Seedwork.Enums;
using Jobs.Domain.Aggregates.JobCandidatesAggregate.Entities;
using Jobs.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jobs.Application.Queries.JobsCandidates
{
    public class GetJobShortlistedCandidateQueryHandler : IRequestHandler<GetJobShortlistedCandidateQuery, JobSelectedCandidate?>
    {
        private readonly IJobContext _context;

        public GetJobShortlistedCandidateQueryHandler(IJobContext context)
        {
            _context = context;
        }

        public async Task<JobSelectedCandidate?> Handle(GetJobShortlistedCandidateQuery request, CancellationToken cancellationToken)
        {
            var query = await _context.JobCandidates
                .Where(x => x.Id == request.JobId)
                .SelectMany(x => x.SelectedCandidates
                    .Where(x => x.CandidateId == request.CandidateId)
                    .Where(x => x.Stage == SelectedCandidateStage.NoInterview ||
                                x.Stage == SelectedCandidateStage.FirstInterview ||
                                x.Stage == SelectedCandidateStage.SecondInterview ||
                                x.Stage == SelectedCandidateStage.ThirdInterview))
                .SingleOrDefaultAsync();

            return query;
        }
    }
}
