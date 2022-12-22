using BackOffice.Bff.API.Models.Job.Filters;

namespace BackOffice.Bff.API.Models.ElasticSearch.Requests
{
    public class JobRequest
    {
        public string? Position { get; set; }
        public string? Location { get; set; }
        public ICollection<string> Skills { get; set; } = new List<string>();
        public ICollection<string> Seniorities { get; set; } = new List<string>();
        public ICollection<string> WorkTypes { get; set; } = new List<string>();
        public ICollection<string> WorkingHourTypes { get; set; } = new List<string>();
        public ICollection<string> WorkingFormats { get; set; } = new List<string>();
        public ICollection<string> Industries { get; set; } = new List<string>();
        public ICollection<string> Languages { get; set; } = new List<string>();
        
        public static JobRequest FromSuggestedCandidatesRequest(SuggestedCandidatesRequest request)
        {
            return new JobRequest
            {
                Position = request.Position,
                Skills = request.Skills,
                Seniorities = request.Seniorities,
                WorkTypes = request.WorkTypes,
                WorkingHourTypes = request.WorkingHourTypes,
                WorkingFormats = request.WorkingFormats,
                Industries = request.Industries,
                Languages = request.Languages,
                Location = request.Location
            };
        }
    }
}
