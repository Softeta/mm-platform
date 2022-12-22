using Candidates.Domain.Aggregates.CandidateJobsAggregate;
using Candidates.Domain.Aggregates.CandidateJobsAggregate.Entities;
using Candidates.Infrastructure.Persistence.Repositories;
using Domain.Seedwork.Enums;
using EventBus.EventHandlers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Candidates.Application.IntegrationEventHandlers.Subscribers.Jobs
{
    public class JobSelectedCandidatesAddedIntegrationEventHandler :
        IntegrationEventHandler, 
        IIntegrationEventHandler<JobSelectedCandidatesAddedIntegrationEvent>
    {
        private readonly IServiceProvider _serviceProvider;

        public JobSelectedCandidatesAddedIntegrationEventHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public override async Task<bool> ExecuteAsync(string message)
        {
            var @event = JsonConvert.DeserializeObject<JobSelectedCandidatesAddedIntegrationEvent>(message);

            if (@event?.Payload is null) return false;

            var job = @event.Payload;
            var selectedCandidates = @event.Payload.SelectedCandidates;

            await using var scope = _serviceProvider.CreateAsyncScope();
            var candidateJobsRepository = scope.ServiceProvider.GetRequiredService<ICandidateJobsRepository>();
            var logger = scope.ServiceProvider
                .GetRequiredService<ILogger<JobSelectedCandidatesAddedIntegrationEventHandler>>();

            foreach (var selectedCandidate in selectedCandidates)
            {
                try
                {
                    if (selectedCandidate.Stage is not SelectedCandidateStage.New)
                    {
                        continue;
                    }

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

                        candidateJobs.SyncNewlyAddedSelectedJob(selectedJob);
                        candidateJobsRepository.Add(candidateJobs);
                    }
                    else
                    {
                        candidateJobs.SyncNewlyAddedSelectedJob(selectedJob);
                        candidateJobsRepository.Update(candidateJobs);
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(
                        ex, 
                        "Newly added job cannot be synced. JobId:{JobId} CandidateId:{CandidateId}",
                        job.JobId, 
                        selectedCandidate.CandidateId);
                }
            }

            return await candidateJobsRepository.UnitOfWork.SaveEntitiesAsync<CandidateJobs>();
        }
    }
}
