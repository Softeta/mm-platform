namespace BackOffice.Bff.API.Models.ElasticSearch.Responses
{
    public class CandidateElasticSearchResponse
    {
        public Guid Id { get; set; }
        public double? Score { get; set; }
    }
}
