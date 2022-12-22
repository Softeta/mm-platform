namespace Candidates.Infrastructure.Settings
{
    public class TalogySettings
    {
        public PackageTypes PackageTypes { get; set; } = null!;
        public ReportTypes ReportTypes { get; set; } = null!;
        public Dictionary<string, string> SupportedLocales { get; set; } = null!;
        public string DefaultLocale { get; set; } = null!;
    }

    public class PackageTypes
    {
        public string Logic { get; set; } = null!;
        public string Personality { get; set; } = null!;
    }

    public class ReportTypes
    {
        public ReportType PapiDynamicWheel { get; set; } = null!;
        public ReportType PapiGeneralFeedback { get; set; } = null!;
        public ReportType LgiGeneralFeedback { get; set; } = null!;
    }

    public class ReportType
    {
        public string Id { get; set; } = null!;
        public string FileFormat { get; set; } = null!;
    }
}
