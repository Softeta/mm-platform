using API.Customization.Pagination;
using System;

namespace ElasticSearch.Search.Models.Filters
{
    internal class CandidatesSearchFilter : PagedFilter
    {
        public string? JobId { get; set; }
    }
}
