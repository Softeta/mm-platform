using System.ComponentModel.DataAnnotations;

namespace Contracts.Job.JobCandidates.Requests
{
    public class ActivateShortlistViaEmailRequest
    {
        [Required]
        public string Email { get; set; } = null!;
    }
}
