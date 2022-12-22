namespace Candidates.Infrastructure.Clients.Talogy.Models.Responses.PackageInstance
{
    public class ReportPage
    {
        public string? Url { get; set; }
        public string? AccessType { get; set; }
        public string? AccessCode { get; set; }
        public DateTimeOffset? AccessExpiresAt { get; set; }
    }
}
