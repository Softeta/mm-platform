using Azure.Data.Tables;
using BackOffice.Shared.Constants;
using BackOffice.Shared.Entities;
using Persistence.Customization.TableStorage.Clients;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BackOffice.Users.CacheRefresher.Commands
{
    internal abstract class BaseCommandHandler
    {
        private readonly TableClient _backOfficeUsersClient;

        protected BaseCommandHandler(IWebTableServiceClient client)
        {
            _backOfficeUsersClient = client.GetTableClient(TableStorageConstants.BackOfficeUsersTable);
        }

        protected async Task SyncBackOfficeUserChangesAsync(
            ICollection<BackOfficeUserEntity> users,
            TableTransactionActionType transactionActionType,
            CancellationToken cancellationToken)
        {
            for (var skip = 0; skip < users.Count; skip += TableStorageConstants.MaximumBatchOperations)
            {
                var batch = GetTransactionActionItems(
                    users,
                    skip,
                    skip + TableStorageConstants.MaximumBatchOperations,
                    transactionActionType);
                await _backOfficeUsersClient.SubmitTransactionAsync(batch, cancellationToken);
            }
        }

        private static IEnumerable<TableTransactionAction> GetTransactionActionItems(
            IEnumerable<BackOfficeUserEntity> users,
            int skip,
            int take,
            TableTransactionActionType transactionActionType)
        {
            var usersToTake = users.Skip(skip).Take(take);
            return usersToTake
                .Select(backOfficeUser => new TableTransactionAction(transactionActionType, backOfficeUser))
                .ToList();
        }
    }
}
