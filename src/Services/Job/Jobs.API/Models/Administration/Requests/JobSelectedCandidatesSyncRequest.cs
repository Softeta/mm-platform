namespace Jobs.API.Models.Administration.Requests
{
    public class JobSelectedCandidatesSyncRequest
    {
        public ICollection<Guid> JobIds { get; set; } = new List<Guid>();
    }
}
