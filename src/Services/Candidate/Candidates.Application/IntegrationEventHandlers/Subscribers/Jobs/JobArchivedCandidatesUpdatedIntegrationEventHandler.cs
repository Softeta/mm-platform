using Candidates.Domain.Aggregates.CandidateJobsAggregate;
using Candidates.Domain.Aggregates.CandidateJobsAggregate.Entities;
using Candidates.Infrastructure.Persistence.Repositories;
using EventBus.EventHandlers;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Candidates.Application.IntegrationEventHandlers.Subscribers.Jobs
{
    public class JobArchivedCandidatesUpdatedIntegrationEventHandler : 
        IntegrationEventHandler, 
        IIntegrationEventHandler<JobArchivedCandidatesUpdatedIntegrationEvent>
    {
        private readonly IServiceProvider _serviceProvider;

        public JobArchivedCandidatesUpdatedIntegrationEventHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override async Task<bool> ExecuteAsync(string message)
        {
            var @event = JsonConvert.DeserializeObject<JobArchivedCandidatesUpdatedIntegrationEvent>(message);

            if (@event?.Payload is null) return false;

            var job = @event.Payload;
            var archivedCandidates = @event.Payload.ArchivedCandidates;

            await using var scope = _serviceProvider.CreateAsyncScope();
            var candidateJobsRepository = scope.ServiceProvider.GetRequiredService<ICandidateJobsRepository>();

            foreach (var archivedCandidate in archivedCandidates)
            {
                var candidateJobs = await candidateJobsRepository.GetAsync(archivedCandidate.CandidateId);

                var archivedJob = new CandidateArchivedInJob(
                    job.JobId,
                    job.Stage,
                    job.Position.Id,
                    job.Position.Code,
                    job.Position.AliasTo?.Id,
                    job.Position.AliasTo?.Code,
                    archivedCandidate.CandidateId,
                    job.Company.Id, 
                    job.Company.Name,
                    job.Company.LogoUri,
                    job.Freelance,
                    job.Permanent,
                    job.StartDate,
                    job.DeadlineDate,
                    archivedCandidate.Stage,
                    archivedCandidate.InvitedAt,
                    archivedCandidate.HasApplied);

                if (candidateJobs is null)
                {
                    candidateJobs = new CandidateJobs(archivedCandidate.CandidateId);

                    candidateJobs.SyncCandidateArchivedJob(archivedJob);
                    candidateJobsRepository.Add(candidateJobs);
                }
                else
                {
                    candidateJobs.SyncCandidateArchivedJob(archivedJob);
                    candidateJobsRepository.Update(candidateJobs);
                }
            }

            return await candidateJobsRepository.UnitOfWork.SaveEntitiesAsync<CandidateJobs>();
        }
    }
}
