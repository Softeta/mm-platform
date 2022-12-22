using Candidates.Application.Contracts;
using Candidates.Domain.Aggregates.CandidateJobsAggregate.Entities;
using Candidates.Infrastructure.Persistence;
using Contracts.Candidate.CandidateJobs.Responses;
using Contracts.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Candidates.Application.Queries
{
    public class GetCandidateSelectedInJobsQueryHandler : IRequestHandler<GetCandidateSelectedInJobsQuery, GetCandidateSelectedInJobsResponse>
    {
        private readonly ICandidateContext _context;

        public GetCandidateSelectedInJobsQueryHandler(ICandidateContext context)
        {
            _context = context;
        }

        public async Task<GetCandidateSelectedInJobsResponse> Handle(GetCandidateSelectedInJobsQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<CandidateSelectedInJob, bool>> filterBySelectedCandidateStage = candidate =>
                request.SelectedCandidateStages == null || request.SelectedCandidateStages.Contains(candidate.Stage);

            Expression<Func<CandidateSelectedInJob, bool>> filterByIsInvited = candidate =>
                request.IsInvited == null || request.IsInvited == candidate.InvitedAt.HasValue;

            var query = _context.CandidateSelectedInJobs
                .Where(x => x.CandidateId == request.CandidateId)
                .Where(filterBySelectedCandidateStage)
                .Where(filterByIsInvited)
                .OrderByDescending(c => c.CreatedAt);

            var count = await query.CountAsync(cancellationToken);
            var candidateSelectedInJobs = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new GetCandidateSelectedInJobBriefResponse
                {
                    Id = x.Id,
                    JobId = x.JobId,
                    Company = new CompanyResponse
                    {
                        Name = x.Company.Name,
                        LogoUri = x.Company.LogoUri
                    },
                    Position = new Position
                    {
                        Id = x.Position.Id,
                        Code = x.Position.Code
                    },
                    CoverLetter = x.CoverLetter,
                    HasMotivationVideo = x.MotivationVideo != null && x.MotivationVideo.Uri != null,
                    HasApplied = x.HasApplied,
                    JobStage = x.JobStage,
                    Freelance = x.Freelance,
                    Permanent = x.Permanent,
                    StartDate = x.StartDate,
                    DeadlineDate = x.DeadlineDate,
                    Stage = x.Stage
                }).ToListAsync(cancellationToken);

            return new GetCandidateSelectedInJobsResponse(count, candidateSelectedInJobs);
        }
    }
}
