using API.Customization.Pagination;
using Domain.Seedwork.Enums;

namespace Candidates.API.Areas.CandidateJobs.Models.Filters
{
    public class CandidateSelectedJobsFilter : PagedFilter
    {
        public IEnumerable<SelectedCandidateStage>? SelectedCandidateStages { get; set; }
        public bool? IsInvited { get; set; }
    }
}
