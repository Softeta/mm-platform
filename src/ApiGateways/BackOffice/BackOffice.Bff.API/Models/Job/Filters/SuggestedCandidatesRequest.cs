namespace BackOffice.Bff.API.Models.Job.Filters
{
    public class SuggestedCandidatesRequest
    {
        public Guid? JobId { get; set; }
        public string? Position { get; set; }
        public string? Location { get; set; }
        public ICollection<string> Skills { get; set; } = new List<string>();
        public ICollection<string> Seniorities { get; set; } = new List<string>();
        public ICollection<string> WorkTypes { get; set; } = new List<string>();
        public ICollection<string> WorkingHourTypes { get; set; } = new List<string>();
        public ICollection<string> WorkingFormats { get; set; } = new List<string>();
        public ICollection<string> Industries { get; set; } = new List<string>();
        public ICollection<string> Languages { get; set; } = new List<string>();
    }
}
