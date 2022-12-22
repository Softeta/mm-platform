using Domain.Seedwork.Shared.ValueObjects;
using System;
using Tests.Shared;
using Xunit;

namespace Domain.Seedwork.UnitTests.ValueObjects
{
    public class EmailTests
    {
        [Theory]
        [InlineAutoMoqData("valid@email.com")]
        public void Create_ShouldInitializeNotVerifiedEmail(string emailAddress)
        {
            // Assert

            // Act
            var email = Email.CreateNullable(emailAddress);

            // Arrange
            Assert.Equal(emailAddress, email!.Address);
            Assert.False(email.IsVerified);
            Assert.Null(email.VerificationKey);
            Assert.Null(email.VerificationRequestedAt);
            Assert.Null(email.VerifiedAt);
        }

        [Fact]
        public void Create_ShouldReturnNull_WhenEmailIsNull()
        {
            // Assert

            // Act
            var email = Email.CreateNullable(null);

            // Arrange
            Assert.Null(email);
        }

        [Theory]
        [InlineAutoMoqData("valid@email.com")]
        public void CreateWithVerification_ShouldInitializeNotVerifiedEmailWithVerificationKey(string emailAddress)
        {
            // Assert

            // Act
            var email = Email.CreateWithVerification(emailAddress);

            // Arrange
            Assert.Equal(emailAddress, email!.Address);
            Assert.False(email.IsVerified);
            Assert.NotEqual(Guid.Empty, email.VerificationKey);
            Assert.NotNull(email.VerificationRequestedAt);
            Assert.Null(email.VerifiedAt);
        }

        [Theory]
        [InlineAutoMoqData("valid@email.com")]
        public void CreateVerified_ShouldInitializeVerifiedEmail(string emailAddress)
        {
            // Assert

            // Act
            var email = Email.CreateVerified(emailAddress);

            // Arrange
            Assert.Equal(emailAddress, email!.Address);
            Assert.True(email.IsVerified);
            Assert.Null(email.VerificationKey);
            Assert.Null(email.VerificationRequestedAt);
            Assert.NotNull(email.VerifiedAt);
        }
    }
}
