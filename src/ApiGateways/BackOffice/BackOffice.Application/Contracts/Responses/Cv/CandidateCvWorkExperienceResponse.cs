using Contracts.Shared;

namespace BackOffice.Application.Contracts.Responses.Cv;

public class CandidateCvWorkExperienceResponse
{
    public string? CompanyName { get; set; }
    public Position? Position { get; set; }
    public DateTimeOffset? From { get; set; }
    public DateTimeOffset? To { get; set; }
    public string? JobDescription { get; set; }
    public bool IsCurrentJob { get; set; }
}