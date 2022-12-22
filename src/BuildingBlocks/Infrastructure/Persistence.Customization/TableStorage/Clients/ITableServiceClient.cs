using Azure.Data.Tables;

namespace Persistence.Customization.TableStorage.Clients
{
    public interface ITableServiceClient
    {
        TableClient GetTableClient(string tableName);
        Task CreateTableIfNotExistsAsync(string tableName, CancellationToken token);
    }
}
