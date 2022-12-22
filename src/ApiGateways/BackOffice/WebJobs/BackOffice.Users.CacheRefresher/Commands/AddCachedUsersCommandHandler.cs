using Azure.Data.Tables;
using MediatR;
using Persistence.Customization.TableStorage.Clients;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BackOffice.Users.CacheRefresher.Commands
{
    internal class AddCachedUsersCommandHandler : BaseCommandHandler, INotificationHandler<AddCachedUsersCommand>
    {
        public AddCachedUsersCommandHandler(IWebTableServiceClient client): base(client)
        {
        }

        public async Task Handle(AddCachedUsersCommand request, CancellationToken cancellationToken)
        {
            var usersToCreate = (
                    from backOfficeUserEntity in request.BackOfficeUsers
                    let notExist = request.CachedUsers.All(x =>
                        x.PartitionKey == backOfficeUserEntity.PartitionKey && x.RowKey != backOfficeUserEntity.RowKey)
                    where notExist
                    select backOfficeUserEntity)
                .ToList();

            if (usersToCreate.Count == 0)
            {
                return;
            }

            await SyncBackOfficeUserChangesAsync(usersToCreate, TableTransactionActionType.Add, cancellationToken);
        }
    }
}
