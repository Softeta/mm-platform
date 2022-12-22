using Azure.Data.Tables;
using Candidates.Application.Queries;
using Candidates.Domain.Aggregates.CandidateAggregate;
using Candidates.Infrastructure.Clients.MicrosoftGraph;
using Candidates.Infrastructure.Persistence.Repositories;
using EventBus.EventHandlers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Customization.TableStorage;
using Persistence.Customization.TableStorage.Clients;
using Persistence.Customization.TableStorage.Models;

namespace Candidates.Application.IntegrationEventHandlers.Subscribers.Schedulers
{
    public class DeleteCandidatesSheduledIntegrationEventHandler : 
        IntegrationEventHandler,
        IIntegrationEventHandler<DeleteCandidatesSheduledIntegrationEvent>
    {
        private readonly IServiceProvider _serviceProvider;

        public DeleteCandidatesSheduledIntegrationEventHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override async Task<bool> ExecuteAsync(string message)
        {
            await using var scope = _serviceProvider.CreateAsyncScope();
            var candidateRepository = scope.ServiceProvider.GetRequiredService<ICandidateRepository>();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            var msClientService = scope.ServiceProvider.GetRequiredService<IMsGraphServiceClient>();
            var privateTableServiceClient = scope.ServiceProvider.GetRequiredService<IPrivateTableServiceClient>();
            var emailMessageTable = privateTableServiceClient.GetTableClient(EmailMessageTableStorageConstants.TableName);

            var expiredRegisteredCandidates = await mediator.Send(new GetExpiredRegisteredCandidatesQuery());

            if (expiredRegisteredCandidates.Count == 0)
            {
                return true;
            }

            foreach (var expiredCandidate in expiredRegisteredCandidates)
            {
                await DeleteExpiredCandidateFromAzureAdAsync(msClientService, expiredCandidate.ExternalId);
                await DeleteExpiredCandidateFromTableStorageAsync(emailMessageTable, expiredCandidate.CandidateId);
                await DeleteCandidatesFromDatabaseAsync(candidateRepository, expiredCandidate.CandidateId);
            }

            return true;
        }

        private async Task DeleteExpiredCandidateFromAzureAdAsync(IMsGraphServiceClient client, Guid id)
        {
            await client.DeleteUserAsync(id);
        }

        private async Task DeleteExpiredCandidateFromTableStorageAsync(TableClient emailMessageTable, Guid id)
        {
            var entitiesToRemove = emailMessageTable.QueryAsync<EmailMessageTrackerEntity>(x => x.EntityId == id);
            await foreach (var entity in entitiesToRemove)
            {
                await emailMessageTable.DeleteEntityAsync(entity.PartitionKey, entity.RowKey, entity.ETag);
            }
        }

        private async Task DeleteCandidatesFromDatabaseAsync(ICandidateRepository repository, Guid id)
        {
            await repository.RemoveAsync(id);
            await repository.UnitOfWork.SaveEntitiesAsync<Candidate>();
        }
    }
}
 