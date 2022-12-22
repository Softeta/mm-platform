using API.Customization.Pagination;
using Domain.Seedwork.Enums;

namespace Candidates.API.Areas.CandidateJobs.Models.Filters
{
    public class GetAppliedJobsFilter : PagedFilter
    {
        public CandidateAppliedToJobOrderBy? OrderBy { get; set; }
    }
}
