using Azure;
using Azure.Data.Tables;
using BackOffice.Shared.Constants;
using Microsoft.Graph;

namespace BackOffice.Shared.Entities
{
    public class BackOfficeUserEntity : ITableEntity, IEquatable<BackOfficeUserEntity>
    {
        public string PartitionKey { get; set; } = null!;
        public string RowKey { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PictureUri { get; set; }
        public string? PictureETag { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }

        public static BackOfficeUserEntity FromTableEntity(TableEntity entity)
        {
            return new BackOfficeUserEntity
            {
                PartitionKey = entity.PartitionKey,
                RowKey = entity.RowKey,
                FirstName = entity.GetString(nameof(FirstName)),
                LastName = entity.GetString(nameof(LastName)),
                Email = entity.GetString(nameof(Email)),
                PictureUri = entity.GetString(nameof(PictureUri)),
                PictureETag = entity.GetString(nameof(PictureETag)),
                Timestamp = entity.Timestamp,
                ETag = entity.ETag
            };
        }

        public static BackOfficeUserEntity FromAdUser(User user)
        {
            return new BackOfficeUserEntity
            {
                PartitionKey = TableStorageConstants.BackOfficeUserPartitionKey,
                RowKey = user.Id,
                FirstName = user.GivenName,
                LastName = user.Surname,
                Email = user.Mail,
                Timestamp = DateTimeOffset.UtcNow
            };
        }

        public bool Equals(BackOfficeUserEntity? other)
        {
            if (other is null)
            {
                return false;
            }

            return PartitionKey == other.PartitionKey &&
                   RowKey == other.RowKey &&
                   FirstName == other.FirstName &&
                   LastName == other.LastName &&
                   Email == other.Email;
        }
    }
}
