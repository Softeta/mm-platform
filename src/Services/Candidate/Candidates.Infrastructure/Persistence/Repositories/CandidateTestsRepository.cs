using Candidates.Domain.Aggregates.CandidateTestsAggregate;
using Domain.Seedwork;
using Microsoft.EntityFrameworkCore;

namespace Candidates.Infrastructure.Persistence.Repositories
{
    public class CandidateTestsRepository : ICandidateTestsRepository
    {
        private readonly CandidateContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public CandidateTestsRepository(CandidateContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public CandidateTests Add(CandidateTests candidateTests)
        {
            return _context.CandidateTests.Add(candidateTests).Entity;
        }

        public async Task<CandidateTests?> GetAsync(Guid id)
        {
            var candidateTests = await _context.CandidateTests
                .SingleOrDefaultAsync(x => x.Id == id);

            return candidateTests;
        }

        public async Task<CandidateTests?> GetAsync(long candidateOldPlatformid)
        {
            var candidateTests = await _context.CandidateTests
                .SingleOrDefaultAsync(x => x.CandidateOldPlatformId == candidateOldPlatformid);

            return candidateTests;
        }

        public void Update(CandidateTests candidateTests)
        {
            _context.Entry(candidateTests).State = EntityState.Modified;
        }
    }
}
