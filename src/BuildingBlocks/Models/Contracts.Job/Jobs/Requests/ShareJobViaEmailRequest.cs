using System.ComponentModel.DataAnnotations;

namespace Contracts.Job.Jobs.Requests
{
    public class ShareJobViaEmailRequest
    {
        [Required]
        public string ReceiverEmail { get; set; } = null!;
    }
}
