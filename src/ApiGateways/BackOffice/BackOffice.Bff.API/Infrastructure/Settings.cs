namespace BackOffice.Bff.API.Infrastructure
{
    public class Services
    {
        public string JobsApiUrl { get; set; } = null!;
        public string CompaniesApiUrl { get; set; } = null!;
        public string CandidatesApiUrl { get; set; } = null!;
        public string ElasticSearchApiUrl { get; set; } = null!;
        public string AdministrationSettingsApiUrl { get; set; } = null!;
        public string TagSystemApiUrl { get; set; } = null!;
    }

    public class ProfileImageSettings
    {
        public int MaxSizeInKilobytes { get; set; }
    }

    public class ElasticSearchConfigurations
    {
        public string SuggestedCandidatesKey { get; set; } = null!;
    }
}
