using Domain.Seedwork.Shared.ValueObjects;
using Tests.Shared;
using Xunit;

namespace Domain.Seedwork.UnitTests.ValueObjects
{
    public class AddressTests
    {
        [Theory]
        [InlineAutoMoqData("Vilnius", "Lithuania", "Vilnius (Lithuania)")]
        [InlineAutoMoqData("Kaunas", "Estonia", "Kaunas (Estonia)")]
        [InlineAutoMoqData(null, "Lithuania", null)]
        [InlineAutoMoqData("", "Lithuania", null)]
        [InlineAutoMoqData("Vilnius", null, null)]
        [InlineAutoMoqData("Vilnius", "", null)]
        public void Constructor_ShouldSetLocation(
            string? city,
            string? country,
            string? expectedLocation,
            string addressLine,
            string? postalCode,
            double? longitude,
            double? latitude)
        {
            // Arrange

            // Act
            var address = new Address(
               addressLine,
               city,
               country,
               postalCode,
               longitude,
               latitude);

            // Assert
            Assert.Equal(expectedLocation, address.Location);
        }
    }
}
