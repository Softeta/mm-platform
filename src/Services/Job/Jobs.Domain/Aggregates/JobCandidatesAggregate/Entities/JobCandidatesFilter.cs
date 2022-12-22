namespace Jobs.Domain.Aggregates.JobCandidatesAggregate.Entities;

public class JobCandidatesFilter
{
    public int Index { get; set; }

    public Guid JobId { get; set; }

    public Guid UserId { get; set; }

    public string Title { get; set; } = null!;

    public string FilterParams { get; set; } = null!;
}