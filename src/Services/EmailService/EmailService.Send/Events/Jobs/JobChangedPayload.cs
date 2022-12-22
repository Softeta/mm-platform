using EmailService.Send.Events.Jobs.Models;
using System;

namespace EmailService.Send.Events.Jobs
{
    internal class JobChangedPayload
    {
        public Guid JobId { get; set; }
        public Position? Position { get; set; }
        public Company? Company { get; set; }
    }
}
