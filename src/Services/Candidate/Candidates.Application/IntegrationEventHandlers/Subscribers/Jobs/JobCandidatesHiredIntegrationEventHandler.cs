using Candidates.Application.IntegrationEventHandlers.Subscribers.Jobs.Payload;
using Candidates.Domain.Aggregates.CandidateJobsAggregate;
using Candidates.Domain.Aggregates.CandidateJobsAggregate.Entities;
using Candidates.Infrastructure.Persistence.Repositories;
using EventBus.EventHandlers;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Candidates.Application.IntegrationEventHandlers.Subscribers.Jobs
{
    internal class JobCandidatesHiredIntegrationEventHandler :
        IntegrationEventHandler,
        IIntegrationEventHandler<JobCandidatesHiredIntegrationEvent>
    {
        private readonly IServiceProvider _serviceProvider;

        public JobCandidatesHiredIntegrationEventHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override async Task<bool> ExecuteAsync(string message)
        {
            var @event = JsonConvert.DeserializeObject<JobCandidatesHiredIntegrationEvent>(message);

            if (@event?.Payload is null) return false;

            var job = @event.Payload;

            await using var scope = _serviceProvider.CreateAsyncScope();
            var candidateJobsRepository = scope.ServiceProvider.GetRequiredService<ICandidateJobsRepository>();

            await SyncSelectedCandidates(candidateJobsRepository, job);
            await SyncArchivedCandidates(candidateJobsRepository, job);

            return await candidateJobsRepository.UnitOfWork.SaveEntitiesAsync<CandidateJobs>();
        }

        private static async Task SyncSelectedCandidates(
            ICandidateJobsRepository candidateJobsRepository, 
            JobCandidatesInformation job)
        {
            foreach (var selectedCandidate in job.SelectedCandidates)
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

                    candidateJobs.SyncSelectedCandidateHiredJob(selectedJob);
                    candidateJobsRepository.Add(candidateJobs);
                }
                else
                {
                    candidateJobs.SyncSelectedCandidateHiredJob(selectedJob);
                    candidateJobsRepository.Update(candidateJobs);
                }
            }
        }

        private static async Task SyncArchivedCandidates(
            ICandidateJobsRepository candidateJobsRepository,
            JobCandidatesInformation job)
        {
            foreach (var archivedCandidate in job.ArchivedCandidates)
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

                    candidateJobs.SyncArchivedCandidateHiredJob(archivedJob);
                    candidateJobsRepository.Add(candidateJobs);
                }
                else
                {
                    candidateJobs.SyncArchivedCandidateHiredJob(archivedJob);
                    candidateJobsRepository.Update(candidateJobs);
                }
            }
        }
    }
}
