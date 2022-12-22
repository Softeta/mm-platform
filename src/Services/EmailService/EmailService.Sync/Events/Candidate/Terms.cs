using EmailService.Sync.Events.Models;
using System;

namespace EmailService.Sync.Events.Candidate;

public class Terms
{
    public DateTimeOffset? StartDate { get; set; }
    public DateTimeOffset? EndDate { get; set; }
    public Formats? Formats { get; set; }
    public FreelanceContract? Freelance { get; set; }
    public PermanentContract? Permanent { get; set; }
}