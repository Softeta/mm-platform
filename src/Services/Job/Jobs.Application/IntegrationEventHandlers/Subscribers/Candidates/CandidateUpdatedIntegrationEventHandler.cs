using Domain.Seedwork.Enums;
using EventBus.EventHandlers;
using Jobs.Application.Queries.JobsCandidates;
using Jobs.Domain.Aggregates.JobCandidatesAggregate;
using Jobs.Infrastructure.Persistence.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Jobs.Application.IntegrationEventHandlers.Subscribers.Candidates
{
    public class CandidateUpdatedIntegrationEventHandler : IntegrationEventHandler, IIntegrationEventHandler<CandidateUpdatedIntegrationEvent>
    {
        private readonly IServiceProvider _serviceProvider;

        public CandidateUpdatedIntegrationEventHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async override Task<bool> ExecuteAsync(string message)
        {
            var @event = JsonConvert.DeserializeObject<CandidateUpdatedIntegrationEvent>(message);

            if (@event?.Payload is null)
            {
                return false;
            }

            var candidate = @event.Payload;

            if (candidate.Status != CandidateStatus.Approved) return true;
            if (string.IsNullOrWhiteSpace(candidate.FirstName)) return true;
            if (string.IsNullOrWhiteSpace(candidate.LastName)) return true;
            if (string.IsNullOrWhiteSpace(candidate.Email?.Address)) return true;

            await using var scope = _serviceProvider.CreateAsyncScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            var jobCandidatesRepository = scope.ServiceProvider.GetRequiredService<IJobCandidatesRepository>();

            var jobIds = await mediator.Send(new GetJobCandidatesByCandidateIdQuery(candidate.Id));

            foreach (var jobId in jobIds)
            {
                var jobCandidates = await jobCandidatesRepository.GetAsync(jobId);

                jobCandidates.SyncCandidateProfile(
                    candidate.Id,
                    candidate.FirstName,
                    candidate.LastName,
                    candidate.Email.Address,
                    candidate.Phone?.PhoneNumber,
                    candidate.PictureUri,
                    candidate.CurrentPosition?.Id,
                    candidate.CurrentPosition?.Code,
                    candidate.CurrentPosition?.AliasTo?.Id,
                    candidate.CurrentPosition?.AliasTo?.Code,
                    candidate.SystemLanguage);

                jobCandidatesRepository.Update(jobCandidates);
            }

            return await jobCandidatesRepository.UnitOfWork.SaveEntitiesAsync<JobCandidates>();
        }
    }
}
