using Companies.Application.Commands.ContactPersons;
using Companies.Application.IntegrationEventHandlers.Subscribers.Companies.Payload;
using Companies.Domain.Aggregates.CompanyAggregate;
using Companies.Infrastructure.Persistence.Repositories;
using EventBus.EventHandlers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Persistence.Customization.Commands.Files;
using Persistence.Customization.FileStorage.Clients.Public;

namespace Companies.Application.IntegrationEventHandlers.Subscribers.Companies
{
    public class CompanyRejectedIntegrationEventHandler :
        IntegrationEventHandler,
        IIntegrationEventHandler<CompanyRejectedIntegrationEvent>
    {
        private readonly IServiceProvider _serviceProvider;

        public CompanyRejectedIntegrationEventHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override async Task<bool> ExecuteAsync(string message)
        {
            var @event = JsonConvert.DeserializeObject<CompanyRejectedIntegrationEvent>(message);

            if (@event?.Payload is null) return false;

            var company = @event.Payload;

            await using var scope = _serviceProvider.CreateAsyncScope();
            var companyRepository = scope.ServiceProvider.GetRequiredService<ICompanyRepository>();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            await DeleteAllPublicCompanyFilesAsync(mediator, company);

            await Parallel.ForEachAsync(company.ContactPersons, async (contactPerson, _) =>
            {
                var command = new DeleteContactPersonAssetsCommand(
                    contactPerson.ExternalId,
                    contactPerson.Picture?.OriginalUri,
                    contactPerson.Picture?.ThumbnailUri);

                await mediator.Publish(command);
            });

            await companyRepository.RemoveAsync(company.Id);
            return await companyRepository.UnitOfWork.SaveEntitiesAsync<Company>();
        }


        private async Task DeleteAllPublicCompanyFilesAsync(
            IMediator mediator,
            CompanyPayload company)
        {
            var uris = new List<Uri>();

            if (company.Logo != null)
            {
                uris.Add(new Uri(company.Logo.OriginalUri));
                uris.Add(new Uri(company.Logo.ThumbnailUri));
            }

            var command = new DeleteFilesBatchCommand<IPublicFileDeleteClient>(uris);
            await mediator.Publish(command);
        }
    }
}
