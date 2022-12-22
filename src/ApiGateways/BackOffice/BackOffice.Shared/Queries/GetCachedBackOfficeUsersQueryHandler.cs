using Azure.Data.Tables;
using BackOffice.Shared.Constants;
using BackOffice.Shared.Entities;
using MediatR;
using Persistence.Customization.TableStorage.Clients;
using System.Collections.ObjectModel;
using System.Text;

namespace BackOffice.Shared.Queries
{
    internal class GetCachedBackOfficeUsersQueryHandler : IRequestHandler<GetCachedBackOfficeUsersQuery, ICollection<BackOfficeUserEntity>>
    {
        private readonly TableClient _backOfficeUsersClient;

        public GetCachedBackOfficeUsersQueryHandler(IWebTableServiceClient client)
        {
            _backOfficeUsersClient = client.GetTableClient(TableStorageConstants.BackOfficeUsersTable);
        }

        public async Task<ICollection<BackOfficeUserEntity>> Handle(GetCachedBackOfficeUsersQuery request, CancellationToken cancellationToken)
        {
            var cachedUsers = new Collection<BackOfficeUserEntity>();

            var filter = BuildFilter(request.BackOfficeUserIds);
            var users = _backOfficeUsersClient.QueryAsync<TableEntity>(filter, cancellationToken: cancellationToken);

            await foreach (var user in users)
            {
                if (user != null)
                {
                    cachedUsers.Add(BackOfficeUserEntity.FromTableEntity(user));
                }
            }

            return cachedUsers;
        }

        private static string BuildFilter(IList<Guid>? backOfficeUserIds)
        {
            var filter = new StringBuilder($"PartitionKey eq '{TableStorageConstants.BackOfficeUserPartitionKey}'");

            if (backOfficeUserIds is null || backOfficeUserIds.Count == 0)
            {
                return filter.ToString();
            }

            filter.Append(" and (");
            
            for (var i = 0; i < backOfficeUserIds.Count; i++)
            {
                if (i == 0)
                {
                    filter.Append($"RowKey eq '{backOfficeUserIds[i]}'");
                    continue;
                }
                filter.Append($" or RowKey eq '{backOfficeUserIds[i]}'");
            }

            filter.Append(')');

            return filter.ToString();
        }
    }
}
