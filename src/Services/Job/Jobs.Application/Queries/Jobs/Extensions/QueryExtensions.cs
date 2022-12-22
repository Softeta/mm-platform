using Domain.Seedwork.Enums;
using Jobs.Domain.Aggregates.JobAggregate;

namespace Jobs.Application.Queries.Jobs.Extensions
{
    public static class QueryExtensions
    {
        public static IQueryable<Job> OrderJobsData(this IQueryable<Job> query, JobOrderBy? orderBy)
        {
            if (!orderBy.HasValue) return query;

            switch (orderBy)
            {
                case JobOrderBy.IsPriorityDesc:
                    return query.OrderByDescending(x => x.IsPriority);
                case JobOrderBy.CreatedAtDesc:
                    return query.OrderByDescending(x => x.CreatedAt);
                default:
                    return query;
            }
        }
    }
}
