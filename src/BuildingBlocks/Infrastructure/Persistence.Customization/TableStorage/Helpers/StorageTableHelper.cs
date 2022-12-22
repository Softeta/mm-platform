using Azure.Data.Tables;

namespace Persistence.Customization.TableStorage.Helpers
{
    public static class StorageTableHelper
    {
        public static async Task CreateIfNotExistAsync(string connectionString, string tableName)
        {
            var tableServiceClient = new TableServiceClient(connectionString);
            await tableServiceClient.CreateTableIfNotExistsAsync(tableName);
        }
    }
}
