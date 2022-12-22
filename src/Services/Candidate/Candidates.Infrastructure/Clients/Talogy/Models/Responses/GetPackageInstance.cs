using Candidates.Infrastructure.Clients.Talogy.Models.Responses.PackageInstance;

namespace Candidates.Infrastructure.Clients.Talogy.Models.Responses
{
    public class GetPackageInstance
    {
        public string LogonUrl { get; set; } = null!;
        public string CompletionUrl { get; set; } = null!;
        public ICollection<AssessmentInstance> AssessmentInstances { get; set; } = new List<AssessmentInstance>();
        public ICollection<ReportInstance> ReportInstances { get; set; } = new List<ReportInstance>();
        public TotalScore? TotalScore { get; set; }
        public ReportPage? ReportsPage { get; set; }
        public string ParticipantId { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string EmailAddress { get; set; } = null!;
    }
}
