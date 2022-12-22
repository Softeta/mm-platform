namespace FrontOffice.Bff.API.Areas.Jobs.Models.ElasticSearch.Responses
{
    public class JobElasticSearchResponse
    {
        public Guid Id { get; set; }
        public double? Score { get; set; }
    }
}
