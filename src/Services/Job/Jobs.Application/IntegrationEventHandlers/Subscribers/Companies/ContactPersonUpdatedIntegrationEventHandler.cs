using EventBus.EventHandlers;
using Jobs.Application.Queries.Jobs;
using Jobs.Domain.Aggregates.JobAggregate;
using Jobs.Infrastructure.Persistence.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Jobs.Application.IntegrationEventHandlers.Subscribers.Companies
{
    public class ContactPersonUpdatedIntegrationEventHandler : IntegrationEventHandler, IIntegrationEventHandler<ContactPersonUpdatedIntegrationEvent>
    {
        private readonly IServiceProvider _serviceProvider;

        public ContactPersonUpdatedIntegrationEventHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async override Task<bool> ExecuteAsync(string message)
        {
            var @event = JsonConvert.DeserializeObject<ContactPersonUpdatedIntegrationEvent>(message);

            if (@event?.Payload is null)
            {
                return false;
            }

            var contactPerson = @event.Payload;

            if (string.IsNullOrWhiteSpace(contactPerson.FirstName)) return true;
            if (string.IsNullOrWhiteSpace(contactPerson.LastName)) return true;

            await using var scope = _serviceProvider.CreateAsyncScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            var jobRepository = scope.ServiceProvider.GetRequiredService<IJobRepository>();

            var jobIds = await mediator.Send(new GetJobsHavingContactPersonQuery(contactPerson.Id));

            foreach (var jobId in jobIds)
            {
                var job = await jobRepository.GetAsync(jobId);

                job.SyncContactPerson(
                    contactPerson.Id,
                    contactPerson.FirstName,
                    contactPerson.LastName,
                    contactPerson.Email.Address,
                    contactPerson.Phone?.PhoneNumber,
                    contactPerson.PictureUri,
                    contactPerson.Position?.Id,
                    contactPerson.Position?.Code,
                    contactPerson.Position?.AliasTo?.Id,
                    contactPerson.Position?.AliasTo?.Code,
                    contactPerson.SystemLanguage);

                jobRepository.Update(job);
            }

            await jobRepository.UnitOfWork.SaveEntitiesAsync<Job>();

            return true;
        }
    }
}
