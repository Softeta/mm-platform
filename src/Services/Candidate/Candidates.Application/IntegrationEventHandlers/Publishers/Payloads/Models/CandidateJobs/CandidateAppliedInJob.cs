using DomainEntities = Candidates.Domain.Aggregates.CandidateJobsAggregate.Entities;
using Candidates.Application.IntegrationEventHandlers.Publishers.Payloads.Models.Shared;

namespace Candidates.Application.IntegrationEventHandlers.Publishers.Payloads.Models.CandidateJobs
{
    internal class CandidateAppliedInJob : CandidateInJob
    {
        public static CandidateAppliedInJob FromDomain(DomainEntities.CandidateAppliedInJob candidateAppliedInJob)
        {
            return new CandidateAppliedInJob
            {
                JobId = candidateAppliedInJob.JobId,
                Position = Position.FromDomain(candidateAppliedInJob.Position),
                CandidateId = candidateAppliedInJob.CandidateId,
                Company = Company.FromDomain(candidateAppliedInJob.Company)
            };
        }
    }
}
