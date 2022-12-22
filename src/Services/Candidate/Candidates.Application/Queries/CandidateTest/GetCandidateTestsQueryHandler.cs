using API.Customization.Authorization.Constants;
using Candidates.Domain.Aggregates.CandidateTestsAggregate;
using Candidates.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Candidates.Application.Queries.CandidateTest
{
    public class GetCandidateTestsQueryHandler : IRequestHandler<GetCandidateTestsQuery, CandidateTests?>
    {
        private readonly ICandidateContext _context;

        public GetCandidateTestsQueryHandler(ICandidateContext context)
        {
            _context = context;
        }

        public async Task<CandidateTests?> Handle(GetCandidateTestsQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<CandidateTests, bool>> filterByCandidate = candidateTest =>
                request.Scope != CustomScopes.FrontOffice.Candidate ||
                request.Scope == CustomScopes.FrontOffice.Candidate && candidateTest.ExternalId == request.UserId;

            return await _context.CandidateTests
                .Where(filterByCandidate)
                .FirstOrDefaultAsync(t => t.Id == request.CandidateId);
        }
    }
}
