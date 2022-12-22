using Companies.Application.Commands.RegistryCenter;
using EventBus.EventHandlers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Companies.Application.IntegrationEventHandlers.Subscribers.Schedulers
{
    public class SyncRegistryCenterCompaniesSheduledIntegrationEventHandler : 
        IntegrationEventHandler,
        IIntegrationEventHandler<SyncRegistryCenterCompaniesSheduledIntegrationEvent>
    {
        private readonly IServiceProvider _serviceProvider;

        public SyncRegistryCenterCompaniesSheduledIntegrationEventHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override async Task<bool> ExecuteAsync(string message)
        {
            await using var scope = _serviceProvider.CreateAsyncScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            await mediator.Publish(new SyncDanishCompaniesCommand());
            // TODO: publish swedish when implemented. After #2059 Investigation

            return true;
        }
    }
}
 