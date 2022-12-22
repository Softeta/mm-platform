using Domain.Seedwork.Exceptions;
using Domain.Seedwork.Shared.ValueObjects;
using System;
using Xunit;

namespace Domain.Seedwork.UnitTests.ValueObjects
{
    public class PhoneTests
    {
        [Theory]
        [InlineData("+370", "1122356")]
        [InlineData("+3", "1111")]
        [InlineData("+37", "1234567891234567891234567891")]
        public void Constructor_ShouldInitialize(string? countryCode, string? number)
        {
            // Arrange
            var expectedPhoneNumber = $"{countryCode}{number}";

            // Act
            var phone = new Phone(countryCode, number);

            // Assert
            Assert.Equal(countryCode, phone.CountryCode);
            Assert.Equal(number, phone.Number);
            Assert.Equal(expectedPhoneNumber, phone.PhoneNumber);
        }

        [Theory]
        [InlineData("", "1122356")]
        [InlineData(null, "1111")]
        public void Constructor_ShouldThrowDomainException_WhenCountryCodeIsEmpty(string? countryCode, string? number)
        {
            // Arrange

            // Act
            Action action = () => new Phone(countryCode, number);

            // Assert
            Assert.Throws<DomainException>(action);
        }


        [Theory]
        [InlineData("+3706", "1122356")]
        public void Constructor_ShouldThrowDomainException_WhenCountryCodeLongerThan4(string? countryCode, string? number)
        {
            // Arrange

            // Act
            Action action = () => new Phone(countryCode, number);

            // Assert
            Assert.Throws<DomainException>(action);
        }

        [Theory]
        [InlineData("+37", "")]
        [InlineData("+3", null)]
        [InlineData("+3", "1")]
        public void Constructor_ShouldThrowDomainException_WhenNumberIsEmptyOrShorterThan4(string? countryCode, string? number)
        {
            // Arrange

            // Act
            Action action = () => new Phone(countryCode, number);

            // Assert
            Assert.Throws<DomainException>(action);
        }

        [Theory]
        [InlineData("+37", "12345678912345678912345678912")]
        public void Constructor_ShouldThrowDomainException_WhenNumberIsLongerThan28(string? countryCode, string? number)
        {
            // Arrange

            // Act
            Action action = () => new Phone(countryCode, number);

            // Assert
            Assert.Throws<DomainException>(action);
        }
    }
}
