namespace Candidates.API.Areas.Administration.Models.Requests
{
    public class CandidatesSyncRequest
    {
        public ICollection<Guid> CandidateIds { get; set; } = new List<Guid>();
    }
}
