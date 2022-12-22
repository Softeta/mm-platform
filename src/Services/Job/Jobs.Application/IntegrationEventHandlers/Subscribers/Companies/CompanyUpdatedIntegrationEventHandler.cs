using EventBus.EventHandlers;
using Jobs.Application.Queries.Jobs;
using Jobs.Domain.Aggregates.JobAggregate;
using Jobs.Infrastructure.Persistence.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Jobs.Application.IntegrationEventHandlers.Subscribers.Companies
{
    public class CompanyUpdatedIntegrationEventHandler : IntegrationEventHandler, IIntegrationEventHandler<CompanyUpdatedIntegrationEvent>
    {
        private readonly IServiceProvider _serviceProvider;

        public CompanyUpdatedIntegrationEventHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async override Task<bool> ExecuteAsync(string message)
        {
            var @event = JsonConvert.DeserializeObject<CompanyUpdatedIntegrationEvent>(message);

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
                var job = await jobRepository.GetAsync(jobId);

                job.SyncCompany(company.Name, company.Status, company.LogoUri);
                jobRepository.Update(job);
            }

            await jobRepository.UnitOfWork.SaveEntitiesAsync<Job>();

            return true;
        }
    }
}
