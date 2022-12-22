using Domain.Seedwork.Enums;
using Domain.Seedwork.Exceptions;

namespace Domain.Seedwork.Shared.ValueObjects
{
    public class Image : ValueObject<Image>
    {
        public string OriginalUri { get; init; } = null!;
        public string ThumbnailUri { get; init; } = null!;

        private Image() { }

        public Image(Dictionary<ImageType, string?> imagePaths)
        {
            var thumbnailUri = imagePaths
                .Where(x => x.Key == ImageType.Thumbnail)
                .Select(x => x.Value)
                .SingleOrDefault();

            var originalUri = imagePaths
                .Where(x => x.Key == ImageType.Original)
                .Select(x => x.Value)
                .SingleOrDefault();

            Validate(thumbnailUri, originalUri);

            ThumbnailUri = thumbnailUri!;
            OriginalUri = originalUri!;
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return OriginalUri;
            yield return ThumbnailUri;
        }

        private void Validate(string? thumbnailUri, string? originalUri)
        {
            if (string.IsNullOrWhiteSpace(originalUri))
            {
                throw new DomainException("Original image path was not found", ErrorCodes.Shared.Image.OriginalPathNotFound);
            }
            if (string.IsNullOrWhiteSpace(thumbnailUri))
            {
                throw new DomainException("Thumbnail image path was not found", ErrorCodes.Shared.Image.ThumbnailPathNotFound);
            }
        }
    }
}
