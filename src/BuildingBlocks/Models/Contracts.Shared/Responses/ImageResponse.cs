using Domain.Seedwork.Shared.ValueObjects;

namespace Contracts.Shared.Responses
{
    public class ImageResponse
    {
        public string? Uri { get; set; }

        public static ImageResponse? FromDomain(Image? logo)
        {
            if (logo is null) return null;

            return new ImageResponse
            {
                Uri = logo.ThumbnailUri,
            };
        }
    }
}
