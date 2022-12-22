using Candidates.Application.Queries.Extensions;
using Candidates.Infrastructure.Persistence;
using Contracts.Candidate.CandidateJobs.Responses;
using Contracts.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Candidates.Application.Queries
{
    public class GetAppliedJobsQueryHandler : IRequestHandler<GetAppliedJobsQuery, GetCandidateAppliedToJobsResponse>
    {
        private readonly ICandidateContext _context;

        public GetAppliedJobsQueryHandler(ICandidateContext context)
        {
            _context = context;
        }

        public async Task<GetCandidateAppliedToJobsResponse> Handle(GetAppliedJobsQuery request, CancellationToken cancellationToken)
        {
            var query = _context.CandidateAppliedInJobs
                .AsNoTracking()
                .Where(x => x.CandidateId == request.CandidateId);

            query = query.OrderAppliedToJobsData(request.OrderBy);

            var count = await query.CountAsync(cancellationToken);

            var candidateAppliedToJobs = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new GetCandidateAppliedToJobResponse
                {
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
                    JobStage = x.JobStage,
                    Freelance = x.Freelance,
                    Permanent = x.Permanent,
                    StartDate = x.StartDate,
                    DeadlineDate = x.DeadlineDate
                }).ToListAsync(cancellationToken);

            return new GetCandidateAppliedToJobsResponse(count, candidateAppliedToJobs);
        }
    }
}
