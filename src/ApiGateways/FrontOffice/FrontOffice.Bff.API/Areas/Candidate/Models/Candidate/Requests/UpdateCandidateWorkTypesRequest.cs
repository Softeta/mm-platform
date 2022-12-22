using Domain.Seedwork.Enums;

namespace FrontOffice.Bff.API.Areas.Candidate.Models.Candidate.Requests
{
    public class UpdateCandidateWorkTypesRequest
    {
        public IEnumerable<WorkType> WorkTypes { get; set; } = new List<WorkType>();
    }
}
