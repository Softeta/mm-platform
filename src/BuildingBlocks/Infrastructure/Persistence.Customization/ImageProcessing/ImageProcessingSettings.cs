namespace Persistence.Customization.ImageProcessing
{
    public class ImageProcessingSettings
    {
        public Types[] Types { get; set; } = null!;
    }

    public class Types
    {
        public string Name { get; set; } = null!;
        public int SizeInPixels { get; set; }
    }
}