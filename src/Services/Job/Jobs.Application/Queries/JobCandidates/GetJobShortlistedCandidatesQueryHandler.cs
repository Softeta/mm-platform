using Contracts.Job.SelectedCandidates.Responses;
using Contracts.Shared;
using Contracts.Shared.Responses;
using Domain.Seedwork.Enums;
using Jobs.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jobs.Application.Queries.JobsCandidates
{
    public class GetJobShortlistedCandidatesQueryHandler : IRequestHandler<GetJobShortlistedCandidatesQuery, JobSelectedCandidatesResponse>
    {
        private readonly IJobContext _context;

        public GetJobShortlistedCandidatesQueryHandler(IJobContext context)
        {
            _context = context;
        }

        public async Task<JobSelectedCandidatesResponse> Handle(GetJobShortlistedCandidatesQuery request, CancellationToken cancellationToken)
        {
            var query = _context.JobCandidates
                .Where(x => x.Id == request.JobId)
                .SelectMany(x => x.SelectedCandidates)
                .Where(x => x.Stage == SelectedCandidateStage.NoInterview ||
                            x.Stage == SelectedCandidateStage.FirstInterview ||
                            x.Stage == SelectedCandidateStage.SecondInterview ||
                            x.Stage == SelectedCandidateStage.ThirdInterview);

            var count = await query.CountAsync(cancellationToken);

            var shortlistedCandidates = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .OrderBy(x => x.Ranking)
                .Select(x => new JobSelectedCandidateResponse
                {
                    Id = x.Id,
                    CandidateId = x.CandidateId,
                    Email = x.Email,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    PhoneNumber = x.PhoneNumber,
                    Stage = x.Stage,
                    Ranking = x.Ranking,
                    Position = x.Position != null 
                    ? new Position
                    {
                        Id = x.Position.Id,
                        Code = x.Position.Code,
                    } 
                    : null,
                    Picture = !string.IsNullOrWhiteSpace(x.PictureUri) 
                    ? new ImageResponse
                    {
                        Uri = x.PictureUri,
                    }
                    : null,
                    Brief = x.Brief
                })
                .ToListAsync(cancellationToken);

            return new JobSelectedCandidatesResponse(count, shortlistedCandidates);
        }
    }
}
