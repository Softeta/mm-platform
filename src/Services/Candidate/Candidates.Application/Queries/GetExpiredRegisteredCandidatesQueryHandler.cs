using Candidates.Application.Queries.Models;
using Candidates.Infrastructure.Persistence;
using Candidates.Infrastructure.Settings;
using Domain.Seedwork.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Candidates.Application.Queries
{
    public class GetExpiredRegisteredCandidatesQueryHandler : IRequestHandler<GetExpiredRegisteredCandidatesQuery, List<ExpiredRegisteredCandidate>>
    {
        private readonly int _registeredCandidatesExpiresInDays;
        private readonly ICandidateContext _context;

        public GetExpiredRegisteredCandidatesQueryHandler(IOptions<RegisteredCandidateSettings> options, ICandidateContext context)
        {
            _registeredCandidatesExpiresInDays = options.Value.ExpiresInDays;
            _context = context;
        }

        public async Task<List<ExpiredRegisteredCandidate>> Handle(GetExpiredRegisteredCandidatesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Candidates
                .Where(x => x.CreatedAt.AddDays(_registeredCandidatesExpiresInDays) < DateTimeOffset.UtcNow)
                .Where(x => x.Status == CandidateStatus.Registered)
                .Where(x => x.ExternalId != null)
                .Where(x => x.Email != null && !x.Email.IsVerified)
                .Select(x => new ExpiredRegisteredCandidate
                {
                    CandidateId = x.Id,
                    ExternalId = x.ExternalId!.Value
                })
                .ToListAsync();
        }
    }
}
