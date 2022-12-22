namespace Persistence.Customization.TableStorage.Models
{
    public class FileCacheEntity : TableStorageEntityBase, IEquatable<FileCacheEntity>
    {
        public DateTimeOffset ExpirationDate { get; set; }
        public string Category { get; set; } = null!;
        public string FileName { get; set; } = null!;
        public string FullFilePath { get; set; } = null!;

        public bool Equals(FileCacheEntity? other)
        {
            if (other is null)
            {
                return false;
            }

            return PartitionKey == other.PartitionKey &&
                   RowKey == other.RowKey &&
                   ExpirationDate == other.ExpirationDate &&
                   Category == other.Category &&
                   FileName == other.FileName &&
                   FullFilePath == other.FullFilePath;
        }
    }
}
