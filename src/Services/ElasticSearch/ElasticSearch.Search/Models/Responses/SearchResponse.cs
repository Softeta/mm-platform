using System;

namespace ElasticSearch.Search.Models.Responses
{
    public class SearchResponse
    {
        public Guid Id { get; set; }
        public double? Score { get; set; }

        public static SearchResponse ToResponse(string id, double? score)
        {
            return new SearchResponse
            {
                Id = Guid.Parse(id),
                Score = score
            };
        }
    }
}
