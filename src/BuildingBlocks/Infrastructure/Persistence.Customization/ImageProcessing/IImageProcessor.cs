using Domain.Seedwork.Enums;

namespace Persistence.Customization.ImageProcessing
{
    public interface IImageProcessor
    {
        Task<List<(ImageType Type, Stream Stream)>> ExecuteAsync(
                    Stream imageStream,
                    CancellationToken cancellation);
    }
}
