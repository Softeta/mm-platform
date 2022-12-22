namespace Candidates.Infrastructure.Settings
{
    public class SelfServiceSettings
    {
        public string ApiUrl { get; set; } = null!;
        public SelfServiceWebsite Website { get; set; } = null!;
    }

    public class SelfServiceWebsite
    {
        public string Url { get; set; } = null!;
        public string TestsPath { get; set; } = null!;

        public string AbsolutePath(string path) => $"{Url}/{path}";
    }
}
