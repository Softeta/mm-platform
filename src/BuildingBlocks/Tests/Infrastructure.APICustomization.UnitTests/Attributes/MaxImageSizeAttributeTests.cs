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
    public class MaxImageSizeAttributeTests
    {
        [Theory]
        [InlineAutoMoqData(100, 101)]
        [InlineAutoMoqData(20, 101)]
        [InlineAutoMoqData(1000, 2000)]
        public void Validate_ThrowsValidationException_WhenFileIsLargerThanAllowed(
            int maxFileSize,
            int actualFileSize,
            Mock<IFormFile> fileMock,
            Mock<IOptions<ImageSettings>> imageSettingsMock,
            Mock<IServiceProvider> serviceProviderMock)
        {
            // Arrange
            var attribute = new MaxImageSizeAttribute();
            var imageSettings = new ImageSettings
            {
                MaxSizeInKilobytes = maxFileSize
            };

            fileMock
                .Setup(x => x.Length)
                .Returns(1024 * actualFileSize);

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
