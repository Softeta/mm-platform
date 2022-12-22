using BackOffice.Shared.Entities;

namespace BackOffice.Bff.API.Models.Users
{
    public class BackOfficeUser
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PictureUri { get; set; }

        public static BackOfficeUser FromTableEntity(BackOfficeUserEntity entity)
        {
            return new BackOfficeUser
            {
                Id = new Guid(entity.RowKey),
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email,
                PictureUri = entity.PictureUri
            };
        }
    }
}
