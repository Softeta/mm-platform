using Candidates.Infrastructure.Clients.Talogy.Models.Responses.PackageInstance;

namespace Candidates.Application.NotificationHandlers.Events
{
    public class PackageInstanceScored : AssessmentInstanceCompleted
    {
        public ICollection<ReportInstance> ReportInstances { get; set; } = new List<ReportInstance>();
    }
}
