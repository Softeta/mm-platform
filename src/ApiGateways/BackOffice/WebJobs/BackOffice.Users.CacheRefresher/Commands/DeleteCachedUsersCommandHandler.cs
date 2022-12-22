using Azure.Data.Tables;
using MediatR;
using Microsoft.Extensions.Logging;
using Persistence.Customization.FileStorage.Clients.Public;
using Persistence.Customization.TableStorage.Clients;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BackOffice.Users.CacheRefresher.Commands
{
    internal class DeleteCachedUsersCommandHandler : BaseCommandHandler, INotificationHandler<DeleteCachedUsersCommand>
    {
        private readonly IPublicFileDeleteClient _fileDeleteClient;
        private readonly ILogger<DeleteCachedUsersCommandHandler> _logger;

        public DeleteCachedUsersCommandHandler(
            IWebTableServiceClient client,
            IPublicFileDeleteClient fileDeleteClient, 
            ILogger<DeleteCachedUsersCommandHandler> logger): base(client)
        {
            _fileDeleteClient = fileDeleteClient;
            _logger = logger;
        }

        public async Task Handle(DeleteCachedUsersCommand request, CancellationToken cancellationToken)
        {
            var uris = request.Users
                .Where(x => !string.IsNullOrWhiteSpace(x.PictureUri))
                .Select(x => new Uri(x.PictureUri!))
                .ToArray();

            if (uris.Length > 0)
            {
                try
                {
                    await _fileDeleteClient.BatchDeleteAsync(uris, cancellationToken);
                }
                catch (AggregateException ex)
                {
                    foreach (var innerException in ex.InnerExceptions)
                    {
                        _logger.LogCritical(innerException, "Failed trying delete blob");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex, "BatchDeleteAsync failed");
                }
            }

            await SyncBackOfficeUserChangesAsync(request.Users, TableTransactionActionType.Delete, cancellationToken);
        }
    }
}
