namespace Jobs.API.Models.Administration.Requests
{
    public class JobsUpdatedSyncRequest
    {
        public ICollection<Guid> JobIds { get; set; } = new List<Guid>();
    }
}
