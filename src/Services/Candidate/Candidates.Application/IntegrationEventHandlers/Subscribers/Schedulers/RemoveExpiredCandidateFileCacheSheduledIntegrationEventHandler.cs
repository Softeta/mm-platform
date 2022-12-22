using Candidates.Infrastructure.Settings;
using EventBus.EventHandlers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Persistence.Customization.Commands.Files;
using Persistence.Customization.FileStorage;
using Persistence.Customization.FileStorage.Clients.Private;
using Persistence.Customization.FileStorage.Clients.Public;
using Persistence.Customization.Queries;
using Persistence.Customization.TableStorage;

namespace Candidates.Application.IntegrationEventHandlers.Subscribers.Schedulers
{
    public class RemoveExpiredCandidateFileCacheSheduledIntegrationEventHandler : 
        IntegrationEventHandler,
        IIntegrationEventHandler<RemoveExpiredCandidateFileCacheSheduledIntegrationEvent>
    {
        private readonly IServiceProvider _serviceProvider;

        public RemoveExpiredCandidateFileCacheSheduledIntegrationEventHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override async Task<bool> ExecuteAsync(string message)
        {
            await using var scope = _serviceProvider.CreateAsyncScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            var candidateContainers = scope.ServiceProvider.GetRequiredService<IOptions<BlobContainerSettings>>().Value;
            var expiredFileCaches = await mediator.Send(new GetExpiredFileCachesQuery(
                FileCacheTableStorage.Candidate.FilePartitionKey));

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
                        case FileCacheTableStorage.Candidate.Category.Video:
                            var commandVideo = new DeleteFileCommand<IPrivateFileDeleteClient>(
                                candidateContainers.CandidateVideosContainer,
                                FileCacheTableStorage.Candidate.FilePartitionKey,
                                Guid.Parse(cache.RowKey));
                            await mediator.Publish(commandVideo);
                            break;
                        case FileCacheTableStorage.Candidate.Category.Picture:
                            var commandPicture = new DeleteFileCommand<IPublicFileDeleteClient>(
                                candidateContainers.CandidatePicturesContainer,
                                FileCacheTableStorage.Candidate.FilePartitionKey,
                                Guid.Parse(cache.RowKey));
                            await mediator.Publish(commandPicture);
                            break;
                        case FileCacheTableStorage.Candidate.Category.CurriculumVitae:
                            var commandCurriculumVitae = new DeleteFileCommand<IPrivateFileDeleteClient>(
                                candidateContainers.CandidateCurriculumVitaesContainer,
                                FileCacheTableStorage.Candidate.FilePartitionKey,
                                Guid.Parse(cache.RowKey));
                            await mediator.Publish(commandCurriculumVitae);
                            break;
                        case FileCacheTableStorage.Candidate.Category.Certificate:
                            var commandCertificate = new DeleteFileCommand<IPrivateFileDeleteClient>(
                                candidateContainers.CandidateCertificatesContainer,
                                FileCacheTableStorage.Candidate.FilePartitionKey,
                                Guid.Parse(cache.RowKey));
                            await mediator.Publish(commandCertificate);
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
 