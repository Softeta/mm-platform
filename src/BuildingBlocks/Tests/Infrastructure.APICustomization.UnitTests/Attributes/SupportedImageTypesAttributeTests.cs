using Custom.Attributes;
using Custom.Attributes.Settings;
using Domain.Seedwork.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.ComponentModel.DataAnnotations;
using Tests.Shared;
using Xunit;

namespace Infrastructure.APICustomization.UnitTests.Attributes
{
    public class SupportedImageTypesAttributeTests
    {

        [Theory]
        [InlineAutoMoqData("image/jog")]
        [InlineAutoMoqData("pdf/png")]
        [InlineAutoMoqData("png")]
        public void Validate_ThrowsValidationException_WhenFileIsLargerThanAllowed(
            string imageType,
            Mock<IFormFile> fileMock,
            Mock<IOptions<ImageSettings>> imageSettingsMock,
            Mock<IServiceProvider> serviceProviderMock)
        {
            // Arrange
            var attribute = new SupportedImageTypesAttribute();
            var imageSettings = new ImageSettings
            {
                SupportedTypes = new[] { "image/png" }
            };

            fileMock
                .Setup(x => x.ContentType)
                .Returns(imageType);

            imageSettingsMock
                .Setup(x => x.Value)
                .Returns(imageSettings);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IOptions<ImageSettings>)))
                .Returns(imageSettingsMock.Object);

            var validationContext = new ValidationContext(fileMock.Object, serviceProviderMock.Object, null);

            // Act
            Action action = () => attribute.Validate(fileMock.Object, validationContext);

            // Assert
            Assert.Throws<BadRequestException>(action);
        }
    }
}
