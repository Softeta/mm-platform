using Candidates.Application.IntegrationEventHandlers.Publishers.Payloads.Models.Shared;
using Domain.Seedwork.Enums;
using DomainEntities = Candidates.Domain.Aggregates.CandidateJobsAggregate.Entities;

namespace Candidates.Application.IntegrationEventHandlers.Publishers.Payloads.Models.CandidateJobs
{
    internal class CandidateSelectedInJob : CandidateInJob
    {
        public SelectedCandidateStage Stage { get; set; }
        public DateTimeOffset? InvitedAt { get; set; }

        public static CandidateSelectedInJob FromDomain(DomainEntities.CandidateSelectedInJob candidateSelectedInJob)
        {
            return new CandidateSelectedInJob
            {
                JobId = candidateSelectedInJob.JobId,
                Position = Position.FromDomain(candidateSelectedInJob.Position),
                CandidateId = candidateSelectedInJob.CandidateId,
                Company = Company.FromDomain(candidateSelectedInJob.Company),
                Stage = candidateSelectedInJob.Stage,
                InvitedAt = candidateSelectedInJob.InvitedAt,
            };
        }
    }
}
