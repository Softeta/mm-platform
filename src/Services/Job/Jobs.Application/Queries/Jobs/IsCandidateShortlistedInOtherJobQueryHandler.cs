using Domain.Seedwork.Enums;
using Jobs.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jobs.Application.Queries.Jobs
{
    public class IsCandidateShortlistedInOtherJobQueryHandler
        : IRequestHandler<IsCandidateShortlistedInOtherJobQuery, bool>
    {
        private readonly IJobContext _context;

        public IsCandidateShortlistedInOtherJobQueryHandler(IJobContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(IsCandidateShortlistedInOtherJobQuery request, CancellationToken cancellationToken)
        {
            var shortListedStages = GetShortListedStages();
            var archivedJobStages = GetArchivedJobStages();

            return await _context.JobCandidates
                .Where(x => x.Id != request.JobId)
                .Where(x => x.SelectedCandidates.Any(c => 
                        c.CandidateId == request.CandidateId && 
                        shortListedStages.Contains(c.Stage)))
                .Where(x => !archivedJobStages.Contains(x.Stage))
                .AnyAsync(cancellationToken);
        }

        private static List<SelectedCandidateStage> GetShortListedStages() =>
            new()
            {
                SelectedCandidateStage.NoInterview,
                SelectedCandidateStage.FirstInterview,
                SelectedCandidateStage.SecondInterview,
                SelectedCandidateStage.ThirdInterview
            };

        private static List<JobStage> GetArchivedJobStages() =>
            new()
            {
                JobStage.OnHold,
                JobStage.Lost,
                JobStage.Successful
            };
    }
}
