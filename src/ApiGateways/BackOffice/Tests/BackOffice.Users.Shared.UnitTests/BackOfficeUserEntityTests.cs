using Azure.Data.Tables;
using BackOffice.Shared.Constants;
using BackOffice.Shared.Entities;
using Microsoft.Graph;
using Tests.Shared;
using Xunit;

namespace BackOffice.Users.Shared.UnitTests
{
    public class BackOfficeUserEntityTests
    {
        [Theory, AutoMoqData]
        public void FromAdUser_ShouldInitializeInstance(string id, string givenName, string surName, string mail)
        {
            // Arrange
            var user = new User
            {
                Id = id,
                GivenName = givenName,
                Surname = surName,
                Mail = mail
            };

            // Act
            var entity = BackOfficeUserEntity.FromAdUser(user);

            // Assert
            Assert.Equal(TableStorageConstants.BackOfficeUserPartitionKey, entity.PartitionKey);
            Assert.Equal(user.Id, entity.RowKey);
            Assert.Equal(user.GivenName, entity.FirstName);
            Assert.Equal(user.Surname, entity.LastName);
            Assert.Equal(user.Mail, entity.Email);
        }

        [Theory, AutoMoqData]
        public void FromTableEntity_ShouldInitializeInstance(
            TableEntity entity,
            string firstName,
            string lastName,
            string email,
            string pictureUri,
            string pictureETag)
        {
            // Arrange
            entity[nameof(BackOfficeUserEntity.FirstName)] = firstName;
            entity[nameof(BackOfficeUserEntity.LastName)] = lastName;
            entity[nameof(BackOfficeUserEntity.Email)] = email;
            entity[nameof(BackOfficeUserEntity.PictureUri)] = pictureUri;
            entity[nameof(BackOfficeUserEntity.PictureETag)] = pictureETag;

            // Act
            var user = BackOfficeUserEntity.FromTableEntity(entity);

            // Assert
            Assert.Equal(entity.PartitionKey, entity.PartitionKey);
            Assert.Equal(entity.RowKey, user.RowKey);
            Assert.Equal(firstName, user.FirstName);
            Assert.Equal(lastName, user.LastName);
            Assert.Equal(email, user.Email);
            Assert.Equal(pictureUri, user.PictureUri);
            Assert.Equal(pictureETag, user.PictureETag);
        }
    }
}