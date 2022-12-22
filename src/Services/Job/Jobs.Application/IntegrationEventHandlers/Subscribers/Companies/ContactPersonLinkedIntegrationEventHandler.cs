using EventBus.EventHandlers;
using Jobs.Application.Queries.Jobs;
using Jobs.Domain.Aggregates.JobAggregate;
using Jobs.Infrastructure.Persistence.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Jobs.Application.IntegrationEventHandlers.Subscribers.Companies
{
    public class ContactPersonLinkedIntegrationEventHandler : IntegrationEventHandler, IIntegrationEventHandler<ContactPersonLinkedIntegrationEvent>
    {
        private readonly IServiceProvider _serviceProvider;

        public ContactPersonLinkedIntegrationEventHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async override Task<bool> ExecuteAsync(string message)
        {
            var @event = JsonConvert.DeserializeObject<ContactPersonLinkedIntegrationEvent>(message);

            if (@event?.Payload is null)
            {
                return false;
            }

            var contactPerson = @event.Payload;

            await using var scope = _serviceProvider.CreateAsyncScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            var jobRepository = scope.ServiceProvider.GetRequiredService<IJobRepository>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<ContactPersonLinkedIntegrationEventHandler>>();

            var jobIds = await mediator.Send(new GetJobsByContactPersonIdQuery(contactPerson.Id));

            foreach (var jobId in jobIds)
            {
                try
                {
                    var job = await jobRepository.GetAsync(jobId);
                    job.SyncContactPersonExternalId(contactPerson.Id, contactPerson.ExternalId);

                    jobRepository.Update(job);
                    await jobRepository.UnitOfWork.SaveEntitiesAsync<Job>();
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Failed to sync ExternalId for job: {jobId}. Payload: {message}", jobId, message);
                }
            }

            return true;
        }
    }
}