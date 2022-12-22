using System;

namespace EmailService.Send.Events.Candidates.Models
{
    internal class Email
    {
        public string Address { get; set; } = null!;
        public Guid? VerificationKey { get; set; }
    }
}
