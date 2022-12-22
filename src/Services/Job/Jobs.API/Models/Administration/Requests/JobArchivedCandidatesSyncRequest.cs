namespace Jobs.API.Models.Administration.Requests
{
    public class JobArchivedCandidatesSyncRequest
    {
        public ICollection<Guid> JobIds { get; set; } = new List<Guid>();
    }
}
