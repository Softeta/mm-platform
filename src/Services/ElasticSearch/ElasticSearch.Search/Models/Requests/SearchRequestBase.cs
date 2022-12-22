using System.Collections.Generic;

namespace ElasticSearch.Search.Models.Requests
{
    public abstract class SearchRequestBase
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
    }
}
