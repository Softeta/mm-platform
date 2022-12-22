namespace Contracts.Job.JobCandidatesFilters.Responses;

public class GetCandidatesFilterResponse
{
    public int Index { get; set; }
    public string Title { get; set; } = null!;
    public string FilterParams { get; set; } = null!;
}