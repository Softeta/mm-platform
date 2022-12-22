namespace FrontOffice.Bff.API.Infrastructure
{
    public class Services
    {
        public string JobsApiUrl { get; set; } = null!;
        public string CompaniesApiUrl { get; set; } = null!;
        public string CandidatesApiUrl { get; set; } = null!;
        public string ElasticSearchApiUrl { get; set; } = null!;
    }

    public class ElasticSearchConfigurations
    {
        public string RecommendedJobsKey { get; set; } = null!;
    }
}
