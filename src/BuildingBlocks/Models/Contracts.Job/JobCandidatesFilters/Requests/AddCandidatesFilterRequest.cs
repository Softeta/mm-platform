namespace Contracts.Job.JobCandidatesFilters.Requests;

public class AddCandidatesFilterRequest
{
    public int Index { get; set; }
    public string Title { get; set; } = null!;
    public string FilterParams { get; set; } = null!;
}