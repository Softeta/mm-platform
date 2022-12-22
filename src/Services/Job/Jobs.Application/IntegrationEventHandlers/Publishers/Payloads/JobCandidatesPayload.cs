using Domain.Seedwork.Enums;
using Jobs.Application.IntegrationEventHandlers.Publishers.Payloads.Models.JobCandidates;
using Jobs.Application.IntegrationEventHandlers.Publishers.Payloads.Models.Shared;
using Jobs.Domain.Aggregates.JobCandidatesAggregate;

namespace Jobs.Application.IntegrationEventHandlers.Publishers.Payloads
{
    public class JobCandidatesPayload
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

        public IEnumerable<SelectedCandidate> SelectedCandidates { get; set; } = new List<SelectedCandidate>();
        public IEnumerable<ArchivedCandidate> ArchivedCandidates { get; set; } = new List<ArchivedCandidate>();

        public static JobCandidatesPayload FromDomain(JobCandidates jobCandidates)
        {
            return new JobCandidatesPayload
            {
                JobId = jobCandidates.Id,
                Stage = jobCandidates.Stage,
                Company = Company.FromDomain(jobCandidates.Company),
                Position = Position.FromDomain(jobCandidates.Position),
                ShortListActivatedAt = jobCandidates.ShortListActivatedAt,
                Freelance = jobCandidates.Freelance,
                Permanent = jobCandidates.Permanent,
                StartDate = jobCandidates.StartDate,
                DeadlineDate = jobCandidates.DeadlineDate,
                SelectedCandidates = jobCandidates.SelectedCandidates.Select(SelectedCandidate.FromDomain),
                ArchivedCandidates = jobCandidates.ArchivedCandidates.Select(ArchivedCandidate.FromDomain)
            };
        }
    }
}
