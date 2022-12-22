using API.Customization.Pagination;
using System.Collections.Generic;

namespace ElasticSearch.Search.Models.Filters
{
    internal class JobsSearchFilter : PagedFilter
    {
        public string? CandidateId { get; set; }
        public ICollection<string>? Stages { get; set; }
        public bool? IsPublished { get; set; }
    }
}
