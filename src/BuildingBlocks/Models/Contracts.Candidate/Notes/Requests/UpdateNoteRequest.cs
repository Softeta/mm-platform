namespace Contracts.Candidate.Notes.Requests
{
    public class UpdateNoteRequest
    {
        public string? Value { get; set; }
        public DateTimeOffset? EndDate { get; set; }
    }
}
