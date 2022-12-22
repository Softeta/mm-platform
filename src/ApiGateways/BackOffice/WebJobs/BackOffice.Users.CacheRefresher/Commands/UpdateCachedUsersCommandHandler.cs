using Azure.Data.Tables;
using BackOffice.Shared.Entities;
using BackOffice.Users.CacheRefresher.EventHandlers;
using MediatR;
using Persistence.Customization.TableStorage.Clients;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BackOffice.Users.CacheRefresher.Commands
{
    internal class UpdateCachedUsersCommandHandler : BaseCommandHandler, INotificationHandler<UpdateCachedUsersCommand>
    {
        private readonly IMediator _mediator;

        public UpdateCachedUsersCommandHandler(
            IWebTableServiceClient client, 
            IMediator mediator) : base(client)
        {
            _mediator = mediator;
        }

        public async Task Handle(UpdateCachedUsersCommand request, CancellationToken cancellationToken)
        {
            var usersToUpdate = new Collection<BackOfficeUserEntity>();

            foreach (var backOfficeUser in request.BackOfficeUsers)
            {
                var cachedUser = request.CachedUsers.FirstOrDefault(x =>
                    x.PartitionKey == backOfficeUser.PartitionKey && x.RowKey == backOfficeUser.RowKey);

                if (cachedUser is null)
                {
                    continue;
                }

                var hasUserChanged = !cachedUser.Equals(backOfficeUser);

                if (hasUserChanged)
                {
                    backOfficeUser.PictureUri = cachedUser.PictureUri;
                    backOfficeUser.PictureETag = cachedUser.PictureETag;

                    usersToUpdate.Add(backOfficeUser);
                }
            }

            if (usersToUpdate.Count == 0)
            {
                return;
            }

            await SyncBackOfficeUserChangesAsync(usersToUpdate, TableTransactionActionType.UpdateMerge, cancellationToken);
            await _mediator.Publish(new BackOfficeUsersUpdatedEvent(usersToUpdate), cancellationToken);
        }
    }
}
