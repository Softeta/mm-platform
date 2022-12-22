using System;

namespace EmailService.Send.Events.JobShare.Models
{
    internal class AskedForJobApproval
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = null!;
    }
}
