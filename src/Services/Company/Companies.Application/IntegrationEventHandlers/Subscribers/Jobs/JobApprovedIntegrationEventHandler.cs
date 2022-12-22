using Companies.Application.Commands.Companies;
using Domain.Seedwork.Enums;
using EventBus.EventHandlers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Companies.Application.IntegrationEventHandlers.Subscribers.Jobs
{
    public class JobApprovedIntegrationEventHandler :
        IntegrationEventHandler,
        IIntegrationEventHandler<JobApprovedIntegrationEvent>
    {
        private readonly IServiceProvider _serviceProvider;

        public JobApprovedIntegrationEventHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override async Task<bool> ExecuteAsync(string message)
        {
            var @event = JsonConvert.DeserializeObject<JobApprovedIntegrationEvent>(message);

            if (@event?.Payload is null) return false;

            var job = @event.Payload;

            await using var scope = _serviceProvider.CreateAsyncScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            if (job.Company.Status == CompanyStatus.Pending)
            {
                await mediator.Publish(new ApproveCompanyCommand(job.Company.Id));
            }
 
            return true;
        }
    }
}
