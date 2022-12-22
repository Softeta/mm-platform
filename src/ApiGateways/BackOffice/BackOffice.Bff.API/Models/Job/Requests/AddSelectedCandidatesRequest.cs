using System.Collections.ObjectModel;

namespace BackOffice.Bff.API.Models.Job.Requests
{
    public class AddSelectedCandidatesRequest
    {
        public Collection<Guid> Candidates { get; set; } = new();
    }
}
