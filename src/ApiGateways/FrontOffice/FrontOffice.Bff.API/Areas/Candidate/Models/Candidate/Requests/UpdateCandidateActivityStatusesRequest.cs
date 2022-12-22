using Domain.Seedwork.Enums;
using System.ComponentModel.DataAnnotations;

namespace FrontOffice.Bff.API.Areas.Candidate.Models.Candidate.Requests
{
    public class UpdateCandidateActivityStatusesRequest
    {
        [Required]
        public ICollection<ActivityStatus> ActivityStatuses { get; set; } = null!;
    }
}
