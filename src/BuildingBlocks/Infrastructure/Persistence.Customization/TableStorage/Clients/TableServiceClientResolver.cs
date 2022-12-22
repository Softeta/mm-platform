using Azure.Data.Tables;

namespace Persistence.Customization.TableStorage.Clients
{
    public class TableServiceClientResolver : IPrivateTableServiceClient, IWebTableServiceClient
    {
        private TableServiceClient Client;

        public TableServiceClientResolver(string connectionString)
        {
            Client = new TableServiceClient(connectionString);
        }

        public TableClient GetTableClient(string tableName)
        {
            return Client.GetTableClient(tableName);
        }

        public async Task CreateTableIfNotExistsAsync(string tableName, CancellationToken token)
        {
            await Client.CreateTableIfNotExistsAsync(tableName, token);
        }
    }
}
