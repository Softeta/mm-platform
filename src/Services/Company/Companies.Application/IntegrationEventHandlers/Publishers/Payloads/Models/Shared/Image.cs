using ValueObjects = Domain.Seedwork.Shared.ValueObjects;

namespace Companies.Application.IntegrationEventHandlers.Publishers.Payloads.Models.Shared
{
    internal class Image
    {
        public string OriginalUri { get; set; } = null!;
        public string ThumbnailUri { get; set; } = null!;

        public static Image? FromDomain(ValueObjects.Image? image)
        {
            if (image is null) return null;

            return new Image
            {
                OriginalUri = image.OriginalUri,
                ThumbnailUri = image.ThumbnailUri,
            };
        }
    }
}
