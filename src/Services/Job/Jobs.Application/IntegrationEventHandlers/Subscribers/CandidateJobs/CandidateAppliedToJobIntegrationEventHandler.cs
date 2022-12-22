using Domain.Seedwork.Shared.ValueObjects;
using EventBus.EventHandlers;
using Jobs.Domain.Aggregates.JobCandidatesAggregate;
using Jobs.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Jobs.Application.IntegrationEventHandlers.Subscribers.CandidateJobs
{
    public class CandidateAppliedToJobIntegrationEventHandler :
        IntegrationEventHandler,
        IIntegrationEventHandler<CandidateAppliedToJobIntegrationEvent>
    {
        private readonly IServiceProvider _serviceProvider;

        public CandidateAppliedToJobIntegrationEventHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override async Task<bool> ExecuteAsync(string message)
        {
            var @event = JsonConvert.DeserializeObject<CandidateAppliedToJobIntegrationEvent>(message);

            var payload = @event?.Payload;

            if (payload is null || payload.Candidate is null)
            {
                return false;
            }

            await using var scope = _serviceProvider.CreateAsyncScope();
            var jobCandidatesRepository = scope.ServiceProvider.GetRequiredService<IJobCandidatesRepository>();
            
            var jobCandidates = await jobCandidatesRepository.GetAsync(payload.JobId);

            jobCandidates.SyncApplyToJob(
                payload.CandidateId,
                payload.Candidate.FirstName,
                payload.Candidate.LastName,
                payload.Candidate.Email,
                payload.Candidate.PhoneNumber,
                payload.Candidate.PictureUri,
                Position.Create(
                    payload.Candidate.Position?.Id,
                    payload.Candidate.Position?.Code,
                    payload.Candidate.Position?.AliasTo?.Id,
                    payload.Candidate.Position?.AliasTo?.Code),
                payload.Candidate.SystemLanguage);

            jobCandidatesRepository.Update(jobCandidates);
            return await jobCandidatesRepository.UnitOfWork.SaveEntitiesAsync<JobCandidates>();
        }
    }
}
