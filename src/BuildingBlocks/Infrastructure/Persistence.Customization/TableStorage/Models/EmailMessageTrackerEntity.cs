namespace Persistence.Customization.TableStorage.Models
{
    public class EmailMessageTrackerEntity : TableStorageEntityBase, IEquatable<EmailMessageTrackerEntity>
    {
        public Guid EntityId { get; set; }
        public Guid TargetId { get; set; }
        public string Email { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string FilterName { get; set; } = null!;

        public bool Equals(EmailMessageTrackerEntity? other)
        {
            if (other is null)
            {
                return false;
            }

            return PartitionKey == other.PartitionKey &&
                   RowKey == other.RowKey &&
                   EntityId == other.EntityId &&
                   TargetId == other.TargetId &&
                   Email == other.Email &&
                   Status == other.Status &&
                   FilterName == other.FilterName;
        }
    }
}
