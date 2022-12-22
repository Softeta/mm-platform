using Candidates.Domain.Aggregates.CandidateAggregate;
using Domain.Seedwork;
using Domain.Seedwork.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Candidates.Infrastructure.Persistence.Repositories
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly CandidateContext _context = null!;

        public IUnitOfWork UnitOfWork => _context;

        public CandidateRepository(CandidateContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Candidate Add(Candidate candidate)
        {
            return _context.Candidates.Add(candidate).Entity;
        }

        public async Task<Candidate> GetAsync(Guid id)
        {
            var candidate = await _context.Candidates
                .Include(c => c.ActivityStatuses)
                .Include(c => c.Industries)
                .Include(c => c.Skills)
                .Include(c => c.DesiredSkills)
                .Include(c => c.Languages)
                .Include(c => c.Hobbies)
                .Include(c => c.Courses)
                .Include(c => c.Educations)
                .Include(c => c.WorkExperiences)
                    .ThenInclude(c => c.Skills)
                .AsSplitQuery()
                .Where(j => j.Id == id)
                .SingleAsync();

            if (candidate == null)
            {
                throw new NotFoundException("Candidate was not found", ErrorCodes.NotFound.CandidateNotFound);
            }
            return candidate;
        }

        public async Task<Candidate?> GetByExternalidAsync(Guid externalId)
        {
            var candidate = await _context.Candidates
                .Include(c => c.ActivityStatuses)
                .Include(c => c.Industries)
                .Include(c => c.Skills)
                .Include(c => c.DesiredSkills)
                .Include(c => c.Languages)
                .Include(c => c.Hobbies)
                .Include(c => c.Courses)
                .Include(c => c.Educations)
                .Include(c => c.WorkExperiences)
                    .ThenInclude(c => c.Skills)
                .AsSplitQuery()
                .Where(x =>
                    x.ExternalId != null &&
                    x.ExternalId == externalId)
                .SingleOrDefaultAsync();

            return candidate;
        }

        public async Task<Candidate?> GetByEmailAsync(string email)
        {
            var candidate = await _context.Candidates
                .Include(c => c.ActivityStatuses)
                .Include(c => c.Industries)
                .Include(c => c.Skills)
                .Include(c => c.DesiredSkills)
                .Include(c => c.Languages)
                .Include(c => c.Hobbies)
                .Include(c => c.Courses)
                .Include(c => c.Educations)
                .Include(c => c.WorkExperiences)
                    .ThenInclude(c => c.Skills)
                .AsSplitQuery()
                .Where(x =>
                    x.Email != null &&
                    x.Email.Address == email)
                .SingleOrDefaultAsync();

            return candidate;
        }

        public void Update(Candidate candidate)
        {
            _context.Entry(candidate).State = EntityState.Modified;
        }

        public async Task RemoveAsync(Guid id)
        {
            var candidateToRemove = await _context.Candidates
                .Where(x => x.Id == id)
                .SingleAsync();

            _context.Remove(candidateToRemove);
        }
    }
}
