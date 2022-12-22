using Domain.Seedwork.Enums;

namespace Contracts.Job.Jobs.Requests;

public class ArchiveJobRequest
{
    public JobStage Stage { get; set; }
}