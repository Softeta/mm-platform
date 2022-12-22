using Domain.Seedwork;
using Jobs.Domain.Aggregates.JobCandidatesAggregate.Entities;
using Microsoft.EntityFrameworkCore;

namespace Jobs.Infrastructure.Persistence.Repositories;

public class JobCandidatesFilterRepository : IJobCandidatesFilterRepository
{
    private readonly JobContext _context;

    public JobCandidatesFilterRepository(JobContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<JobCandidatesFilter> AddAsync(JobCandidatesFilter jobCandidatesFilter)
    {
        var entry = await _context.JobCandidatesFilters.AddAsync(jobCandidatesFilter);
        await _context.SaveChangesAsync();
        return entry.Entity;
    }

    public async Task<IEnumerable<JobCandidatesFilter>> GetAllAsync(Guid userId, Guid jobId)
    {
        return await _context.JobCandidatesFilters
            .Where(x => x.JobId == jobId && x.UserId == userId)
            .OrderBy(x => x.Index)
            .ToListAsync();
    }

    public async Task DeleteAsync(Guid jobId, Guid userId, int index)
    {
        var entity = await _context.JobCandidatesFilters.FirstOrDefaultAsync(
            e => e.Index == index &&
                 e.JobId == jobId &&
                 e.UserId == userId);

        if (entity != null)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<JobCandidatesFilter?> UpdateTitleAsync(Guid jobId, Guid userId, int index, string title)
    {
        var entity = await _context.JobCandidatesFilters.FirstOrDefaultAsync(
            e => e.Index == index &&
                 e.JobId == jobId &&
                 e.UserId == userId);

        if (entity == null) return entity;

        entity.Title = title;
        await _context.SaveChangesAsync();

        return entity;
    }
}