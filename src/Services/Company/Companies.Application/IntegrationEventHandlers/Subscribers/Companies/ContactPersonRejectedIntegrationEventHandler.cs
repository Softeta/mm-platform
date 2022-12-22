using Companies.Application.Commands.ContactPersons;
using Companies.Domain.Aggregates.CompanyAggregate;
using Companies.Infrastructure.Persistence.Repositories;
using EventBus.EventHandlers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Companies.Application.IntegrationEventHandlers.Subscribers.Companies
{
    public class ContactPersonRejectedIntegrationEventHandler :
        IntegrationEventHandler,
        IIntegrationEventHandler<ContactPersonRejectedIntegrationEvent>
    {
        private readonly IServiceProvider _serviceProvider;

        public ContactPersonRejectedIntegrationEventHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override async Task<bool> ExecuteAsync(string message)
        {
            var @event = JsonConvert.DeserializeObject<ContactPersonRejectedIntegrationEvent>(message);

            if (@event?.Payload is null) return false;

            var contactPerson = @event.Payload;

            await using var scope = _serviceProvider.CreateAsyncScope();
            var companyRepository = scope.ServiceProvider.GetRequiredService<ICompanyRepository>();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            var command = new DeleteContactPersonAssetsCommand(
                contactPerson.ExternalId,
                contactPerson.Picture?.OriginalUri,
                contactPerson.Picture?.ThumbnailUri);

            await mediator.Publish(command);

            var company = await companyRepository.GetAsync(contactPerson.CompanyId);
            company.DeleteContactPerson(contactPerson.Id);

            companyRepository.Update(company);
            await companyRepository.UnitOfWork.SaveEntitiesAsync<Company>();

            return true;
        }
    }
}
