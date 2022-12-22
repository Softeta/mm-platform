using Azure;
using Azure.Data.Tables;

namespace Persistence.Customization.TableStorage.Models
{
    public abstract class TableStorageEntityBase : ITableEntity
    {
        public string PartitionKey { get; set; } = null!;
        public string RowKey { get; set; } = null!;
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }
}
