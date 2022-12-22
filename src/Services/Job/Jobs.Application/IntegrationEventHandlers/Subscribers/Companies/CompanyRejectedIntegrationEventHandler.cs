using EventBus.EventHandlers;
using Jobs.Application.Queries.Jobs;
using Jobs.Domain.Aggregates.JobAggregate;
using Jobs.Infrastructure.Persistence.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Jobs.Application.IntegrationEventHandlers.Subscribers.Companies
{
    public class CompanyRejectedIntegrationEventHandler : IntegrationEventHandler, IIntegrationEventHandler<CompanyRejectedIntegrationEvent>
    {
        private readonly IServiceProvider _serviceProvider;

        public CompanyRejectedIntegrationEventHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }


        public override async Task<bool> ExecuteAsync(string message)
        {
            var @event = JsonConvert.DeserializeObject<CompanyRejectedIntegrationEvent>(message);

            if (@event?.Payload is null)
            {
                return false;
            }

            var company = @event.Payload;

            await using var scope = _serviceProvider.CreateAsyncScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            var jobRepository = scope.ServiceProvider.GetRequiredService<IJobRepository>();

            var jobIds = await mediator.Send(new GetJobsHavingCompanyQuery(company.Id));
            
            foreach (var jobId in jobIds)
            {
                await jobRepository.RemoveAsync(jobId);
            }

            return await jobRepository.UnitOfWork.SaveEntitiesAsync<Job>();
        }
    }
}
