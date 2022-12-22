using EventBus.EventHandlers;
using Jobs.Application.Queries.Jobs;
using Jobs.Domain.Aggregates.JobAggregate;
using Jobs.Infrastructure.Persistence.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Jobs.Application.IntegrationEventHandlers.Subscribers.BackOfficeUsers
{
    public class BackOfficeUserUpdatedIntegrationEventHandler : IntegrationEventHandler, IIntegrationEventHandler<BackOfficeUserUpdatedIntegrationEvent>
    {
        private readonly IServiceProvider _serviceProvider;

        public BackOfficeUserUpdatedIntegrationEventHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override async Task<bool> ExecuteAsync(string message)
        {
            var @event = JsonConvert.DeserializeObject<BackOfficeUserUpdatedIntegrationEvent>(message);

            if (@event?.Payload is null)
            {
                return false;
            }

            var employee = @event.Payload;

            await using var scope = _serviceProvider.CreateAsyncScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            var jobsRepository = scope.ServiceProvider.GetRequiredService<IJobRepository>();

            var jobIds = await mediator.Send(new GetJobsHavingEmployeeQuery(employee.UserId));

            foreach (var jobId in jobIds)
            {
                var job = await jobsRepository.GetAsync(jobId);

                if (job.Owner != null && job.Owner.Id == employee.UserId)
                {
                    job.UpdateJobOwner(employee.FirstName, employee.LastName, employee.PictureUri);
                }

                job.UpdateAssignedEmployee(
                    employee.UserId,
                    employee.FirstName,
                    employee.LastName,
                    employee.PictureUri);

                jobsRepository.Update(job);
            }

            return await jobsRepository.UnitOfWork.SaveEntitiesAsync<Job>();
        }
    }
}
