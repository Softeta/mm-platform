using Contracts.Shared;
using Domain.Seedwork.Enums;

namespace Jobs.Application.IntegrationEventHandlers.Publishers.Payloads.Models.JobCandidates
{
    public class Job
    {
        public Guid JobId { get; set; }
        public JobStage Stage { get; set; }
        public Company Company { get; set; } = null!;
        public Position Position { get; set; } = null!;
        public DateTimeOffset? ShortListActivatedAt { get; set; }
        public WorkType? Freelance { get; set; }
        public WorkType? Permanent { get; set; }
        public DateTimeOffset? DeadlineDate { get; set; }
        public DateTimeOffset? StartDate { get; set; }

        public static Job FromDomain(Domain.Aggregates.JobCandidatesAggregate.JobCandidates jobCandidates)
        {
            return new Job
            {
                JobId = jobCandidates.Id,
                Stage = jobCandidates.Stage,
                Company = Company.FromDomain(jobCandidates.Company),
                Position = Position.FromDomainNotNullable(jobCandidates.Position),
                ShortListActivatedAt = jobCandidates.ShortListActivatedAt,
                Freelance = jobCandidates.Freelance,
                Permanent = jobCandidates.Permanent,
                DeadlineDate = jobCandidates.DeadlineDate,
                StartDate = jobCandidates.StartDate
            };
        }
    }
}
