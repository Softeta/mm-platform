using Candidates.Domain.Aggregates.CandidateJobsAggregate.Entities;
using Domain.Seedwork.Enums;

namespace Candidates.Application.Queries.Extensions
{
    public static class QueryExtensions
    {
        public static IQueryable<CandidateAppliedInJob> OrderAppliedToJobsData(
            this IQueryable<CandidateAppliedInJob> query, 
            CandidateAppliedToJobOrderBy? orderBy)
        {
            if (!orderBy.HasValue) return query;

            switch (orderBy)
            {
                case CandidateAppliedToJobOrderBy.CreatedAtDesc:
                    return query.OrderByDescending(x => x.CreatedAt);
                default:
                    return query;
            }
        }
    }
}
