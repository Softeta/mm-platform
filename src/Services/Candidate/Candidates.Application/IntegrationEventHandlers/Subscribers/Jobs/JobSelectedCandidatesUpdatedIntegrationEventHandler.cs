using Candidates.Domain.Aggregates.CandidateJobsAggregate;
using Candidates.Domain.Aggregates.CandidateJobsAggregate.Entities;
using Candidates.Infrastructure.Persistence.Repositories;
using EventBus.EventHandlers;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Candidates.Application.IntegrationEventHandlers.Subscribers.Jobs
{
    public class JobSelectedCandidatesUpdatedIntegrationEventHandler :
        IntegrationEventHandler, 
        IIntegrationEventHandler<JobSelectedCandidatesUpdatedIntegrationEvent>
    {
        private readonly IServiceProvider _serviceProvider;

        public JobSelectedCandidatesUpdatedIntegrationEventHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override async Task<bool> ExecuteAsync(string message)
        {
            var @event = JsonConvert.DeserializeObject<JobSelectedCandidatesUpdatedIntegrationEvent>(message);

            if (@event?.Payload is null) return false;

            var job = @event.Payload;
            var selectedCandidates = @event.Payload.SelectedCandidates;

            await using var scope = _serviceProvider.CreateAsyncScope();
            var candidateJobsRepository = scope.ServiceProvider.GetRequiredService<ICandidateJobsRepository>();

            foreach (var selectedCandidate in selectedCandidates)
            {
                var candidateJobs = await candidateJobsRepository.GetAsync(selectedCandidate.CandidateId);

                var selectedJob = new CandidateSelectedInJob(
                    job.JobId,
                    job.Stage,
                    job.Position.Id,
                    job.Position.Code,
                    job.Position.AliasTo?.Id,
                    job.Position.AliasTo?.Code,
                    selectedCandidate.CandidateId,
                    job.Company.Id, 
                    job.Company.Name,
                    job.Company.LogoUri,
                    job.Freelance,
                    job.Permanent,
                    job.StartDate,
                    job.DeadlineDate,
                    selectedCandidate.Stage,
                    selectedCandidate.InvitedAt,
                    selectedCandidate.HasApplied);

                if (candidateJobs is null)
                {
                    candidateJobs = new CandidateJobs(selectedCandidate.CandidateId);

                    candidateJobs.SyncCandidateSelectedJob(selectedJob);
                    candidateJobsRepository.Add(candidateJobs);
                }
                else
                {
                    candidateJobs.SyncCandidateSelectedJob(selectedJob);
                    candidateJobsRepository.Update(candidateJobs);
                }
            }

            return await candidateJobsRepository.UnitOfWork.SaveEntitiesAsync<CandidateJobs>();
        }
    }
}
