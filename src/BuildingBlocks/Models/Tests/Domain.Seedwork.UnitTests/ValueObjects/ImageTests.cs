using Domain.Seedwork.Enums;
using Domain.Seedwork.Exceptions;
using Domain.Seedwork.Shared.ValueObjects;
using System.Collections.Generic;
using Tests.Shared;
using Xunit;

namespace Domain.Seedwork.UnitTests.ValueObjects
{
    public class ImageTests
    {
        [Theory, AutoMoqData]
        public void Constructor_ShouldInitializeImage(
            string originalPath,
            string thumbnailPath)
        {
            // Arrange
            var imagePaths = new Dictionary<ImageType, string?>();
            imagePaths.TryAdd(ImageType.Original, originalPath);
            imagePaths.TryAdd(ImageType.Thumbnail, thumbnailPath);

            // Act
            var image = new Image(imagePaths);

            // Assert
            Assert.Equal(originalPath, image.OriginalUri);
            Assert.Equal(thumbnailPath, image.ThumbnailUri);
        }

        [Theory, AutoMoqData]
        public void Constructor_ShouldThrowDomainException_WhenNoOriginalPath(
            string thumbnailPath)
        {
            // Arrange
            var imagePaths = new Dictionary<ImageType, string?>();
            imagePaths.TryAdd(ImageType.Thumbnail, thumbnailPath);

            // Act
            var action = () => new Image(imagePaths);

            // Assert
            Assert.Throws<DomainException>(action);
        }

        [Theory, AutoMoqData]
        public void Constructor_ShouldThrowDomainException_WhenNoThumbnailPath(
            string originalPath)
        {
            // Arrange
            var imagePaths = new Dictionary<ImageType, string?>();
            imagePaths.TryAdd(ImageType.Original, originalPath);

            // Act
            var action = () => new Image(imagePaths);

            // Assert
            Assert.Throws<DomainException>(action);
        }

        [Fact]
        public void Constructor_ShouldThrowDomainException_WhenNoImagePaths()
        {
            // Arrange
            var imagePaths = new Dictionary<ImageType, string?>();

            // Act
            var action = () => new Image(imagePaths);

            // Assert
            Assert.Throws<DomainException>(action);
        }
    }
}
