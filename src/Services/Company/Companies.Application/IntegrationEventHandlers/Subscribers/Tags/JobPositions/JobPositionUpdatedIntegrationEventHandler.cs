using Companies.Application.IntegrationEventHandlers.Subscribers.Tags.JobPositions.Payloads;
using Companies.Application.Queries;
using Companies.Domain.Aggregates.CompanyAggregate;
using Companies.Infrastructure.Persistence.Repositories;
using EventBus.EventHandlers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Companies.Application.IntegrationEventHandlers.Subscribers.Tags.JobPositions
{
    public class JobPositionUpdatedIntegrationEventHandler :
        IntegrationEventHandler,
        IIntegrationEventHandler<JobPositionUpdatedIntegrationEvent>
    {
        private readonly IServiceProvider _serviceProvider;
        public JobPositionUpdatedIntegrationEventHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override async Task<bool> ExecuteAsync(string message)
        {
            var @event = JsonConvert.DeserializeObject<JobPositionUpdatedIntegrationEvent>(message);

            ValidatePayload(@event?.Payload);

            var jobPosition = @event!.Payload!;

            await using var scope = _serviceProvider.CreateAsyncScope();
            var companyRepository = scope.ServiceProvider.GetRequiredService<ICompanyRepository>();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<JobPositionUpdatedIntegrationEventHandler>>();

            var companyIds = await mediator.Send(new GetCompaniesByJobPositionQuery(jobPosition.Id!.Value));

            foreach (var companyId in companyIds)
            {
                try
                {
                    var company = await companyRepository.GetAsync(companyId);
                    company.SyncJobPositions(jobPosition.Id!.Value, jobPosition.AliasTo?.Id, jobPosition.AliasTo?.Code);

                    companyRepository.Update(company);
                    await companyRepository.UnitOfWork.SaveEntitiesAsync<Company>();
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Failed to sync job position for company: {companyId}. Payload: {message}", companyId, message);
                }
            }

            return true;
        }

        private void ValidatePayload(JobPositionPayload? payload)
        {
            if (payload is null) throw new Exception("Payload is empty");
            if (payload.Id is null) throw new Exception("Job position id was not found");
        }
    }
}
