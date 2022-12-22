using Domain.Seedwork.Enums;

namespace FrontOffice.Bff.API.Areas.Candidate.Models.Candidate.Requests
{
    public class UpdateCandidateWorkTermsRequest
    {
        public List<WorkingHoursType>? WorkingHoursTypes { get; set; }

        public int? WeeklyWorkHours { get; set; }

        public DateTimeOffset? StartDate { get; set; }

        public List<FormatType>? Formats { get; set; }

    }
}
