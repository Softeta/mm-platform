using EmailService.Send.Events.JobShare.Models;
using System;

namespace EmailService.Send.Events.JobShare
{
    internal class JobShareChangedPayload
    {
        public Guid Id { get; set; }
        public string Key { get; set; } = null!;
        public AskedForJobApproval AskedForJobApproval { get; set; } = null!;
    }
}
