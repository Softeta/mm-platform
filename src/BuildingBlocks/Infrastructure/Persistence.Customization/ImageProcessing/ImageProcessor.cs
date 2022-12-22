using Domain.Seedwork.Enums;
using Microsoft.Extensions.Options;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace Persistence.Customization.ImageProcessing
{
    public class ImageProcessor : IImageProcessor
    {
        private readonly ImageProcessingSettings _imageProcesingSettings;

        public ImageProcessor(IOptions<ImageProcessingSettings> imageProcesingSettings)
        {
            _imageProcesingSettings = imageProcesingSettings.Value;
        }

        public async Task<List<(ImageType Type, Stream Stream)>> ExecuteAsync(
            Stream imageStream, 
            CancellationToken cancellation)
        {
            var processedImageStreams = new List<(ImageType Type, Stream Stream)>();

            imageStream.Position = 0;
            var image = await Image.LoadAsync(imageStream, cancellation);

            var originalImageStream = await ConvertImageAsync(image, cancellation);
            processedImageStreams.Add((ImageType.Original, originalImageStream));

            foreach (var imageType in _imageProcesingSettings.Types)
            {
                ResizeImage(image,
                    imageType.SizeInPixels,
                    imageType.SizeInPixels);
                var processedImageStream = await ConvertImageAsync(image, cancellation);
                Enum.TryParse(imageType.Name, out ImageType typeName);
                processedImageStreams.Add((typeName, processedImageStream));
            }
            return processedImageStreams;
        }

        private static async Task<Stream> ConvertImageAsync(Image image, CancellationToken cancellation)
        {
            var memoryStream = new MemoryStream();
            await image.SaveAsJpegAsync(memoryStream, cancellation);
            return memoryStream;
        }

        private static void ResizeImage(Image image, int width, int height)
        {
            image.Mutate(ctx => ctx.Resize(new ResizeOptions
            {
                Mode = ResizeMode.Max,
                Size = new Size(width, height)
            }).BackgroundColor(Color.White));
        }
    }
}
