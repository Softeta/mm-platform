using Domain.Seedwork;
using Domain.Seedwork.Exceptions;
using Jobs.Domain.Aggregates.JobAggregate;
using Microsoft.EntityFrameworkCore;

namespace Jobs.Infrastructure.Persistence.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly JobContext _context = null!;

        public IUnitOfWork UnitOfWork => _context;

        public JobRepository(JobContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Job Add(Job job)
        {
            return _context.Jobs.Add(job).Entity;
        }

        public async Task<Job> GetAsync(Guid id)
        {
            var job = await _context.Jobs
                .Include(j => j.Company.ContactPersons)
                .Include(j => j.AssignedEmployees)
                .Include(j => j.Languages)
                .Include(j => j.SeniorityLevels)
                .Include(j => j.Skills)
                .Include(j => j.Industries)
                .Include(j => j.InterestedCandidates)
                .Include(j => j.InterestedLinkedIns)
                .AsSplitQuery()
                .Where(j => j.Id == id)
                .SingleOrDefaultAsync();

            if (job == null)
            {
                throw new NotFoundException("Job not found", ErrorCodes.NotFound.JobNotFound);
            }

            return job;
        }

        public void Update(Job job)
        {
            _context.Entry(job).State = EntityState.Modified;
        }

        public async Task RemoveAsync(Guid id)
        {
            var jobToRemove = await _context.Jobs
                .Where(x => x.Id == id)
                .SingleAsync();

            _context.Remove(jobToRemove);
        }
    }
}
