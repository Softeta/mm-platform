namespace BackOffice.Application.Contracts.Responses.Cv;

public class CandidateCvEducationResponse
{
    public string? SchoolName { get; set; }
    public string? Degree { get; set; }
    public string? FieldOfStudy { get; set; }
    public DateTimeOffset? From { get; set; }
    public DateTimeOffset? To { get; set; }
}