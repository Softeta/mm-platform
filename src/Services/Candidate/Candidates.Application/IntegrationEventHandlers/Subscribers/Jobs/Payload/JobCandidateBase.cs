using Candidates.Application.IntegrationEventHandlers.Subscribers.Jobs.Payload.Models;
using Domain.Seedwork.Enums;

namespace Candidates.Application.IntegrationEventHandlers.Subscribers.Jobs.Payload
{
    public abstract class JobCandidateBase
    {
        public Guid JobId { get; set; }
        public JobStage Stage { get; set; }
        public Company Company { get; set; } = null!;
        public Position Position { get; set; } = null!;
        public WorkType? Freelance { get; set; }
        public WorkType? Permanent { get; set; }
        public DateTimeOffset? DeadlineDate { get; set; }
        public DateTimeOffset? StartDate { get; set; }
    }
}
