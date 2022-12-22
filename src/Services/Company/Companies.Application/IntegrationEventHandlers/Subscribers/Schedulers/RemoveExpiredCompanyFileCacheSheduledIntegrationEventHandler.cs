using Companies.Infrastructure.Settings;
using EventBus.EventHandlers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Persistence.Customization.Commands.Files;
using Persistence.Customization.FileStorage.Clients.Public;
using Persistence.Customization.Queries;
using Persistence.Customization.TableStorage;

namespace Companies.Application.IntegrationEventHandlers.Subscribers.Schedulers
{
    public class RemoveExpiredCompanyFileCacheSheduledIntegrationEventHandler : 
        IntegrationEventHandler,
        IIntegrationEventHandler<RemoveExpiredCompanyFileCacheSheduledIntegrationEvent>
    {
        private readonly IServiceProvider _serviceProvider;

        public RemoveExpiredCompanyFileCacheSheduledIntegrationEventHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override async Task<bool> ExecuteAsync(string message)
        {
            await using var scope = _serviceProvider.CreateAsyncScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            var companyContainers = scope.ServiceProvider.GetRequiredService<IOptions<BlobContainerSettings>>().Value;
            var expiredFileCaches = await mediator.Send(new GetExpiredFileCachesQuery(
                FileCacheTableStorage.Company.FilePartitionKey));

            if (expiredFileCaches.Count == 0)
            {
                return true;
            }

            var exceptions = new List<Exception>();

            await Parallel.ForEachAsync(expiredFileCaches, async (cache, _) =>
            {
                try
                {
                    switch (cache.Category)
                    {
                        case FileCacheTableStorage.Company.Category.Logo:
                            var commandLogo = new DeleteFileCommand<IPublicFileDeleteClient>(
                                companyContainers.LogosContainer,
                                FileCacheTableStorage.Company.FilePartitionKey,
                                Guid.Parse(cache.RowKey));
                            await mediator.Publish(commandLogo);
                            break;
                        case FileCacheTableStorage.Company.Category.ContactPersonPicture:
                            var commandContactPersonPicture = new DeleteFileCommand<IPublicFileDeleteClient>(
                                companyContainers.ContactPicturesContainer,
                                FileCacheTableStorage.Company.FilePartitionKey,
                                Guid.Parse(cache.RowKey));
                            await mediator.Publish(commandContactPersonPicture);
                            break;
                        default:
                            break;
                    }

                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }
            });

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }

            return true;
        }
    }
}
 