using Candidates.Application.IntegrationEventHandlers.Publishers.Payloads.Models.Shared;
using Domain.Seedwork.Enums;
using DomainEntities = Candidates.Domain.Aggregates.CandidateJobsAggregate.Entities;

namespace Candidates.Application.IntegrationEventHandlers.Publishers.Payloads.Models.CandidateJobs
{
    internal class CandidateArchivedInJob : CandidateInJob
    {
        public ArchivedCandidateStage Stage { get; set; }

        public static CandidateArchivedInJob FromDomain(DomainEntities.CandidateArchivedInJob candidateArchivedInJob)
        {
            return new CandidateArchivedInJob
            {
                JobId = candidateArchivedInJob.JobId,
                Position = Position.FromDomain(candidateArchivedInJob.Position),
                CandidateId = candidateArchivedInJob.CandidateId,
                Company = Company.FromDomain(candidateArchivedInJob.Company),
                Stage = candidateArchivedInJob.Stage
            };
        }
    }
}
